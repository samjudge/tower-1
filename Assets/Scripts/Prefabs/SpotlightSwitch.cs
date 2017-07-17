using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightSwitch : Switch {

    [SerializeField]
    private Vector3 accel;

    [SerializeField]
    private float speed = 0.03f;

    public override void Toggle() {
        Destroy(this.gameObject);
    }

    private void Update() {
        this.transform.position = Vector3.Lerp(this.transform.position,
            this.transform.position + accel,
            speed
        );
    }

    public override void SwitchOff() {
        //stub
    }

    public override void SwitchOn() {
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
