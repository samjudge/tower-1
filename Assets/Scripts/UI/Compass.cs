using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Compass : MonoBehaviour {

    [SerializeField]
	private Player Player;

	void Update () {
		Text Text = this.GetComponent<Text>();
		float angle = Player.transform.rotation.eulerAngles.y;
		if((int)angle == 0){
			Text.text = "N";
		} else if((int)angle == 90 || (int)angle == -270){
			Text.text = "E";
		} else if((int)angle == 180 || (int)angle == -180){
			Text.text = "S";
		} else if((int)angle == -90 || (int)angle == 270){
			Text.text = "W";
		}
	}
}
