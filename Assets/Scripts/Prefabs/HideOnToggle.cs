using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HideOnToggle : MonoBehaviour, Togglable {

    [SerializeField]
    private bool IsActive = true;

    protected void SetActive(bool active) {
        this.IsActive = active;
    }

    public bool GetActive() {
        return this.IsActive;
    }

    virtual public void Toggle() {
        IsActive = !IsActive;
        this.gameObject.SetActive(IsActive);
    }
}
