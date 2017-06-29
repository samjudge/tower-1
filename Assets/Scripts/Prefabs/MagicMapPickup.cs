using UnityEngine;
using System.Collections;

public class MagicMapPickup : Pickup {

    [SerializeField]
    private MapContainer MapContainer;

    override protected Item CreateItem() {
        MagicMap i = this.ItemFactory.MakeItem(this.GetName()) as MagicMap;
        i.SetHand(this.GetHand());
        i.SetMapContainer(this.MapContainer);
        return i;
    }
}
