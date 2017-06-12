using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFillBar : MonoBehaviour {
	
	private Image Bar;
	public Image EffectBar;

	void Start(){
		this.Bar = this.GetComponent<Image>() as Image;
	}
	
	public void UpdateBar(float val, float maxVal){
		this.Bar.fillAmount = val/maxVal;
		StartCoroutine(DoEffect());
	}

	private IEnumerator DoEffect(){
		float currentFill = this.Bar.fillAmount;
		float targetFill = this.EffectBar.fillAmount;
		while(EffectBar.fillAmount > this.Bar.fillAmount){
			this.EffectBar.fillAmount -= Time.deltaTime/4;
			yield return null;
		}
		this.EffectBar.fillAmount = this.Bar.fillAmount;

	}
	
}
