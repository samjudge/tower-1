using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {
	public void Reload(){
        SceneManager.LoadScene("1", LoadSceneMode.Single);
    }
}
