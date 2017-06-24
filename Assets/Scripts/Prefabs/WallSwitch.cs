using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviour {

    public Player Player;
    public GameObject[] Interactions;
    public bool IsSwitchedOn = false;
    public bool OneWay = false;

    void OnMouseDown() {
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2) {
            foreach (GameObject G in Interactions) {
                Openable O = G.GetComponent<Openable>() as Openable;
                if (O != null) {
                    if (!IsSwitchedOn) {
                        if (O.CanOpen()) {
                            O.Open();
                        }
                    } else if (OneWay == false) {
                        if (O.CanClose()) {
                            O.Close();
                        }
                    }
                }
                Destroyable D = G.GetComponent<Destroyable>() as Destroyable;
                if (D != null) {
                    D.Destroy();
                }
            }
            if (!IsSwitchedOn) {
                IsSwitchedOn = true;
            } else if (OneWay == false) {
                IsSwitchedOn = false;
            }

        }

    }
                
}
