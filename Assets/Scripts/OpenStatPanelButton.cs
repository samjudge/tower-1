using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class OpenStatPanelButton : MonoBehaviour, IPointerClickHandler{
	
	public StatPanel Panel;
	public bool IsPanelVisible = false;

	public void OnPointerClick(PointerEventData e){
		IsPanelVisible = !IsPanelVisible;
		Panel.MakeVisible(IsPanelVisible);
	}
}
