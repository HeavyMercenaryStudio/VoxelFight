using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject {

    [SerializeField] GameObject weaponPrefab;

    [SerializeField] float weaponDamage;
    [SerializeField] float weaponMaxAmmo;
    [SerializeField] float weaponAmmo;
    [SerializeField] float weaponRange;


    #region Getters
    public float GetWeaponDamage(){
        return weaponDamage;
    }
    public float GetWeaponAmmo(){
        return weaponAmmo;
    }
    public float GetWeaponMaxAmmo(){
        return weaponMaxAmmo;
    }
    public float GetWeaponRange(){
        return weaponRange;
    }
    #endregion
    #region Setters
    public void SetWeaponDamage(float value){
         weaponDamage = value;
    }
    public void SetWeaponAmmo(float value){
         weaponAmmo = value;
    }
    public void SetWeaponMaxAmmo(float value){
         weaponMaxAmmo = value;
    }
    public void SetWeaponRange(float value){
         weaponRange = value;
    }
    #endregion

}
