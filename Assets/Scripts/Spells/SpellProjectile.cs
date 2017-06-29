using UnityEngine;

public abstract class SpellProjectile : MonoBehaviour {

    [SerializeField]
    private GameObject Caster;
    [SerializeField]
    private Quaternion CastDirection;

    public GameObject GetCaster() {
        return this.Caster;
    }

    public Quaternion GetDirection() {
        return this.CastDirection;
    }

    public void SetDirection(Quaternion r) {
        CastDirection = r;
    }

    public void SetCaster(GameObject c) {
        this.Caster = c;
        
    }

    /**
     * SpellEffectOn(GameObject Target)
     * @param GameObject Target - the target for this spell effect
     * Perform thie implemented action on collision with the target GameObject
     */
    public abstract void SpellEffectOn(GameObject Target);

    /**
     * MoveForward()
     * Propigate this GameObeject Forwards
     */
    protected void MoveForward() {
        Vector3 translateTo = new Vector3(
            1,
            0,
            0
            );
        translateTo = (CastDirection * translateTo);
        this.transform.position = (this.transform.position + (translateTo*(Time.deltaTime*4)));
    }
}