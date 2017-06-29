using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanelButton : MonoBehaviour, IPointerClickHandler{
	[SerializeField]
	private ContainerPanel Panel;
    [SerializeField]
    private GameObject[] MakeUnclickable;
    [SerializeField]
    private bool IsPanelVisible = false;

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
