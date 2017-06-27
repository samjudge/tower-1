using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit {

    public Player Player;
    public Weapon Fist;
    public float TickActionsEvery = 1f;
    private float TickActionsCurrentTimer = 0;
    private bool DeadFlag = false;

    void Start() {
        this.Equipment = new EquipmentSlots(new Equipped[] { new Equipped("Left", Fist) }, this);
    }

    void Update() {
        //make this guy face towards player
        this.transform.rotation = Quaternion.Euler(
            new Vector3(
                Player.transform.rotation.eulerAngles.x,
                Player.transform.rotation.eulerAngles.y + 180,
                Player.transform.rotation.eulerAngles.z
            )
        );
        if (DeadFlag) {
            return;
        }
        if (IsDead()) {
            Animator a = this.GetComponentInChildren<Animator>() as Animator;
            DeadFlag = true;
            a.Play("SwirlyDeath");
            return;
        }
        this.TickActionsCurrentTimer += Time.deltaTime;
        if(!InputLocked && TickActionsCurrentTimer > TickActionsEvery) {
            TickActionsCurrentTimer = 0f;
            this.InputLocked = true;
            if ((Player.transform.position - this.transform.position).sqrMagnitude < 12f) {
                AStarPathfindAroundWalls Pathfinder = new AStarPathfindAroundWalls(Player.transform.position, new Vector3(1f, 1f, 1f));
                AStarPathfind.Node InitalPosition = new AStarPathfind.Node();
                InitalPosition.position = this.transform.position;
                AStarPathfind.Node Target = Pathfinder.FindPath(InitalPosition);
                while (Target.parent.parent != null) {
                    Target = Target.parent;
                }
                this.target = Target.position;
            }
            LayerMask Mask = LayerMask.GetMask("Player", "Floor", "Walls");
            RaycastHit hit = new RaycastHit();
            Physics.Linecast(
                this.transform.position,
                this.target,
                out hit,
                Mask
            );
            Player p = hit.transform.gameObject.GetComponent<Player>() as Player;
            if (p != null){
                this.Attack(Player);
            } else {
                this.StartCoroutine(this.Move());
            }
        }
    }

    private void OnMouseOver(){
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1){
            //TODO : make the mouse pointer a sword on hover
        }
    }

    private void OnMouseExit(){
        //TODO : make the mouse pointer normal again
    }

    void OnMouseDown(){
        if (DeadFlag) {
            return;
        }
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.1) {
            Player.Attack(this);
        }
        
    }

    override public void Attack(Unit u) {
        if (DeadFlag) {
            return;
        }
        Debug.Log(this.Equipment.Get("Left").Owner);
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        a.Play("BatAttack");
        Weapon w = this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon;
        u.TakeDamage(w.RollDice()+2);
        this.InputLocked = false;
    }

    override public void TakeDamage(float dmg) {
        if (DeadFlag) {
            return;
        }
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        a.Play("TakeDamage");
        this.Hp -= dmg;
    }
}
