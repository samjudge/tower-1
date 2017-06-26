using UnityEngine;
using System.Collections;

public abstract class Switch : MonoBehaviour, Togglable {

    public Player Player;
    public GameObject[] Interactions;
    public bool IsSwitchedOn = false;
    public bool OneWay = false;

    abstract public void SwitchOn();
    abstract public void SwitchOff();

    public void Toggle() {
        this.IsSwitchedOn = !this.IsSwitchedOn;
        if (IsSwitchedOn) {
            SwitchOff();
        } else {
            SwitchOn();
        }
    }

    public void PerformInteractions() {
        foreach (GameObject G in Interactions) {
            Openable O = G.GetComponent<Openable>() as Openable;
            if (O != null) {
                if (!IsSwitchedOn) {
                    O.ToggleOpen();
                } else if (OneWay == false){
                    O.ToggleOpen();
                }
            }
            Destroyable D = G.GetComponent<Destroyable>() as Destroyable;
            if (D != null) {
                if (!IsSwitchedOn) {
                    D.Destroy();
                }
            }
            Togglable T = G.GetComponent<Togglable>() as Togglable;
            if (T != null) {
                if (!IsSwitchedOn) {
                    T.Toggle();
                }
            }
        }
        if (!IsSwitchedOn) {
            Toggle();
        } else if (OneWay == false) {
            Toggle();
        }
    }

}
