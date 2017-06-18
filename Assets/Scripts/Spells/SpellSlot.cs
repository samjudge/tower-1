using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IPointerClickHandler {

    public GameObject Spell;
    public GameObject Caster;

    public void Start() {
        
    }

    public void SetSpellAndMakeChild(GameObject o) {
        this.SetSpell(o);
        if (o != null) {
            o.transform.SetParent(this.transform);
            (o.GetComponent<Image>() as Image).rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public GameObject GetSpellAndDetatch() {
        this.transform.DetachChildren();
        GameObject t = this.Spell;
        this.Spell = null;
        return t;
    }

    public void SetSpell(GameObject o) {
        this.Spell = o;
    }

    public Spell GetSpell() {
        return this.Spell.GetComponent<Spell>() as Spell;
    }

    public void Cast() {
        if (Spell != null) {
            GameObject Sp = (Spell.GetComponent<Spell>() as Spell).Projectile;
            int Manacost = (Spell.GetComponent<Spell>() as Spell).ManaCost;
            Player CasterUnitComponent = Caster.GetComponent<Player>() as Player;
            //action on cooldown
            if (CasterUnitComponent.AttackTimer < CasterUnitComponent.NextAttackTimerMin) {
                return;
            }
            //not enough mana
            if (CasterUnitComponent.Mp < Manacost) {
                CasterUnitComponent.ActionLog.WriteNewLine("you are too drained to cast that spell!");
                return;
            }
            //create spell projectile
            GameObject Casted = Instantiate(Sp , Caster.transform.position,
                Quaternion.Euler(
                    Caster.transform.rotation.eulerAngles.x - 90,
                    Caster.transform.rotation.eulerAngles.y,
                    Caster.transform.rotation.eulerAngles.z
                )
            );
            (Casted.GetComponent<SpellProjectile>() as SpellProjectile).CastDirection = Quaternion.Euler(
                    Caster.transform.rotation.eulerAngles.x,
                    Caster.transform.rotation.eulerAngles.y - 90,
                    Caster.transform.rotation.eulerAngles.z
                );
            (Casted.GetComponent<SpellProjectile>() as SpellProjectile).Caster = Caster;
            //update player mp
            CasterUnitComponent.Mp -= Manacost;
            CasterUnitComponent
                .MPBar
                .UpdateBar(
                    CasterUnitComponent.Mp, CasterUnitComponent.CalculateMaxMp()
                );
            //reset attack timer
            CasterUnitComponent.AttackTimer = 0;
            CasterUnitComponent.NextAttackTimerMin = Manacost;
        }
    }

    virtual public void OnPointerClick(PointerEventData e) {
        //cast spell
        Cast();
    }

}
