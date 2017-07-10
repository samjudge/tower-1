using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour, MonoTrigger {

    public void DestroyParent() {
        MonoBehaviour.Destroy(this.transform.parent.gameObject); //destroy on death
    }

    private bool ReadyToBlowUp = false;

    public bool IsTriggerSet() {
        return ReadyToBlowUp;
    }

    public void SetTrigger() {
        ReadyToBlowUp = true;
    }
}
