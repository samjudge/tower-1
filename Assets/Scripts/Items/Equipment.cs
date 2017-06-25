using UnityEngine;
using System.Collections;

abstract public class Equipment : Item {
    public Unit Owner;
    public string[] EquippableTo;
}
