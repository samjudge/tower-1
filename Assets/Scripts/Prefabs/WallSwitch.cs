using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : Switch {

    public override void SwitchOff() {
        this.transform.rotation = Quaternion.Euler(new Vector3(-270f, this.transform.rotation.y, this.transform.rotation.z));
    }

    public override void SwitchOn() {
        this.transform.rotation = Quaternion.Euler(new Vector3(-90f, this.transform.rotation.y, this.transform.rotation.z));
    }

    void OnMouseDown() {
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2) {
            PerformInteractions();
        }
    }
                
}
