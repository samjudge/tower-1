using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//check if player
		Player p = other.gameObject.GetComponent<Player>() as Player;
		if(p != null){
			//set flag to cancel movement and return
			p.SetCancelMovementFlag(true);
		}
	}

}
