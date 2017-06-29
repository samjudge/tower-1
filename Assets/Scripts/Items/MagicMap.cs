using UnityEngine.UI;
using UnityEngine;

public class MagicMap : Item {

    [SerializeField]
    private Hand Hand;

    [SerializeField]
    private MapContainer Map;

    void Update() {
        if (Hand.IsHoldingThing(this.transform.gameObject)) {
            Map.gameObject.SetActive(true);
        } else {
            Map.gameObject.SetActive(false);
        }
    }

}
