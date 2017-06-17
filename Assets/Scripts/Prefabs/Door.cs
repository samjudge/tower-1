using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Player Player;
    bool isOpended = false;
    public Hand Hand;
    public string NeedsKeyOfName = "";

    void OnMouseDown(){
        if (this.NeedsKeyOfName != "") {
            GameObject Held = this.Hand.GetHeld();
            if (Held != null) {
                Item Item = this.Hand.GetHeld().GetComponent<Item>() as Item;
                if (Item.Name != NeedsKeyOfName) {
                    Player.ActionLog.WriteNewLine("the key doesn't seem to fit.");
                    return;
                }
            } else {
                Player.ActionLog.WriteNewLine("the door is locked tight.");
                return;
            }
        }
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2)
        {
            if (!isOpended)
            {
                isOpended = !isOpended;
                StartCoroutine(SlideDown());
            }
        }
    }

    private IEnumerator SlideDown() {
        Player.ActionLog.WriteNewLine("the door creeks open...");
        Vector3 target = new Vector3(
            this.transform.position.x,
            -1,
            this.transform.position.z
        );
        float t = 0;
        Vector3 origin = this.transform.position;
        while ((this.transform.position - target).sqrMagnitude > Vector3.kEpsilon) {
            t += Time.deltaTime;
            this.transform.position = Vector3.Lerp(origin,target,t);
            yield return null;
        }
        yield return null;
    }

    void OnTriggerEnter(Collider other) {
        //check if player
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null)
        {
            //set flag to cancel movement and return
            p.SetCancelMovementFlag(true);
        }
    }
}
