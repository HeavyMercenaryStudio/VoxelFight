using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("OBJECTS/WEAPON"))]
public class Weapon : ScriptableObject {

    [SerializeField] AudioClip soundEffect;
    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAmmo;
    [SerializeField] float Range;

    #region Getters
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
        return Range;
    }
    public AudioClip GetWeaponSound()
    {
        return soundEffect;
    }
    #endregion
}
