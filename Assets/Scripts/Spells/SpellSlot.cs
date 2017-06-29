using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    public GameObject Spell;
    [SerializeField]
    public GameObject Caster;

    public GameObject GetSpell() {
        return Spell;
    }

    private void SetSpell(GameObject o) {
        this.Spell = o;
    }

    private void AttachSpell(GameObject o) {
        if (o != null) {
            o.transform.SetParent(this.transform);
            (o.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void DetatchAllSpells() {
        this.transform.DetachChildren();
    }
    /**
     * Cast the spell in this slot, in the direction the caster GameObject is facing
     */
    private void Cast() {
        if (Spell != null) {
            GameObject Sp = (Spell.GetComponent<Spell>() as Spell).GetProjectile().gameObject;
            int Manacost = (Spell.GetComponent<Spell>() as Spell).ManaCost;
            Player CasterUnitComponent = Caster.GetComponent<Player>() as Player;
            /*
             * action on cooldown
             */
            if (CasterUnitComponent.AttackTimer < CasterUnitComponent.NextAttackTimerMin) {
                return;
            }
            /*
             * not enough man
             */
            if (CasterUnitComponent.Mp < Manacost) {
                CasterUnitComponent.ActionLog.WriteNewLine("you are too drained to cast that spell!");
                return;
            }
            /*
             * spawn projectile
             */
            GameObject Casted = Instantiate(Sp , Caster.transform.position,
                Quaternion.Euler(
                    Caster.transform.rotation.eulerAngles.x - 90,
                    Caster.transform.rotation.eulerAngles.y,
                    Caster.transform.rotation.eulerAngles.z
                )
            );
            (Casted.GetComponent<SpellProjectile>() as SpellProjectile).SetDirection(Quaternion.Euler(
                    Caster.transform.rotation.eulerAngles.x,
                    Caster.transform.rotation.eulerAngles.y - 90,
                    Caster.transform.rotation.eulerAngles.z
                )
            );
            (Casted.GetComponent<SpellProjectile>() as SpellProjectile).SetCaster(Caster);
            /*
             * update caster mp
             */
            CasterUnitComponent.Mp -= Manacost;
            CasterUnitComponent.MPBar.UpdateBar(
                CasterUnitComponent.Mp, CasterUnitComponent.CalculateMaxMp()
            );
            /*
             * reset caster timer
             */
            CasterUnitComponent.AttackTimer = 0;
            CasterUnitComponent.NextAttackTimerMin = Manacost;
        }
    }

    virtual public void OnPointerClick(PointerEventData e) {
        Cast();
    }

}
