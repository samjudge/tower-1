using UnityEngine.UI;
using UnityEngine;

public class MagicMap : Item {

    [SerializeField]
    private Hand Hand;

    [SerializeField]
    private MapContainer Map;

    public void SetHand(Hand h) {
        this.Hand = h;
    }

    public void SetMapContainer(MapContainer c) {
        this.Map = c;
    }

    void Update() {
        if (Hand.IsHoldingThing(this.transform.gameObject)) {
            Map.gameObject.SetActive(true);
        } else {
            Map.gameObject.SetActive(false);
        }
    }

}
