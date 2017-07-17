using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class PickupFactory : MonoBehaviour {

    public ItemFactory ItemFactory;
    public Hand Hand;

    //Weapons
    public Pickup Sword;
    public Pickup Mace;

    //Consumables
    public Pickup HPPotion;
    public Pickup MPPotion;

    //Items
    public Pickup Level1Key;
    public Pickup Level3Key;
    public Pickup Level3Key2;
    public Pickup Map;

    public Pickup MakePickup(string name) {
        FieldInfo Property = this.GetType().GetField(name);
        Pickup PickupPrefab = Property.GetValue(this) as Pickup;
        Pickup nPickup = Instantiate(PickupPrefab, new Vector3(0, 0, 0), Quaternion.Euler(15, 180, 0)) as Pickup;
        nPickup.SetName(name);
        nPickup.ItemFactory = ItemFactory;
        nPickup.SetHand(Hand);
        return nPickup;
    }
}
