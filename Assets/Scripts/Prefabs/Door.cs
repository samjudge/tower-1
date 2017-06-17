using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Blocker, Openable {

    public Player Player;
    public Hand Hand;
    public string NeedsKeyOfName = "";
    public bool IsOpened = false;
    private bool Locked = true;
    private bool InMotion = false;

    public bool IsLocked() {
        return Locked;
    }

    public void ToggleLock() {
        Locked = false;
    }

    public bool CanOpen() {
        return !InMotion;
    }

    public bool CanClose() {
        return !InMotion;
    }

    public void Open() {
        if (!IsOpened) {
            IsOpened = !IsOpened;
            StartCoroutine(Slide(-1f));
        }
    }

    public void Close() {
        if (IsOpened) {
            IsOpened = !IsOpened;
            StartCoroutine(Slide(0f));
        }
    }

    void OnMouseDown() {
        //attempt to unlock if locked
        if (IsLocked()) {
            if (this.NeedsKeyOfName != "") {
                GameObject Held = this.Hand.GetHeld();
                if (Held != null) {
                    Item Item = this.Hand.GetHeld().GetComponent<Item>() as Item;
                    if (Item.Name != NeedsKeyOfName) {
                        Player.ActionLog.WriteNewLine("the key doesn't seem to fit.");
                    } else {
                        ToggleLock();
                    }
                } else {
                    Player.ActionLog.WriteNewLine("the door is locked tight.");
                    return;
                }
            } else {
                ToggleLock();
            }
        }
        //open door if unlocked
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1.2){
            if (!IsLocked()) {
                Open();
            }
        }
    }

    private IEnumerator Slide(float YDestination) {
        this.InMotion = true;
        Player.ActionLog.WriteNewLine("the door creeks open...");
        Vector3 target = new Vector3(
            this.transform.position.x,
            YDestination,
            this.transform.position.z
        );
        float t = 0;
        Vector3 origin = this.transform.position;
        while ((this.transform.position - target).sqrMagnitude > Vector3.kEpsilon) {
            t += Time.deltaTime;
            this.transform.position = Vector3.Lerp(origin,target,t);
            yield return null;
        }
        this.InMotion = false;
        yield return null;
    }
}
