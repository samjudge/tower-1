using UnityEngine;
using System.Collections;

public abstract class Switch : MonoBehaviour, Togglable {

    [SerializeField]
    private GameObject[] Interactions;
    [SerializeField]
    private bool IsSwitchedOn = false;
    //If true, then interactions will -only- be performed once. Incompatable with IsButton.
    [SerializeField]
    private bool OneWay = false;

    abstract public void SwitchOn();
    abstract public void SwitchOff();

    virtual public void Toggle() {
        this.IsSwitchedOn = !this.IsSwitchedOn;
        if (IsSwitchedOn) {
            SwitchOff();
        } else {
            SwitchOn();
        }
    }

    //If true, then interactions will be performed reguardless of if the button is on or off. Incompatable with OneWay.
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
                } else if (IsButton == true) {
                    O.ToggleOpen();
                }
            }
            Destroyable D = G.GetComponent<Destroyable>() as Destroyable;
            if (D != null) {
                if (!IsSwitchedOn) {
                    D.Destroy();
                } else if (IsButton == true) {
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
            this.IsSwitchedOn = !this.IsSwitchedOn;
            SwitchOn();
        } else if (OneWay == false) {
            this.IsSwitchedOn = !this.IsSwitchedOn;
            SwitchOff();
        }
    }

}
