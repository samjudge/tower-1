using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HideOnToggleTimed : HideOnToggle {

    [SerializeField]
    private float Timer;
    
    override public void Toggle() {
        if (GetActive()) {
            SetActive(!GetActive());
            foreach(MeshRenderer r in this.GetComponentsInChildren<MeshRenderer>()){
                r.enabled = GetActive();
            }
            StartCoroutine(Cooldown());
        }
    }

    private float InternalClock = 0f;

    private IEnumerator Cooldown() {
        while (this.InternalClock < Timer) {
            InternalClock += Time.deltaTime;
            yield return null;
        }
        InternalClock = 0;
        SetActive(!GetActive());
        
        foreach (MeshRenderer r in this.GetComponentsInChildren<MeshRenderer>()) {
            r.enabled = GetActive();
        }
        yield return null;
    }
}
