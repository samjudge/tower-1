using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private GameObject Item;

    [SerializeField]
    private Unit Owner;

    [SerializeField]
    private Hand Hand;

    /* GetHand()
     * Get the hand (mouse pointer) associated with this slot
     */
    public Hand GetHand() {
        return Hand;
    }

    /**
     * MakeChild(GameObject o)
     * @param GameObject o - make O a child of this GameObject's transform
     */
    protected void AttachItem(GameObject o) {
        if (o != null) {
            o.transform.SetParent(this.transform);
            (o.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }

    /**
     * DetatchItem()
     * Detatch all items from this GameObject's transform
     */
    public void DetatchItems() {
        this.transform.DetachChildren();
    }

    /**
     * SetItem(GameObject o)
     * @param GameObject o - set the current item
     */
    protected void SetItem(GameObject o) {
        this.Item = o;
    }

    /**
     * GetItem()
     * @return GameObject - the currently occupying item GameObject
     */
    public GameObject GetItem() {
        return this.Item;
    }

    /**
     * OnPointerClick(PointerEventData e)
     * @param PointerEventData e - event data
     * Swap the item with whatever is in the associated Hand GameObject on left click
     * Attempt to use the item on a right click
     */
    virtual public void OnPointerClick(PointerEventData e){
        if (e.button == PointerEventData.InputButton.Right) {
            GameObject was = GetItem();
            DetatchItems();
            Consumable wasAsCosumable = was.GetComponent<Consumable>() as Consumable;
            if (wasAsCosumable != null) {
                //consume+destory
                wasAsCosumable.ConsumeEffectOn(Owner);
                Destroy(was);
            } else {
                SetItem(was);
                AttachItem(was);
            }
        } else if (e.button == PointerEventData.InputButton.Left) {
            SwapItemWithHeld();
        }
    }

    private void SwapItemWithHeld() {
        GameObject held = this.GetHand().GetHeld();
        this.GetHand().DetatchItems();
        GameObject was = this.GetItem();
        this.DetatchItems();
        this.GetHand().SetHeld(was);
        this.SetItem(held);
        this.AttachItem(held);
    }
}
