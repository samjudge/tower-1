using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public Player Player;
    public GameObject Tilemap;
    public MapFactory MapFactory;
    public int OffsetZ;
    private ArrayList MappedRefs;
    public Transform UITarget;

    void Start() {
        MappedRefs = new ArrayList();
        PlayerTile = MapFactory.MakeMapTile("PlayerTile");
        PlayerTile.transform.SetParent(UITarget.transform);
        StartCoroutine(UpdateMap());
    }
    
    void Update() {
    }

    private GameObject PlayerTile;

    private float TickEvery = 1f;
    private float Current = 0f;

    public IEnumerator UpdateMap () {
        while (true) {
            Current += Time.deltaTime;
            if (Current > TickEvery) {
                Current = 0f;
                int ChildCount = Tilemap.transform.childCount;
                //candidate for BSP tree collision check
                Collider[] HitColliders = Physics.OverlapSphere(Player.transform.position, 8);
                for (int x = HitColliders.Length - 1; x >= 0; x--) {
                    Transform Child = HitColliders[x].transform;
                    if (!MappedRefs.Contains(Child)) {
                        DrawTransformToMap(Child);

                    }
                }
            }
            //update player position
            PlayerTile.GetComponent<Image>().rectTransform.localPosition = new Vector3(
                Player.transform.position.x * 5,
                (Player.transform.position.z + OffsetZ) * 5,
                0
            );
            PlayerTile.transform.SetAsLastSibling();
            yield return null;
        }
	}

    private void DrawTransformToMap(Transform Child) {
        GameObject GameObject = Child.gameObject;
        Blocker Wall = GameObject.GetComponent<Blocker>();
        if (Wall != null) {
            for (int x = 0; x < Child.childCount; x++) {
                Transform Subchild = Child.GetChild(x);
                LayerMask Mask = LayerMask.GetMask("Walls");
                RaycastHit hit = new RaycastHit();
                Physics.Linecast(Subchild.position, Player.transform.position, out hit, Mask);
                if (hit.transform != null) {
                    //something blocking view
                } else {
                    GameObject i = MapFactory.MakeMapTile("WallTile");
                    i.transform.SetParent(UITarget.transform);
                    i.GetComponent<Image>().rectTransform.localPosition = new Vector3(
                        GameObject.transform.position.x * 5,
                        (GameObject.transform.position.z + OffsetZ) * 5,
                        0
                    );
                    MappedRefs.Add(Child);
                }
            }
        }
        Floor Floor = GameObject.GetComponent<Floor>();
        if (Floor != null) {
            LayerMask Mask = LayerMask.GetMask("Walls");
            RaycastHit hit = new RaycastHit();
            Physics.Linecast(Floor.transform.position, Player.transform.position, out hit, Mask);
            if (hit.transform != null) {
                //something blocking view
            } else {
                GameObject i = MapFactory.MakeMapTile("FloorTile");
                i.transform.SetParent(UITarget.transform);
                i.GetComponent<Image>().rectTransform.localPosition = new Vector3(
                    GameObject.transform.position.x * 5,
                    (GameObject.transform.position.z + OffsetZ) * 5,
                    0
                );
                MappedRefs.Add(Child);
            }
        }
    }

    public void ResetMap() {
        StopAllCoroutines();
        this.MappedRefs.Clear();
        for (int x = 0; x < UITarget.transform.childCount; x++) {
            Transform m = UITarget.transform.GetChild(x);
            if (m == PlayerTile.transform) {
                continue;
            }
            Destroy(m.gameObject);
        }
        UITarget.transform.DetachChildren();
        PlayerTile = MapFactory.MakeMapTile("PlayerTile");
        PlayerTile.transform.SetParent(UITarget.transform);
        PlayerTile.transform.SetAsLastSibling();
        StartCoroutine(UpdateMap());
    }
}
