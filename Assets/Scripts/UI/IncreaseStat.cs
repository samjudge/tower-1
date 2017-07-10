using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;
using System;

public class IncreaseStat : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    private Player Player;
    [SerializeField]
    private string StatName;

    void Update() {
        if (Player.HasFreePoints()) {
            this.GetComponent<CanvasRenderer>().SetAlpha(1f);
        } else {
            this.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (Player.HasFreePoints()) {
            Player.SpendPointOn(StatName);
        }
    }
}
