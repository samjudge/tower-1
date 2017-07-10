using UnityEngine;
using System.Collections;

public abstract class Switch : MonoBehaviour, Togglable {

    [SerializeField]
    public Player Player;
    [SerializeField]
    private GameObject[] Interactions;
    [SerializeField]
    private bool IsSwitchedOn = false;
    [SerializeField]
    private bool OneWay = false;

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

    [SerializeField]
    private bool IsButton = false;

    public void PerformInteractions() {
        foreach (GameObject G in Interactions) {
            if (G == null) {
                continue;
            }
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
                } else if (IsButton == true) {
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
