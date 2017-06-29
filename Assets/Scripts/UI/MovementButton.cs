using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MovementButton : MonoBehaviour, IPointerClickHandler{

    [SerializeField]
	private Player Player;
    [SerializeField]
    private float xMotion;
    [SerializeField]
    private float yMotion;
    [SerializeField]
    private float rotation;

    /**
     * OnPointerClick(PointerEventData e)
     * @param PointerEventData e - 
     * Move in the direction/rotation specified in the object's fields
     */
    public void OnPointerClick(PointerEventData e){
		if(this.xMotion != 0 || this.yMotion != 0){
			Player.ShiftPosition(this.xMotion, this.yMotion);
		} else if (this.rotation != 0){
			Player.RotateBy(rotation);
		}
	}
}
