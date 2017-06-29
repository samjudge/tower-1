using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public ItemFactory ItemFactory;

    [SerializeField]
    private string Name;
    [SerializeField]
    private Hand Hand;

    public Hand GetHand() {
        return this.Hand;
    }

    public void SetHand(Hand Hand) {
        this.Hand = Hand;
    }

    public string GetName() {
        return this.Name;
    }

    public void SetName(string Name) {
        this.Name = Name;
    }

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
                p.GetActionLog().WriteNewLine("you pick up the " + Name + ".");
                Item i = this.CreateItem();
                this.Hand.SetHeld(i.gameObject);
                MonoBehaviour.Destroy(this.gameObject);
            } else {
                p.GetActionLog().WriteNewLine("Your hand is full!");
            }
        }
    }

    virtual protected Item CreateItem() {
        Item i = ItemFactory.MakeItem(Name);
        return i;
    }
}
