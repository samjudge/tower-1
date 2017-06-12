using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Unit {

    public Player Player;
    public float TickActionsEvery = 3f;
    private float TickActionsCurrentTimer = 0f;

    void Start() {
    }

    void Update() {
        //make this guy face towards player
        this.transform.rotation = Quaternion.Euler(
            new Vector3(
                Player.transform.rotation.eulerAngles.x,
                Player.transform.rotation.eulerAngles.y+180,
                Player.transform.rotation.eulerAngles.z
            )
        );
        this.TickActionsCurrentTimer += Time.deltaTime;
        if(!InputLocked && TickActionsCurrentTimer > TickActionsEvery) {
            TickActionsCurrentTimer = 0f;
            this.InputLocked = true;
            AStarPathfindAroundWalls Pathfinder = new AStarPathfindAroundWalls(Player.transform.position, new Vector3(1f, 1f, 1f));
            AStarPathfind.Node InitalPosition = new AStarPathfind.Node();
            InitalPosition.position = this.transform.position;
            AStarPathfind.Node Target = Pathfinder.FindPath(InitalPosition);
            while (Target.parent.parent != null){
                Target = Target.parent;
            }
            this.target = Target.position;
            LayerMask Mask = LayerMask.GetMask("Player");
            RaycastHit hit = new RaycastHit();
            Physics.Linecast(
                this.transform.position,
                this.target,
                out hit,
                Mask
                );
            if (hit.transform != null)
            {
                this.Attack(Player);
            }
            else
            {
                this.StartCoroutine(this.Move());
            }
        }
    }

    protected void Attack(Unit u) {
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        a.Play("BatAttack");
        u.TakeDamage(2);
        this.InputLocked = false;
    }

    override public void TakeDamage(float dmg) {
        this.Hp -= dmg;
    }
}
