using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flasher : MonoBehaviour {

	public void Flash(){
		Image Image = this.GetComponent<Image>() as Image;
		Color ScreenColor = Image.color;
		ScreenColor.a = 0;
		StartCoroutine(Fade());
	}

	public IEnumerator Fade(){
		float value = 0.7f;
		Image Image = this.GetComponent<Image>() as Image;
		Color ScreenColor = Image.color;
		ScreenColor.a = value;
		Image.color = ScreenColor;
		while(ScreenColor.a > 0){
			ScreenColor = Image.color;
			ScreenColor.a = value;
			Image.color = ScreenColor;
			value -= Time.deltaTime*2;
			yield return null;
		}
	}

}
