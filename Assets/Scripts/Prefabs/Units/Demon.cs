﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Unit {

    [SerializeField]
    private Player Player;
    [SerializeField]
    private Weapon Fist;
    [SerializeField]
    private SpellProjectile FireSpell;
    [SerializeField]
    private float TickActionsEvery = 1f;
    [SerializeField]
    private float TickActionsCurrentTimer = 0;
    [SerializeField]
    private bool DeadFlag = false;

    void Start() {
        if (this.Player == null) {
            this.Player = GameObject.Find("Player").GetComponent<Player>() as Player;
        }
        this.Hp = CalculateMaxHp();
        this.Mp = CalculateMaxMp();
        this.Equipment = new EquipmentModel(new EquippedEntity[] { new EquippedEntity("Left", Fist) }, this);
    }

    private IEnumerator FireLazer() {
        LayerMask ShootingMask = LayerMask.GetMask("Player", "Floor", "Walls");
        RaycastHit ShootHit = new RaycastHit();
        Physics.Linecast(
            this.transform.position,
            Player.transform.position,
            out ShootHit,
            ShootingMask
        );
        if (ShootHit.transform != null) {
            Player p = ShootHit.transform.gameObject.GetComponent<Player>() as Player;
            if (p != null) {
                float totalArc = 180f;
                float angle = 0f;
                float totalDuration = 2.4f;
                float minWait = 0.02f;
                float curWait = 0f;
                Quaternion fireAt = Quaternion.LookRotation(
                    (this.transform.position - Player.transform.position).normalized
                );
                while (angle < totalArc) {
                    while (curWait < minWait) {
                        curWait += Time.deltaTime;
                        yield return null;
                    }
                    angle += (curWait * totalArc)/totalDuration;
                    curWait = 0;
                    SpellProjectile s = Instantiate(
                        FireSpell,
                        new Vector3(
                            this.transform.position.x,
                            this.transform.position.y + 0.3f,
                            this.transform.position.z
                        ),
                        Quaternion.Euler(
                            fireAt.eulerAngles.x,
                            fireAt.eulerAngles.y + angle,
                            fireAt.eulerAngles.z - 5
                        )
                    ) as SpellProjectile;
                    s.SetDirection(Quaternion.Euler(
                        fireAt.eulerAngles.x,
                        fireAt.eulerAngles.y + angle,
                        fireAt.eulerAngles.z - 5
                    ));
                    s.SetCaster(this.gameObject);
                    yield return null;
                }
            }
            //wait until 
            float coolDownMinWait = 0.5f;
            float curCoolDownWait = 0f;
            while (curCoolDownWait < coolDownMinWait) {
                curCoolDownWait += Time.deltaTime;
                yield return null;
            }
        }
        this.InputLocked = false;
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

        if (this.IsFrozen()) {
            return;
        }

        float TickRequired = TickActionsEvery;

        if (this.IsEnraged()) {
            TickRequired = 0.33f;
        }
        this.TickActionsCurrentTimer += Time.deltaTime;
        if (!InputLocked && TickActionsCurrentTimer > TickRequired) {
            TickActionsCurrentTimer = 0f;
            //if close by, then pathfind
            if ((this.transform.position - Player.transform.position).sqrMagnitude < 64f) {
                this.InputLocked = true;
                if ((this.transform.position - Player.transform.position).sqrMagnitude > 4f) {
                    System.Random r = new System.Random();
                    int n = r.Next(0, 100);
                    if (n > 25) {
                        StartCoroutine(FireLazer());
                        return;
                    }
                }
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
                        this.Attack(Player);
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
        Debug.Log((this.transform.position - Player.transform.position).sqrMagnitude);
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
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        Weapon w = this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon;
        u.TakeDamage(w.RollDice()+2);
        this.InputLocked = false;
    }

    override public void TakeDamage(float dmg) {
        if (DeadFlag) {
            return;
        }
        Animator a = this.GetComponentInChildren<Animator>() as Animator;
        this.Hp -= dmg;
    }
}
