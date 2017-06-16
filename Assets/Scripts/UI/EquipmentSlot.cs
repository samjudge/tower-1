using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : ItemSlot
{
    public Player Player;
    public string SlotName;

    private void SwapItemWithHeld() {
        GameObject held = Hand.GetHeld();
        GameObject was = GetItemAndDetatch();
        if (held == null) {
            Hand.SetHeld(was);
            Player.Equipment.SetToDefaultItem(this.SlotName);
            return;
        }
        if (CanEquipTo(held, this.SlotName)) {
            Hand.GetHeldAndRemoveAsChild();
            SetItemAndMakeChild(held);
            Player.Equipment.Set(this.SlotName, held.GetComponent<Equipment>() as Equipment);
        } else {
            SetItemAndMakeChild(was);
        }
    }
    
    override public void OnPointerClick(PointerEventData e) {
        SwapItemWithHeld();
    }

    private bool CanEquipTo(GameObject i, string avail){
        if (i.GetComponent<Item>().GetType().IsSubclassOf(typeof(Equipment))) {
            Equipment e = i.GetComponent<Equipment>() as Equipment;
            foreach(string t in e.EquippableTo) {
                if (avail == t) {
                    return true;
                }
            }
        }
        return false;
    }

}