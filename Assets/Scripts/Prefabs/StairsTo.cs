using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsTo : MonoBehaviour {

    public Vector3 NewPosition;
    public Player Player;
    public Map Map;

    void OnTriggerEnter(Collider o) {
        Player.StopAllCoroutines();
        Player.transform.position = NewPosition;
        Player.ResetCamera();
        Map.ResetMap();
    }
}
