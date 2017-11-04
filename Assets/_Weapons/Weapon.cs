using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("OBJECTS/WEAPON"))]
public class Weapon : ScriptableObject {

    [SerializeField] GameObject weaponProjectile;

    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAmmo;
    [SerializeField] float Range;
    [SerializeField] float projectileSpeed;

    #region Getters
    public GameObject GetWeaponProjectile(){
        return weaponProjectile;
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
        return Range;
    }
    public float GetWeaponProjectileSpeed()
    {
        return projectileSpeed;
    }

    #endregion
}
