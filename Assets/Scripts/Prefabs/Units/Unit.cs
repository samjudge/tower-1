using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EquippedEntity {

    public EquippedEntity(String SlotName, Equipment DefaultItem) {
        this.SlotName = SlotName;
        if (DefaultItem != null) {
            this.DefaultItem = MonoBehaviour.Instantiate(DefaultItem);
        }
    }

    public Equipment DefaultItem;
    public String SlotName;
    public Equipment Item;
}

[Serializable]
public class EquipmentModel {
    public EquippedEntity[] Equipment;
    public Unit Owner;

    public EquipmentModel(EquippedEntity[] Slots, Unit Owner) {
        Equipment = new EquippedEntity[Slots.Length];
        this.Owner = Owner;
        for (int x = 0; x < Equipment.Length; x++) {
            Equipment[x] = Slots[x];
        }
    }

    public Equipment Get(String s) {
        foreach (EquippedEntity e in Equipment) {
            if (e.SlotName == s) {
                if (e.Item == null) {
                    if (e.DefaultItem.GetOwner() == null) {
                        e.DefaultItem.SetOwner(Owner);
                    }
                    return e.DefaultItem;
                }
                if (e.Item.GetOwner() == null) {
                    e.DefaultItem.SetOwner(this.Owner);
                }
                return e.Item;
            }
        }
        return null;
    }

    public void Set(string s, Equipment i) {
        for (int x = 0; x < Equipment.Length; x++) {
            EquippedEntity e = Equipment[x];
            if (e.SlotName == s) {
                i.SetOwner(e.DefaultItem.GetOwner());
                e.Item = i;
            }
        }
    }

    public void SetToDefaultItem(string s) {
        for (int x = 0; x < Equipment.Length; x++) {
            EquippedEntity e = Equipment[x];
            if (e.SlotName == s) {
                e.Item = e.DefaultItem;
            }
        }
    }
}

public abstract class Unit : MonoBehaviour {
    [SerializeField]
    public float Hp = 0;

    public float CalculateMaxHp() {
        return this.Strength * 5;
    }

    [SerializeField]
    public float Mp = 0;

    public float CalculateMaxMp() {
        return this.Intelligence * 3;
    }

    [SerializeField]
    public float Strength;
    [SerializeField]
    public float Luck;
    [SerializeField]
    public float Intelligence;
    [SerializeField]
    public float Dexterity;
    
    public float CalculateEXPBounty() {
        return this.Strength*2 + this.Luck * 2 + this.Intelligence * 2 + this.Dexterity * 2;
    }

    protected Vector3 target;

    public EquipmentModel Equipment;

    private bool CancelMovementFlag = false;

    /**
	 * Begin a coroutine to shift this object's transfrom by the values given
	 * @param x the Forward/Backward tiles to shift this object by
	 * @param y the Left/Right tiles to shift this object by
	 */
    public void ShiftPosition(float x, float y) {
        if (InputLocked) return;
        InputLocked = true;
        Vector3 translateTo = new Vector3(
            y,
            0,
            x
            );
        translateTo = (this.transform.rotation * translateTo);
        this.target = (this.transform.position + translateTo);
        //shove enemies
        GameObject i = this.SearchForUnitsRaycast(target);
        this.StartCoroutine(Move(0.95f + (this.Dexterity/4)));
    }

    protected bool InputLocked = false;

    /** 
 	 * rotoate this game object
 	 * @param rotateBy the number of degrees you wish you rotate this object
 	 */
    public void RotateBy(float rotateBy) {
        if (InputLocked) return;
        InputLocked = true;
        Vector3 rotation = this.transform.rotation.eulerAngles;
        float to = (rotation.y + rotateBy);
        this.targetRotation = new Vector3(
            rotation.x,
            to,
            rotation.z
            );
        this.StartCoroutine(Rotate());
    }

    protected Vector3 targetRotation;

    protected IEnumerator Rotate() {
        Quaternion origin = this.transform.rotation;
        float range = Quaternion.Angle(this.transform.rotation, Quaternion.Euler(targetRotation));
        float t = 0;
        while (range > 1) {
            if (this.CancelMovementFlag == true) {
                break;
            }
            range = Quaternion.Angle(this.transform.rotation, Quaternion.Euler(targetRotation));
            t += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(origin, Quaternion.Euler(targetRotation), t * (0.95f + this.Dexterity / 6));
            yield return null;
        }
        if (this.CancelMovementFlag == true) {
            this.CancelMovementFlag = false;
            this.transform.rotation = origin;
        }
        else {
            this.transform.rotation = Quaternion.Euler(targetRotation);
        }
        InputLocked = false;
    }

    /** 
 	 * A coroutine that causes the object to Lerp-translate to the vector stored in target,
 	 * using delta time * 2 as it's t value.
 	 */
    protected IEnumerator Move(float TravelSpeed){
        Vector3 origin = this.transform.position;
        float distance = (origin - target).sqrMagnitude;
        float t = 0;
        while (distance > Vector3.kEpsilon)
        {
            GameObject o = SearchForUnitsRaycast(target);
            if (o != null) {
                this.CancelMovementFlag = true;
            }
            if (this.CancelMovementFlag == true){
                break;
            }
            distance =
                (this.transform.position - target).sqrMagnitude;
            t += Time.deltaTime;
            this.transform.position =
                Vector3.Lerp(origin, target, t * TravelSpeed);
            yield return null;
        }
        if (this.CancelMovementFlag == true) {
            this.CancelMovementFlag = false;
            //
            this.target = origin;
            //this.transform.position = origin;
            StartCoroutine(Move(1.95f + (this.Dexterity / 6)));
        }
        else {
            this.transform.position = target;
            InputLocked = false;
        }
    }
    /**
     * GameObject SearchForUnitsRaycast(Vector3 to)
     * @param to the vector to cast from the unit's position to
     * @return GameObject the first unit the raycast detects
     */
    private GameObject SearchForUnitsRaycast(Vector3 to) {
        LayerMask Mask = LayerMask.GetMask("Player","Enemies");
        RaycastHit hit = new RaycastHit();
        Physics.Linecast(
            this.transform.position,
            to,
            out hit,
            Mask
            );
        if (hit.transform != null){
            return hit.transform.gameObject;
        }
        else {
            return null;
        }
        
    }

    /**
     * Setting the Cancel Movement Flag to true will stop all
     * movement and return this object to it's last movement
     * launch point
     * @param flag - the new value of the CancelMovementFlag
     */
    public void SetCancelMovementFlag(bool flag) {
        this.CancelMovementFlag = flag;
    }

    abstract public void TakeDamage(float damage);

    abstract public void Attack(Unit u);

    public bool IsDead() {
        if (this.Hp <= 0)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider Other) {
        SpellProjectile s = Other.gameObject.GetComponent<SpellProjectile>() as SpellProjectile;
        if (s != null) {
            if (this.gameObject != s.GetCaster()) { //if the spell hits someone who isn't it's caster... 
                s.SpellEffectOn(this.gameObject);
            }
        }
    }
}
