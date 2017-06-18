using UnityEngine;
using System;

[Serializable]
public class HealthPotion : Consumable{
    public float RestoreXHP;

    override public void ConsumeEffectOn(Unit u) {
        if (u.Hp + RestoreXHP > u.CalculateMaxHp()) {
            u.Hp = u.CalculateMaxHp();
        } else {
            u.Hp += RestoreXHP;
        }
        if (u.GetType() == typeof(Player)) {
            Player p = u as Player;
            p.HPBar.UpdateBar(p.Hp,p.CalculateMaxHp());
        }
    }
}