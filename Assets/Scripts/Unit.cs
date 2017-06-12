using System;
using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour {
    public float Hp = 0;

    public float CalculateMaxHp()
    {
        return this.Strength * 5;
    }

    public float Mp = 0;

    public float CalculateMaxMp()
    {
        return this.Intelligence * 3;
    }

    public float Strength;
    public float Luck;
    public float Intelligence;
    public float Dexterity;

    protected Vector3 target;

    private bool CancelMovementFlag = false;

    /**
	 * Begin a coroutine to shift this object's transfrom by the values given
	 * @param x the Forward/Backward tiles to shift this object by
	 * @param y the Left/Right tiles to shift this object by
	 */
    public void ShiftPosition(float x, float y)
    {
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
        GameObject i = this.GetUnitInfo(target);
        this.StartCoroutine(Move());
    }

    protected bool InputLocked = false;

    /** 
 	 * rotoate this game object
 	 * @param rotateBy the number of degrees you wish you rotate this object
 	 */
    public void RotateBy(float rotateBy)
    {
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

    protected IEnumerator Rotate()
    {
        Quaternion origin = this.transform.rotation;
        float range = Quaternion.Angle(this.transform.rotation, Quaternion.Euler(targetRotation));
        float t = 0;
        while (range > 1)
        {
            if (this.CancelMovementFlag == true)
            {
                break;
            }
            range = Quaternion.Angle(this.transform.rotation, Quaternion.Euler(targetRotation));
            t += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(origin, Quaternion.Euler(targetRotation), t * 2);
            yield return null;
        }
        if (this.CancelMovementFlag == true)
        {
            this.CancelMovementFlag = false;
            this.transform.rotation = origin;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(targetRotation);
        }
        InputLocked = false;
    }

    /** 
 	 * A coroutine that causes the object to Lerp-translate to the vector stored in target,
 	 * using delta time * 2 as it's t value.
 	 */
    protected IEnumerator Move()
    {
        Vector3 origin = this.transform.position;
        float distance = (origin - target).sqrMagnitude;
        float t = 0;
        while (distance > Vector3.kEpsilon)
        {
            GameObject o = GetUnitInfo(target);
            if (o != null) {
                this.CancelMovementFlag = true;
            }
            if (this.CancelMovementFlag == true)
            {
                break;
            }
            distance =
                (this.transform.position - target).sqrMagnitude;
            t += Time.deltaTime;
            this.transform.position =
                Vector3.Lerp(origin, target, t * 2);
            yield return null;
        }
        if (this.CancelMovementFlag == true)
        {
            this.CancelMovementFlag = false;
            this.transform.position = origin;
        }
        else
        {
            this.transform.position = target;
        }
        InputLocked = false;
    }

    private GameObject GetUnitInfo(Vector3 to){
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
    public void SetCancelMovementFlag(bool flag)
    {
        this.CancelMovementFlag = flag;
    }

    abstract public void TakeDamage(float damage);

    public bool IsDead()
    {
        if (this.Hp <= 0)
        {
            return true;
        }
        return false;
    }
}
