using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Unit {
    
	public Camera PlayerCam;
	public ActionLog ActionLog;
	public Flasher RedFlasher;
	public UIFillBar HPBar;
    public UIFillBar MPBar;
    public UIFillBar AttackCDBar;
    public GameOverScreen GameOverScreen;

    public Weapon Fist;
    public Equipment Nothing;

    void Start () {
		this.Hp = CalculateMaxHp();
		this.Mp = CalculateMaxMp();
        this.Equipment = new EquipmentSlots(
            new Equipped[] {
                new Equipped("Left", Fist),
                new Equipped("Right", Fist),
                new Equipped("Head", Nothing),
                new Equipped("Body", Nothing),
                new Equipped("Feet", Nothing),
            },
            this
        );
        this.NextAttackTimerMin = (this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon).Weight+1;
        ResetCamera();
    }

    public void ResetCamera() {
        this.StopAllCoroutines();
        InputLocked = false;
        StartCoroutine(CameraFollow());
    }

    public float AttackTimer = 0f;
    public float NextAttackTimerMin = 0f;

    void Update () {
        //check state
        AttackTimer += Time.deltaTime;
        if (IsDead()){
			GameOverScreen.MakeVisible(true);
			return;
		}
		//inputs
		if(Input.GetKey(KeyCode.W)){
			this.ShiftPosition(1,0);
		}
		if(Input.GetKey(KeyCode.A)){
			this.ShiftPosition(0,-1);
		}
		if(Input.GetKey(KeyCode.S)){
			this.ShiftPosition(-1,0);
		}
		if(Input.GetKey(KeyCode.D)){
			this.ShiftPosition(0,1);
		}
		if(Input.GetKey(KeyCode.Q)){
			this.RotateBy(-90);
		}
		if(Input.GetKey(KeyCode.E)){
			this.RotateBy(90);
		}
        AttackCDBar.UpdateBar(this.AttackTimer, NextAttackTimerMin);

    }

	/** 
 	 * A coroutine to force the main camera to share the same position as this Player object
 	 */
	private IEnumerator CameraFollow(){
		while(true){
			PlayerCam.transform.position = this.transform.position;
			PlayerCam.transform.rotation = this.transform.rotation;
			yield return null;
		}
	}

	private string GetRandomOuchString(){
		System.Random r = new System.Random();
		int roll = r.Next(12);
		string OuchieString = "?";
		if(roll < 3){
			OuchieString = "ouch";
		} else if (roll < 6){
			OuchieString = "baam";
		} else if (roll < 9){
			OuchieString = "tch";
		} else {
			OuchieString = "ooof";
		}
		return OuchieString;
	}

	override public void TakeDamage(float damage){
		this.RedFlasher.Flash();
		this.Hp -= damage;
		HPBar.UpdateBar(this.Hp,this.CalculateMaxHp());
		this.ActionLog.WriteNewLine(this.GetRandomOuchString() + "! " + "you take "+ damage + " damage!");
	}

    override public void Attack(Unit u){
        if (AttackTimer >= NextAttackTimerMin) {
            AttackTimer = 0;
            int roll = (Equipment.Get("Left").GetComponent<Weapon>() as Weapon).RollDice();
            this.ActionLog.WriteNewLine("you smack the enemy for " + roll + "!");
            u.TakeDamage(roll);
            this.NextAttackTimerMin = (this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon).Weight+1;
        }
    }


}
