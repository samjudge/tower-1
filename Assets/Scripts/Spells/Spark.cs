using UnityEngine;
using System.Collections;

public class Spark : SpellProjectile {

    public int DiceCount;
    public int DiceMaxRoll;

    public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for (int x = 0; x < DiceCount; x++) {
            int roll = r.Next((DiceMaxRoll));
            score += (roll + 1);
        }
        return score;
    }

    private void Start() {
        Player p = Caster.GetComponent<Player>() as Player;
        if (p != null) {
            p.ActionLog.WriteNewLine("sparks fly from your fingertips!");
        }
    }

    void Update() {
        //move forward
        MoveForward();
    }

    override public void SpellEffectOn(GameObject Target) {
        Unit UnitTarget = Target.GetComponent<Unit>() as Unit;
        if (UnitTarget != null) {
            Unit UnitCaster = Caster.GetComponent<Unit>() as Unit;
            int roll = RollDice();
            if (UnitCaster.GetType() == typeof(Player)) {
                (UnitCaster as Player)
                    .ActionLog
                    .WriteNewLine(
                        "zapp! the sparks deal " + roll + " damage!"
                     );
            }
            UnitTarget.TakeDamage(roll);
            MonoBehaviour.Destroy(this.gameObject); //destory this projectile
        }
    }
}
