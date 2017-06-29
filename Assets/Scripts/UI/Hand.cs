using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hand : MonoBehaviour{

    [SerializeField]
    private GameObject HeldThing;

    void Update() {
        /*
         * automatically set the children for this object's transform
         */
        RectTransform rectTransform = this.GetComponent<RectTransform>() as RectTransform;
        rectTransform.position = Input.mousePosition;
        if (HeldThing != null) {
            if (HeldThing.transform.parent != this.transform) {
                HeldThing.transform.SetParent(this.transform);
                //for some reason, when i set the parent above,
                //the image scales inverse to the screen resolution
                //i've checked reference pixels, anchoring, turning setParent to false
                //but it always is just something weird
                //setting it directly below here seems to work fine for all resolutions though
                HeldThing.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                (HeldThing.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    /** SetHeld(GameObject t)
     *  @param GameObject t - the item to set as being held
     *  Set this item held by the hand
     */
    public void SetHeld(GameObject t) {
        this.HeldThing = t;
    }

    /** DetatchChildItems()
     *  Remove all children from this object's transform
     */
    public void DetatchChildItems() {
        this.transform.DetachChildren();
    }

    /** GetHeld()
     * @Return GameObject the gameobject held by the samee
     */
    public GameObject GetHeld() {
        return this.HeldThing;
    }

    /* 
     * bool IsHoldingThing()
     * @return bool - true if the hand is holding an item
     * Is this hand holding any item at all?
     */
    public bool IsHoldingThing() {
        if (HeldThing != null) {
            return true;
        }
        return false;
    }

    /* 
     * bool IsHoldingThing(Gameobject o)
     * @param GameObject o - the item to be compared against
     * @return bool - true if the hand is holding the GameObject `o`
     * Is this hand holding any item at all?
     */
    public bool IsHoldingThing(GameObject o) {
        if (HeldThing == o) {
            return true;
        }
        return false;
    }



}
