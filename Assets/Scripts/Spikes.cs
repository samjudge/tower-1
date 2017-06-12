using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

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
			//deal damage to player
			p.TakeDamage(5f);
		}
	}
}
