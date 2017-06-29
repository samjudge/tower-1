using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot {

    [SerializeField]
    private Player Player;

    [SerializeField]
    private string SlotName;

    private void SwapItemWithHeld() {
        GameObject held = this.GetHand().GetHeld();
        this.GetHand().DetatchChildItems();
        if (CanEquip(held)) {
            GameObject was = this.GetItem();
            this.DetatchItems();
            this.GetHand().SetHeld(was);
            this.SetItem(held);
            this.AttachItem(held);
            if (held == null) {
                Player.Equipment.SetToDefaultItem(SlotName);
            } else {
                Player.Equipment.Set(SlotName, held.GetComponent<Equipment>() as Equipment);
            }
        } else {
            this.GetHand().SetHeld(held);
        }
    }

    private bool CanEquip(GameObject i){
        if (i == null) return true;
        if (i.GetComponent<Item>().GetType().IsSubclassOf(typeof(Equipment))) {
            Equipment e = i.GetComponent<Equipment>() as Equipment;
            foreach(string t in e.GetEquippableTo()) {
                if (SlotName == t) {
                    return true;
                }
            }
        }
        return false;
    }

    /** 
     *  OnPointerClick(PointerEventData e)
     *  Swap the item in the Hand object with what is in the slot, and versa versa
     */
    override public void OnPointerClick(PointerEventData e) {
        SwapItemWithHeld();
    }

}