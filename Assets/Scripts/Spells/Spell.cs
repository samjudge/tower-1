using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    [SerializeField]
    private SpellProjectile SpellProjectile;
    [SerializeField]
    public int ManaCost;
    
    public SpellProjectile GetProjectile() {
        return SpellProjectile;
    }
}
