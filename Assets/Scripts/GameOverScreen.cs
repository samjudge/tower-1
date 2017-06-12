using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	public Button Restart;
	public Button Load;
	public Button Quit;

	public void MakeVisible(bool visible){
		this.gameObject.SetActive(visible);
	}
	
	void Start () {
		MakeVisible(false);
	}
}
