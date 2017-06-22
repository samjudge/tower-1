using UnityEngine;
using System.Collections;

public class MagicMapPickup : Pickup {

    public MapContainer MapContainer;

    override protected Item CreateItem() {
        MagicMap i = this.ItemFactory.MakeItem(Name) as MagicMap;
        i.Hand = this.Hand;
        i.Map = this.MapContainer;
        return i;
    }
}
