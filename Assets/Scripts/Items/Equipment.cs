using UnityEngine;

abstract public class Equipment : Item {
    /**
     * GetOwner()
     * @return Unit - the owner of this equipment
     */
    public abstract Unit GetOwner();
    public abstract void SetOwner(Unit u);

    /**
     * GetEquippableTo()
     * @return string[] - an array of names that this equipment item is associated with
     */
    public abstract string[] GetEquippableTo();
    public abstract void SetEquippableTo(string[] s);
}
