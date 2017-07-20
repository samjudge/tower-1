using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : Switch {

    public override void SwitchOff() {
        this.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y - 0.04f,
            this.transform.position.z
        );
    }

    public override void SwitchOn() {
        this.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + 0.04f,
            this.transform.position.z
        );
    }

    void OnTriggerEnter(Collider other) {
        Player p = other.GetComponent<Player>() as Player;
        if (p != null) {
            PerformInteractions();
        }
    }

}
