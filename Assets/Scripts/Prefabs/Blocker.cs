using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		//check if player
		Player p = other.gameObject.GetComponent<Player>() as Player;
		if(p != null){
			//set flag to cancel movement and return
			p.SetCancelMovementFlag(true);
		}
	}

}
