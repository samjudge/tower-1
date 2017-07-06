using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

	void OnTriggerEnter(Collider other){
        HideOnToggle Toggle = this.GetComponent<HideOnToggle>();
        if (Toggle != null) {
            if (!Toggle.GetActive()) {
                return;
            }
        }
        //check if player
        Player p = other.gameObject.GetComponent<Player>() as Player;
		if(p != null){
			//set flag to cancel movement and return
			//deal damage to player
			p.TakeDamage(5f);
		}
	}
}
