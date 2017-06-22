using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string Name;
    public ItemFactory ItemFactory;
    public Hand Hand;

	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(
            this.transform.rotation.eulerAngles.x,
            this.transform.rotation.eulerAngles.y + Time.deltaTime*30,
            this.transform.rotation.eulerAngles.z
            );
	}

    private void OnTriggerEnter(Collider other) {
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null) {
            //pick up item and place in inventory
            if (!Hand.IsHoldingThing()) {
                p.ActionLog.WriteNewLine("you pick up the " + Name + ".");
                Item i = this.CreateItem();
                this.Hand.SetHeld(i.gameObject);
                MonoBehaviour.Destroy(this.gameObject);
            } else {
                p.ActionLog.WriteNewLine("Your hand is full!");
            }
        }
    }

    virtual protected Item CreateItem() {
        Item i = ItemFactory.MakeItem(Name);
        return i;
    }
}
