using UnityEngine;
using System.Collections;

public class Spark : SpellProjectile {

    [SerializeField]
    public int DiceCount;
    [SerializeField]
    public int DiceMaxRoll;

    private void Start() {
        if (this.GetCaster() == null) {
            this.SetCaster(GameObject.Find("Player"));
        }
        Player p = GetCaster().GetComponent<Player>() as Player;
        if (p != null) {
            p.GetActionLog().WriteNewLine("sparks fly from your fingertips!");
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
            bool notDeadFlag = false;
            if (UnitCaster != null) {
                if (UnitCaster.GetType() == typeof(Player)) {
                    (UnitCaster as Player)
                        .GetActionLog()
                        .WriteNewLine(
                            "zapp! the sparks deal " + roll + " damage!"
                         );
                }
                if (!UnitCaster.IsDead()) {
                    notDeadFlag = true;
                }
            }
            UnitTarget.TakeDamage(roll);
            Player p = GetCaster().GetComponent<Player>() as Player;
            if (p != null) {
                if (notDeadFlag && UnitTarget.IsDead()) {
                    p.IncrementExperience(UnitTarget.CalculateEXPBounty());
                }
            }
            MonoBehaviour.Destroy(this.gameObject); //destory this projectile
        }
    }

    
}
