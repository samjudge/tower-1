using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {

    protected GameObject Item;
    public Hand Hand;

    public void SetItemAndMakeChild(GameObject o) {
        this.SetItem(o);
        if (o != null) {
            o.transform.SetParent(this.transform);
            (o.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }
    
    public GameObject GetItemAndDetatch() {
        this.transform.DetachChildren();
        GameObject t = this.Item;
        this.Item = null;
        return t;
    }

    public void SetItem(GameObject o) {
        this.Item = o;
    }

    public Item GetItem() {
        return this.Item.GetComponent<Item>() as Item;
    }

    virtual public void OnPointerClick(PointerEventData e){
        //swap hand with slot
        GameObject held = this.Hand.GetHeldAndRemoveAsChild();
        GameObject was = this.GetItemAndDetatch();
        this.Hand.SetHeld(was);
        this.SetItemAndMakeChild(held);
    }
}
