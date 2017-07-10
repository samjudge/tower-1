using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpellSlot : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private Spell Spell;
    [SerializeField]
    private Unit Caster;

    public Spell GetSpell() {
        return Spell;
    }

    private void SetSpell(Spell o) {
        this.Spell = o;
    }

    private void AttachSpell(Spell o) {
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
            int Manacost = Spell.ManaCost;
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
                CasterUnitComponent.GetActionLog().WriteNewLine("you are too drained to cast that spell!");
                return;
            }
            /*
             * spawn spell and initalize it's projectile
             */
            GameObject Casted = Instantiate(Spell.GetProjectile().gameObject,
                Caster.transform.position,
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
            (Casted.GetComponent<SpellProjectile>() as SpellProjectile).SetCaster(Caster.gameObject);
            /*
             * update caster mp
             */
            CasterUnitComponent.Mp -= Manacost;
            CasterUnitComponent.GetMPBar().UpdateBar(
                CasterUnitComponent.Mp, CasterUnitComponent.CalculateMaxMp()
            );
            /*
             * reset caster timer
             */
            CasterUnitComponent.AttackTimer = 0;
            CasterUnitComponent.NextAttackTimerMin = Math.Max(0.33f,(Manacost*4)/CasterUnitComponent.Intelligence);
        }
    }

    virtual public void OnPointerClick(PointerEventData e) {
        Cast();
    }

}
