using UnityEngine;
using System.Collections;
using System;

public class Pit : Blocker, Destroyable {

    public Player p;

    private void Start() {
        if (this.p == null) {
            this.p = GameObject.Find("Player").GetComponent<Player>() as Player;
        }
    }

    public void Destroy() {
        Destroy(this.gameObject);
        p.GetActionLog().WriteNewLine("you hear a mechanical click...");
    }
}
