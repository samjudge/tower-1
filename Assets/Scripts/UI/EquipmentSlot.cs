using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : ItemSlot
{
    public Player Player;
    public string SlotName;
    public Equipment DefaultItem;

    override public void OnPointerClick(PointerEventData e) {
        if (this.Item != null) {
            if ((this.Item.GetComponent<Item>().GetType()).IsSubclassOf(typeof(Equipment))) { //if it's a weapon
                Debug.Log("Is Equipment in slot");
                Equipment Equipment = this.Item.GetComponent<Equipment>() as Equipment;
                if (CanEquipTo(Equipment,this.SlotName)) { //if the slot is valid
                    Debug.Log("Can equip");
                    GameObject held = Hand.GetHeldAndRemoveAsChild();
                    if (held != null) { //if your hand isn't empty
                        Debug.Log("hand is NOT empty");
                        //unequip the item that was in this slot
                        GameObject was = this.GetItemAndDetatch();
                        //equp the item in hand
                        this.SetItemAndMakeChild(held);
                        Equipment = this.Item.GetComponent<Equipment>() as Equipment;
                        this.Player.Equipment[this.SlotName] = Equipment;
                        //set your hand to hold the item that was in this slot
                        this.Hand.SetHeld(was);
                    } else {
                        Debug.Log("hand IS empty");
                        //set the item in this slot to be null
                        GameObject was = this.GetItemAndDetatch();
                        this.Hand.SetHeld(was);
                        //set player's item to the deault (fist etc)
                        this.Player.Equipment[this.SlotName] = DefaultItem;
                    }
                }
            } else {//if it's something else
                //don't equip
            }
        }
        else{
            Debug.Log("NO item in slot");
            GameObject held = Hand.GetHeld();
            if (held != null) {
                if ((held.GetComponent<Item>().GetType()).IsSubclassOf(typeof(Equipment))) { //if the item in your hand is an equipment
                    if (CanEquipTo(held.GetComponent<Item>(), this.SlotName)) {
                        held = Hand.GetHeldAndRemoveAsChild();
                        this.SetItemAndMakeChild(held);
                    }
                } else {
                    //don't equip
                }
            }
        }
    }

    private bool CanEquipTo(Item i, string avail){
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