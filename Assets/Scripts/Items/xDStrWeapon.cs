using UnityEngine;
using System;

[Serializable]
public class xDStrWeapon : Weapon {

    override public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for(int x = 0; x < DiceCount; x++) {
            int roll = r.Next(((int)Owner.Strength));
            score += (roll + 1);
        }
        return score;
    }
}