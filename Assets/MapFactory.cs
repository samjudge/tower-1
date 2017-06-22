using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MapFactory : MonoBehaviour {

    //Tiles
    public GameObject WallTile;

    public GameObject MakeMapTile(string name) {
        FieldInfo Property = this.GetType().GetField(name);
        GameObject ItemPrefab = Property.GetValue(this) as GameObject;
        GameObject nItem = Instantiate(ItemPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
        return nItem;
    }
}
