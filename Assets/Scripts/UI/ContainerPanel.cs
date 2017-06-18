using UnityEngine;
using System.Collections;

public abstract class ContainerPanel : MonoBehaviour {
    public void MakeVisible(bool visible) {
        this.gameObject.SetActive(visible);
    }
}
