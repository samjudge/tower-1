using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class OpenStatPanelButton : MonoBehaviour, IPointerClickHandler{
	
	public StatPanel Panel;
    public GameObject[] MakeUnclickable; 
	public bool IsPanelVisible = false;

	public void OnPointerClick(PointerEventData e){
		IsPanelVisible = !IsPanelVisible;
		Panel.MakeVisible(IsPanelVisible);
        //disable all clickable events in the stat panel
        //make children clickable
        foreach (GameObject g in MakeUnclickable) {
            for (int x = g.gameObject.transform.childCount-1; x >= 0; x--) {
                g.SetActive(!IsPanelVisible);
            }
        }
	}
}
