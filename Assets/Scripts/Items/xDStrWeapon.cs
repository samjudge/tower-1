using UnityEngine;
using System;

[Serializable]
public class xDStrWeapon : Weapon {
    /**
     * RollDice()
     * @Override
     * @Return int - the roll of the dice
     * Roll the dice as a value between [diceCount] x (0 - Owner Strength)
     */
    override public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for(int x = 0; x < this.DiceCount; x++) {
            int roll = r.Next(( (int) GetOwner().Strength ));
            score += (roll + 1);
        }
        return score;
    }
}