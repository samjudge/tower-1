using UnityEngine;
using System.Collections;
using System;

public class Pit : Blocker, Destroyable {

    public Player p;

    public void Destroy() {
        Destroy(this.gameObject);
        p.GetActionLog().WriteNewLine("you hear a mechanical click...");
    }
}
