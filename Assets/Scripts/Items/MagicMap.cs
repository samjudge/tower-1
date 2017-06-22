using UnityEngine.UI;
using UnityEngine;

public class MagicMap : Item {

    public Hand Hand;
    public MapContainer Map;

    void Update() {
        if (this.Hand.IsHoldingThing(this.transform.gameObject)) {
            Map.gameObject.SetActive(true);
        } else {
            Map.gameObject.SetActive(false);
        }
    }

}
