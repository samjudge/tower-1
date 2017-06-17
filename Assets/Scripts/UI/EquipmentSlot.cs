using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : ItemSlot
{
    public Player Player;
    public string SlotName;

    private void SwapItemWithHeld() {
        GameObject held = this.Hand.GetHeldAndRemoveAsChild();
        if (CanEquip(held)) {
            GameObject was = this.GetItemAndDetatch();
            this.Hand.SetHeld(was);
            this.SetItemAndMakeChild(held);
            if (held == null) {
                Player.Equipment.SetToDefaultItem(SlotName);
            } else {
                Player.Equipment.Set(SlotName, held.GetComponent<Equipment>() as Equipment);
            }
        } else {
            this.Hand.SetHeld(held);
        }
    }
    
    override public void OnPointerClick(PointerEventData e) {
        SwapItemWithHeld();
    }

    private bool CanEquip(GameObject i){
        if (i == null) return true;
        if (i.GetComponent<Item>().GetType().IsSubclassOf(typeof(Equipment))) {
            Equipment e = i.GetComponent<Equipment>() as Equipment;
            foreach(string t in e.EquippableTo) {
                if (SlotName == t) {
                    return true;
                }
            }
        }
        return false;
    }

}