using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public PickupFactory PickupFactory;

    [SerializeField]
    private string Contains;
    [SerializeField]
    private Player Player;
    [SerializeField]
    private bool isOpended = false;

    void OnMouseDown(){
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2){
            if (!isOpended){
                isOpended = !isOpended;
                StartCoroutine(Open());
            }
        }
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

    private IEnumerator Open() {
        Player.GetActionLog().WriteNewLine("you open the chest.");
        Pickup p = this.PickupFactory.MakePickup(Contains);
        p.transform.position = this.transform.position;
        MonoBehaviour.Destroy(this.gameObject); //destroy this chest
        yield return null;
    }

}
