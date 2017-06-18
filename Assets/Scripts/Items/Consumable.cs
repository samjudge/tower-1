using UnityEngine;
using System.Collections;

public abstract class Consumable : Item {
    abstract public void ConsumeEffectOn(Unit u);
}
