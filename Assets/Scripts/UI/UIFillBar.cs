using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFillBar : MonoBehaviour {

	[SerializeField]
	private Image Bar;
    [SerializeField]
    private Image EffectBar;
    [SerializeField]
    private bool ChangeColorIfFilled = false;
    [SerializeField]
    private Color FilledColor;
    [SerializeField]
    private Color UnfilledColor;
    
    void Start(){
		this.Bar = this.GetComponent<Image>() as Image;
	}
    /**
     * UpdateBar(float val, float maxVal)
     * @param float val - the numerator of the bar
     * @param float maxVal - the denominmator of the bar
     * Update the bar to show as a percentage (0.0 - 1.0) of the provided values
     */
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
