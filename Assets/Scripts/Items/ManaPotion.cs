using UnityEngine;
using System;

[Serializable]
public class ManaPotion : Consumable{

    [SerializeField]
    private float RestoreXMP;

    /**
     * ConsumeEffectOn(Unit u)
     * @param Unit u - Restore \RestoreXMP\ mp to this unit, up to the calculated max mp
     */
    override public void ConsumeEffectOn(Unit u) {
        if (u.Mp + RestoreXMP > u.CalculateMaxMp()) {
            u.Mp = u.CalculateMaxMp();
        } else {
            u.Mp += RestoreXMP;
        } if (u.GetType() == typeof(Player)) {
            Player p = u as Player;
            p.GetMPBar().UpdateBar(p.Mp,p.CalculateMaxMp());
        }
    }
}