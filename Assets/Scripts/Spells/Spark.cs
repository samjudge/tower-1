using UnityEngine;
using System.Collections;

public class Spark : SpellProjectile {
    
    void Update() {
        //move forward
        MoveForward();
    }

    override public void SpellEffectOn(GameObject Target) {
        Unit UnitTarget = Target.GetComponent<Unit>() as Unit;
        if (UnitTarget != null) {
            Unit UnitCaster = Caster.GetComponent<Player>() as Player;
            UnitTarget.TakeDamage(3); //deal 5 damage
            MonoBehaviour.Destroy(this.gameObject); //destory this
        }
    }
}
