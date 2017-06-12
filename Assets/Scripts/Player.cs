using UnityEngine;
using System.Collections;

public class Player : Unit {

	public Camera PlayerCam;
	public ActionLog ActionLog;
	public Flasher RedFlasher;
	public UIFillBar HPBar;
	public GameOverScreen GameOverScreen;


	
	void Start () {
		this.Hp = CalculateMaxHp();
		this.Mp = CalculateMaxMp();
		StartCoroutine(CameraFollow());
	}

	void Update () {
		//check state
		if(IsDead()){
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
		this.ActionLog.WriteNewLine(this.GetRandomOuchString() + "! " + "You take "+ damage + " damage!");
	}

}
