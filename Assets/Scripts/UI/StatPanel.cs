using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPanel : ContainerPanel {
    [SerializeField]
	private Player Player;
    [SerializeField]
    private Text NameLabel;
    [SerializeField]
    private Text HpLabel;
    [SerializeField]
    private Text MpLabel;
    [SerializeField]
    private Text LevelLabel;
    [SerializeField]
    private Text ExpLabel;
    [SerializeField]
    private Text StrengthLabel;
    [SerializeField]
    private Text IntelligenceLabel;
    [SerializeField]
    private Text LuckLabel;
    [SerializeField]
    private Text DexterityLabel;
    [SerializeField]
    private Text DamageLabel;
    [SerializeField]
    private Text AcLabel;

	void Start () {
		MakeVisible(false);
	}

	void Update () {
		//update all the labels based on player stats here
		this.SetNameLabel("The Hero");
		this.SetHpLabel(Player.Hp + "/" + Player.CalculateMaxHp());
		this.SetMpLabel(Player.Mp + "/" + Player.CalculateMaxMp());
		this.SetStrengthLabel(Player.Strength.ToString());
		this.SetDexterityLabel(Player.Dexterity.ToString());
		this.SetIntelligenceLabel(Player.Intelligence.ToString());
		this.SetLuckLabel(Player.Luck.ToString());
		this.SetLevelLabel("1");
		this.SetExpLabel("0");
		this.SetArmourLabel("0");
        Debug.Log(Player.Equipment.Get("Left"));
		this.SetDamageLabel(
            (Player.Equipment.Get("Left").GetComponent<Weapon>() as Weapon).GetDmgRangeString()
        );
	}

	private void SetNameLabel(string l){
		this.NameLabel.text = l;
	}

	private void SetHpLabel(string l){
		this.HpLabel.text = l;
	}

	private void SetMpLabel(string l){
		this.MpLabel.text = l;
	}

	private void SetLevelLabel(string l){
		this.LevelLabel.text = l;
	}

	private void SetExpLabel(string l){
		this.ExpLabel.text = l;
	}

	private void SetStrengthLabel(string l){
		this.StrengthLabel.text = l;
	}

	private void SetIntelligenceLabel(string l){
		this.IntelligenceLabel.text = l;
	}

	private void SetLuckLabel(string l){
		this.LuckLabel.text = l;
	}

	private void SetDexterityLabel(string l){
		this.DexterityLabel.text = l;
	}

	private void SetDamageLabel(string l){
		this.DamageLabel.text = l;
	}

	private void SetArmourLabel(string l){
		this.AcLabel.text = l;
	}


}
