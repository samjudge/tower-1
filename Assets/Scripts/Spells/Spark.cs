using UnityEngine;
using System.Collections;

public class Spark : SpellProjectile {

    [SerializeField]
    public int DiceCount;
    [SerializeField]
    public int DiceMaxRoll;

    private void Start() {
        Player p = GetCaster().GetComponent<Player>() as Player;
        if (p != null) {
            p.ActionLog.WriteNewLine("sparks fly from your fingertips!");
        }
    }

    void Update() {
        MoveForward();
    }

    /**
     * RollDice()
     * generate a number by the roll the dice values of this item, in the form of 2D8 etc. (D&D-style)
     */
    public int RollDice() {
        System.Random r = new System.Random();
        int score = 0;
        for (int x = 0; x < DiceCount; x++) {
            int roll = r.Next((DiceMaxRoll));
            score += (roll + 1);
        }
        return score;
    }
    /*
     * SpellEffectOn(GameObject Target)
     * @Override
     */
    override public void SpellEffectOn(GameObject Target) {
        Unit UnitTarget = Target.GetComponent<Unit>() as Unit;
        if (UnitTarget != null) {
            Unit UnitCaster = GetCaster().GetComponent<Unit>() as Unit;
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
