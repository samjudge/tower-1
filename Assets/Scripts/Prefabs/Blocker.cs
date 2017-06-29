using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {

    [SerializeField]
    private bool BlocksSpells = true;

	void OnTriggerEnter(Collider other){
		//check if player
		Player p = other.gameObject.GetComponent<Player>() as Player;
		if(p != null){
            //set flag to cancel movement and return
            p.GetActionLog().WriteNewLine("Oof!");
			p.SetCancelMovementFlag(true);
		}
        SpellProjectile s = other.gameObject.GetComponent<SpellProjectile>() as SpellProjectile;
        if (s != null && BlocksSpells == true) {
            MonoBehaviour.Destroy(s.gameObject);
        }
    }

}
