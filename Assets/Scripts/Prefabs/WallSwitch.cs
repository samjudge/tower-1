using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : Switch {

    public override void SwitchOff() {
        //this.transform.Rotate(new Vector3(180f, 0, 0));
    }

    public override void SwitchOn() {
        //this.transform.Rotate(new Vector3(180f, 0, 0));
    }

    void OnMouseDown() {
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2) {
            PerformInteractions();
        }
    }
                
}
