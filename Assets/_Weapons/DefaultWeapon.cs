using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    public DefaultWeapon()
    {
        name = "Pistol";

        dispersion = 5;
        range = 25;
        maxAmmo = 1;
        attackSpeed = 0.5f;
        damage = 5;

        //soundEffect = ?;
    }
}
