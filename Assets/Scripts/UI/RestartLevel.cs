using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartLevel : MonoBehaviour {
	public void Reload(){
        SceneManager.LoadScene("1", LoadSceneMode.Single);
    }
}
