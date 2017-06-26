using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsTo : MonoBehaviour {

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
