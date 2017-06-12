using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MovementButton : MonoBehaviour, IPointerClickHandler{

	public Player Player;
	public float xMotion;
	public float yMotion;
	public float rotation;


	public void OnPointerClick(PointerEventData e){
		if(this.xMotion != 0 || this.yMotion != 0){
			Player.ShiftPosition(this.xMotion, this.yMotion);
		} else if (this.rotation != 0){
			Player.RotateBy(rotation);
		}
	}
}
