using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Unit {
    
	public Camera PlayerCam;
	public ActionLog ActionLog;
	public Flasher RedFlasher;
	public UIFillBar HPBar;
	public GameOverScreen GameOverScreen;

    public Dictionary<string, Item> Equipment;

	void Start () {
		this.Hp = CalculateMaxHp();
		this.Mp = CalculateMaxMp();
        this.Equipment = new Dictionary<string, Item>();
        this.Equipment.Add("Left",null);
        this.Equipment.Add("Right", null);
        this.Equipment.Add("Head", null);
        this.Equipment.Add("Body", null);
        this.Equipment.Add("Foot", null);
        StartCoroutine(CameraFollow());
	}

    public float AttackTimer = 0f;

	void Update () {
        //check state
        AttackTimer += Time.deltaTime;
        if (IsDead()){
			GameOverScreen.MakeVisible(true);
			return;
		}
		//inputs
		if(Input.GetKeyDown(KeyCode.W)){
			this.ShiftPosition(1,0);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			this.ShiftPosition(0,-1);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			this.ShiftPosition(-1,0);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			this.ShiftPosition(0,1);
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			this.RotateBy(-90);
		}
		if(Input.GetKeyDown(KeyCode.E)){
			this.RotateBy(90);
		}
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
        
        Animator a = u.GetComponentInChildren<Animator>() as Animator;
        a.Play("TakeDamage");
        int roll = this.EquippedWeapon.RollDice();
        this.ActionLog.WriteNewLine("you smack the enemy for " + roll +"!");
        u.TakeDamage(roll);
        this.InputLocked = false;
    }


}
