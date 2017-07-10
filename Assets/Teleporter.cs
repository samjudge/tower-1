using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public Vector3 NewPosition;
    public Player Player;
    public Map Map;

    void OnTriggerEnter(Collider o) {
        if (o.gameObject.GetComponent<Player>() != null) {
            Player.StopAllCoroutines();
            Player.transform.position = NewPosition;
            Player.ResetCamera();
            Map.ResetMap();
        }
    }
}
