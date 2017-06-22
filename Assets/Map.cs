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

    void Start() {
        MappedRefs = new ArrayList();
        StartCoroutine(UpdateMap());
    }
    
    void Update() {
    }

    public IEnumerator UpdateMap () {
        while (true) {
            int ChildCount = Tilemap.transform.childCount;
            //candidate for BSP tree collision check
            for(int x = ChildCount-1; x >= 0; x--) {
                Transform Child = Tilemap.transform.GetChild(x);
                if ((Child.position - Player.transform.position).sqrMagnitude < 12) {
                    if (!MappedRefs.Contains(Child)) {
                        DrawTransformToMap(Child);
                        
                    } 
                }
            }
            yield return null;
        }
	}

    private void DrawTransformToMap(Transform Child) {
        GameObject GameObject = Child.gameObject;
        Blocker Wall = GameObject.GetComponent<Blocker>();
        if (Wall != null) {
            for (int x = 0; x < Child.childCount; x++) {
                Transform Subchild = Child.GetChild(x);
                Debug.Log(Subchild);
                LayerMask Mask = LayerMask.GetMask("Walls");
                RaycastHit hit = new RaycastHit();
                Physics.Linecast(Subchild.position, Player.transform.position, out hit, Mask);
                
                if (hit.transform != null) {
                    //something blocking view
                    //Debug.DrawLine(Player.transform.position, Subchild.position, Color.red, 5f);
                    //Debug.Log(hit.transform);
                } else {
                    Debug.DrawLine(Player.transform.position, Subchild.position, Color.red, 2f);
                    Debug.Log(hit.transform);
                    GameObject i = MapFactory.MakeMapTile("WallTile");
                    i.transform.SetParent(this.transform);
                    i.GetComponent<Image>().rectTransform.localPosition = new Vector3(
                        GameObject.transform.position.x * 5,
                        (GameObject.transform.position.z + OffsetZ) * 5,
                        0
                    );
                    MappedRefs.Add(Child);
                }
            }
        }
    }
}
