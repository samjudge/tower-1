using System.Collections;
using UnityEngine;

public class Bomba : Unit{

    public Killable child;
    public Player Player;
    public Weapon Fangs;
    public float TickActionsEvery = 2f;
    private float TickActionsCurrentTimer = 0;
    private bool DeadFlag = false;
    [SerializeField]
    private float explodeDamage = 20f;
    void Start() {
        if (this.Player == null) {
            this.Player = GameObject.Find("Player").GetComponent<Player>() as Player;
        }
        this.Equipment = new EquipmentModel(new EquippedEntity[] {},this);
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

        if (this.IsFrozen()) {
            return;
        }

        if (IsDead()) {
            BeginExplode();
            return;
        }

        this.TickActionsCurrentTimer += Time.deltaTime;
        if(!InputLocked && TickActionsCurrentTimer > TickActionsEvery) {
            TickActionsCurrentTimer = 0f;
            //if close by, then pathfind
            if ((this.transform.position - Player.transform.position).sqrMagnitude < 32f) {
                this.InputLocked = true;
                AStarPathfindAroundWalls Pathfinder = new AStarPathfindAroundWalls(Player.transform.position, new Vector3(1f, 1f, 1f));
                AStarPathfind.Node InitalPosition = new AStarPathfind.Node();
                InitalPosition.position = this.transform.position;
                AStarPathfind.Node Target = Pathfinder.FindPath(InitalPosition);
                while (Target.parent.parent != null) {
                    Target = Target.parent;
                }
                this.target = Target.position;
                LayerMask Mask = LayerMask.GetMask("Player", "Floor", "Walls");
                RaycastHit hit = new RaycastHit();
                Physics.Linecast(
                    this.transform.position,
                    this.target,
                    out hit,
                    Mask
                    );
                if (hit.transform != null) {
                    Player p = hit.transform.gameObject.GetComponent<Player>() as Player;
                    if (p != null) {
                        BeginExplode();
                    }
                } else {
                    this.StartCoroutine(this.Move(3f));
                }
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

    private void BeginExplode() {
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        this.DeadFlag = true;
        a.Play("Hiss");
        StartCoroutine(Explode());
    }

    override public void Attack(Unit u) {
    }

    public IEnumerator Explode() {
        while (child.IsTriggerSet() == false) {
            yield return null;
        }
        Collider[] hits = Physics.OverlapSphere(transform.position, 16f);
        foreach (Collider hit in hits) {
            Player p = hit.transform.GetComponent<Player>();
            if (p != null) {
                p.TakeDamage(explodeDamage);
            }
        }
        child.DestroyParent();
        yield return null;
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
