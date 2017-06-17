using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hand : MonoBehaviour
{

    private GameObject HeldThing;

    public void SetHeld(GameObject t) {
        this.HeldThing = t;
        
    }

    public GameObject GetHeld() {
        return this.HeldThing;
    }

    public bool IsHoldingThing() {
        if (HeldThing != null) {
            return true;
        }
        return false;
    }

    public GameObject GetHeldAndRemoveAsChild() {
        this.transform.DetachChildren();
        GameObject t = this.HeldThing;
        this.HeldThing = null;
        return t;
    }

    void Update()
    {
        RectTransform rectTransform = this.GetComponent<RectTransform>() as RectTransform;
        rectTransform.position = Input.mousePosition;
        if (HeldThing != null) {
            if (HeldThing.transform.parent != this.transform){
                HeldThing.transform.SetParent(this.transform);
                (HeldThing.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }


}
