using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightSwitch : Switch {

    [SerializeField]
    private Vector3 accel;

    [SerializeField]
    private float speed = 0.03f;

    private void Update() {
        if (this.isOn) {
            Destroy(this.gameObject);
        }
        this.transform.position = Vector3.Lerp(this.transform.position,
            this.transform.position + accel,
            speed
        );
    }

    public override void SwitchOff() {
        //stub
    }

    bool isOn = false;

    public override void SwitchOn() {
        this.isOn = true;
        PerformInteractions();
    }

    private void OnTriggerEnter(Collider other) {
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null) {
            SwitchOn();
        }
        Blocker b = other.gameObject.GetComponent<Blocker>() as Blocker;
        if (b != null) {
            this.accel = -this.accel;
        }
        
    }
}
