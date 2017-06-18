using UnityEngine;
using System;

[Serializable]
public class Weapon : Equipment {
    public int DiceCount;
    public int DiceMaxRoll;

    public string GetDmgRangeString() {
        return (this.DiceCount + "d" + this.DiceMaxRoll);
    }

    public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for(int x = 0; x < DiceCount; x++) {
            int roll = r.Next((DiceMaxRoll));
            score += (roll + 1);
        }
        return score;
    }
}