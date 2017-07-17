using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour, Togglable {

    [SerializeField]
    public Player Player;

    [SerializeField]
    private Spell FireSpell;

    [SerializeField]
    private float FireSpellEvery = 2f;
    private float FireSpellCurrentTimer = 0;

    void Start() {}

    void Update() {
        if (FireSpellEvery != 0f) {
            FireSpellCurrentTimer += Time.deltaTime;
            if (FireSpellCurrentTimer < FireSpellEvery) {
                return;
            }
            FireSpellCurrentTimer = 0;
            Fire();
        }
    }

    private void Fire() {
        GameObject Casted = Instantiate(FireSpell.GetProjectile().gameObject,
            this.transform.position,
            Quaternion.Euler(
                this.transform.rotation.eulerAngles.x - 90,
                this.transform.rotation.eulerAngles.y,
                this.transform.rotation.eulerAngles.z
            )
        );
        (Casted.GetComponent<SpellProjectile>() as SpellProjectile).SetDirection(Quaternion.Euler(
                this.transform.rotation.eulerAngles.x,
                this.transform.rotation.eulerAngles.y - 90,
                this.transform.rotation.eulerAngles.z
            )
        );
        (Casted.GetComponent<SpellProjectile>() as SpellProjectile).SetCaster(this.gameObject);
    }

    public void Toggle() {
        Fire();
    }
}
