using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : ContainerPanel {

    [SerializeField]
	private Button Restart;
    [SerializeField]
    private Button Load;
    [SerializeField]
    private Button Quit;
	
	void Start () {
		MakeVisible(false);
	}
}
