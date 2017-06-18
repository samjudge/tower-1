using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {

    protected GameObject Item;
    public Unit Owner;
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
        if (e.button == PointerEventData.InputButton.Right) {
            //consume item in slot (if consumable)
            GameObject was = this.GetItemAndDetatch();
            Consumable wasAsCosumable = was.GetComponent<Consumable>() as Consumable;
            if (wasAsCosumable != null) {
                Debug.Log(this.Owner);
                Debug.Log(this.Owner.Hp);
                wasAsCosumable.ConsumeEffectOn(this.Owner);
                Debug.Log(this.Owner.Hp);
            } else {
                //put it back, since it isn't a consumable
                this.SetItemAndMakeChild(was);
            }
        } else if (e.button == PointerEventData.InputButton.Left) {
            GameObject held = this.Hand.GetHeldAndRemoveAsChild();
            GameObject was = this.GetItemAndDetatch();
            this.Hand.SetHeld(was);
            this.SetItemAndMakeChild(held);
        }
    }
}
