using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Item : MonoBehaviour {
    [SerializeField]
    private string Name;
    [SerializeField]
    private int Weight;

    public string GetName() {
        return Name;
    }

    public int GetWeight() {
        return Weight;
    }
}
