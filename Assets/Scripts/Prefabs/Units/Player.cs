using UnityEngine;
using System.Collections;
using System;

public class Player : Unit {

    [SerializeField]
    private Camera PlayerCam;
    [SerializeField]
    private ActionLog ActionLog;
    [SerializeField]
    private Flasher RedFlasher;
    [SerializeField]
    private UIFillBar HPBar;
    [SerializeField]
    private UIFillBar MPBar;
    [SerializeField]
    private UIFillBar AttackCDBar;
    [SerializeField]
    private GameOverScreen GameOverScreen;
    [SerializeField]
    private Weapon Fist;
    [SerializeField]
    public Equipment Nothing;


    public UIFillBar GetMPBar() {
        return MPBar;
    }

    public UIFillBar GetHPBar() {
        return HPBar;
    }

    public UIFillBar GetAttackCDBar() {
        return AttackCDBar;
    }

    public ActionLog GetActionLog() {
        return this.ActionLog;
    }


    void Start () {
		this.Hp = CalculateMaxHp();
		this.Mp = CalculateMaxMp();
        this.Equipment = new EquipmentModel(
            new EquippedEntity[] {
                new EquippedEntity("Left", Fist),
                new EquippedEntity("Right", Fist),
                new EquippedEntity("Head", Nothing),
                new EquippedEntity("Body", Nothing),
                new EquippedEntity("Feet", Nothing),
            },
            this
        );
        this.NextAttackTimerMin = (this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon).GetWeight()+1;
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
            this.NextAttackTimerMin = (this.Equipment.Get("Left").GetComponent<Weapon>() as Weapon).GetWeight()+1;
        }
    }


}
