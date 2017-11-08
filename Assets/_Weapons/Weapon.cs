using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    public AudioClip soundEffect;
    public float damage;
    public float attackSpeed;
    public int maxAmmo;
    public float range;
    public int dispersion;
    public string name;

    [HideInInspector] public int currentAmmo;

    #region Getters
    public int GetWeaponDispersion()
    {
        return dispersion;
    }
    public float GetWeaponDamage(){
        return damage;
    }
    public float GetWeaponSpeed()
    {
        return attackSpeed;
    }
    public int GetWeaponMaxAmmo(){
        return maxAmmo;
    }
    public float GetWeaponRange(){
        return range;
    }
    public AudioClip GetWeaponSound()
    {
        return soundEffect;
    }
    #endregion
}
