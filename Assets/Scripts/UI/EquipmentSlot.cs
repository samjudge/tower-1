using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : ItemSlot
{
    public Player Player;
    public string SlotName;
    public Equipment DefaultItem;

    private void SwapItemWithHeld() {
        GameObject held = Hand.GetHeld();
        GameObject was = GetItemAndDetatch();
        if (held == null) {
            Hand.SetHeld(was);
            //SetItemAndMakeChild(was);
            return;
        }
        if (CanEquipTo(held, this.SlotName)) {
            Debug.Log("can equip");
            Hand.GetHeldAndRemoveAsChild();
            
            SetItemAndMakeChild(held);
            Player.Equipment[this.SlotName] = held.GetComponent<Equipment>() as Equipment;
        } else {
            Debug.Log("cannot equip");
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