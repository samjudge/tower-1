using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public string Contains;
    public PickupFactory PickupFactory;
    public Player Player;
    bool isOpended = false;

    void OnMouseDown(){
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1){
            if (!isOpended){
                isOpended = !isOpended;
                StartCoroutine(Open());
            }
        }
    }

    private IEnumerator Open() {
        Player.ActionLog.WriteNewLine("you open the chest");
        Pickup p = this.PickupFactory.MakePickup(Contains);
        p.transform.position = this.transform.position;
        MonoBehaviour.Destroy(this.gameObject); //destroy this chest
        yield return null;
    }

    void OnTriggerEnter(Collider other) {
        //check if player
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null)
        {
            //set flag to cancel movement and return
            p.SetCancelMovementFlag(true);
        }
    }
}
