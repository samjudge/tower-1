using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string Name;
    public ItemFactory ItemFactory;
    public Hand Hand;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.rotation = Quaternion.Euler(
            this.transform.rotation.eulerAngles.x,
            this.transform.rotation.eulerAngles.y + Time.deltaTime*30,
            this.transform.rotation.eulerAngles.z
            );
	}

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null)
        {
            //pick up item and place in inventory
            p.ActionLog.WriteNewLine("you pick up the "+Name+".");
            Item i = ItemFactory.MakeItem(Name);
            this.Hand.SetHeld(i.gameObject);
            MonoBehaviour.Destroy(this.gameObject);
        }
    }
}
