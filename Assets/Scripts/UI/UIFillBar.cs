using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFillBar : MonoBehaviour {
	
	private Image Bar;
	public Image EffectBar;
    public bool ChangeColorIfFilled = false;
    public Color FilledColor;
    public Color UnfilledColor;

    void Start(){
		this.Bar = this.GetComponent<Image>() as Image;
	}
	
	public void UpdateBar(float val, float maxVal){
		this.Bar.fillAmount = val/maxVal;
        if (ChangeColorIfFilled) {
            if (this.Bar.fillAmount == 1) {
                (this.Bar.GetComponent<Image>() as Image).color = FilledColor;
            } else {
                (this.Bar.GetComponent<Image>() as Image).color = UnfilledColor;
            }
        }
        
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
