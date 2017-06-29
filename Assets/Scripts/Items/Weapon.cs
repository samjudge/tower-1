using UnityEngine;
using System;

[Serializable]
public class Weapon : Equipment {

    [SerializeField]
    private Unit Owner;
    [SerializeField]
    private string[] EquippableTo;
    [SerializeField]
    public int DiceCount;
    [SerializeField]
    public int DiceMaxRoll;

    /**
     * GetDmgRangeString()
     * @return string - the damge of this weapon, in the form of 2d8 (D&D-style)
     */
    public string GetDmgRangeString() {
        return (DiceCount + "d" + DiceMaxRoll);
    }

    /**
     * RollDice()
     * generate a number by the roll the dice values of this item, in the form of 2D8 etc. (D&D-style)
     */
    virtual public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for(int x = 0; x < DiceCount; x++) {
            int roll = r.Next((DiceMaxRoll));
            score += (roll + 1);
        }
        return score;
    }

    //@Override
    public override string[] GetEquippableTo() {
        return EquippableTo;
    }

    //@Override
    public override Unit GetOwner() {
        return Owner;
    }

    //@Override
    public override void SetEquippableTo(string[] s) {
        EquippableTo = s;
    }

    //@Override
    public override void SetOwner(Unit o) {
        Owner = o;
    }
}