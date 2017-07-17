﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGrork : Unit {

    public Player Player;
    public Weapon Fist;
    public float TickActionsEvery = 1f;
    private float TickActionsCurrentTimer = 0;
    public float CastAuraSpellEvery = 5f;
    private float CastAuraSpellCurrentTimer = 0;
    private bool DeadFlag = false;

    void Start() {
        this.Equipment = new EquipmentModel(new EquippedEntity[] { new EquippedEntity("Left", Fist) }, this);
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

        this.TickActionsCurrentTimer += Time.deltaTime;
        this.CastAuraSpellCurrentTimer += Time.deltaTime;
        if (!InputLocked && TickActionsCurrentTimer > TickActionsEvery) {
            TickActionsCurrentTimer = 0f;
            if (CastAuraSpellCurrentTimer > CastAuraSpellEvery) {
                CastAuraSpellCurrentTimer = 0f;
                int mask = LayerMask.GetMask("Enemies");
                Collider[] hits = Physics.OverlapSphere(this.transform.position, 4f, mask);
                for (int x = 0; x < hits.Length; x++) {
                    Collider c = hits[x];
                    Skeleton ally = c.gameObject.GetComponent<Skeleton>() as Skeleton;
                    if (ally != null) {
                        ally.Enrage(4f);
                    }
                }
                Animator a = this.GetComponentInChildren<Animator>() as Animator;
                a.Play("Shout");
                return;
            }
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