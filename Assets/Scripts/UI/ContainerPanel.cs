using UnityEngine;
using System.Collections;

public abstract class ContainerPanel : MonoBehaviour {
    /**
     * MakeVisible(bool visible)
     * @param bool visible - what visiblity setting to set the game over screen
     * Make the GameOver screen visible
     */
    public void MakeVisible(bool visible) {
        this.gameObject.SetActive(visible);
    }
}
