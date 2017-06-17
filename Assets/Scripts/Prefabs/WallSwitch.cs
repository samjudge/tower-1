using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviour {

    public Player Player;
    public Door[] OpensDoors;
    public bool IsSwitchedOn = false;
    public bool OneWay = false;

    void OnMouseDown() {
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2) {
            foreach (Openable O in OpensDoors) {
                if (!IsSwitchedOn) {
                    if (O.CanOpen()) {
                        O.Open();
                    }
                } else if(OneWay == false) {
                    if (O.CanClose()) {
                        O.Close();
                    }
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
