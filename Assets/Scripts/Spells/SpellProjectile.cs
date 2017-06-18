using UnityEngine;

public abstract class SpellProjectile : MonoBehaviour {

    public GameObject Caster;
    public Quaternion CastDirection;

    public abstract void SpellEffectOn(GameObject Target);

    public void MoveForward() {
        Vector3 translateTo = new Vector3(
            1,
            0,
            0
            );
        translateTo = (CastDirection * translateTo);
        this.transform.position = (this.transform.position + (translateTo*(Time.deltaTime*4)));
    }
}