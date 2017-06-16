using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

    public void DestroyParent() {
        MonoBehaviour.Destroy(this.transform.parent.gameObject); //destroy on death
    }
}
