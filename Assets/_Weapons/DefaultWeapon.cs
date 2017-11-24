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
        damage = 5;

        //soundEffect = ?;
    }
}
