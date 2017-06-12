using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour {
	public void Reload(){
		Application.LoadLevel(0);
	}
}
