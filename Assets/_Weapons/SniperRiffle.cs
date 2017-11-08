using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRiffle : Weapon
{

    public SniperRiffle()
    {
        name = "Sniper Riffle";

        dispersion = 0;
        range = 50;
        maxAmmo = 25;
        attackSpeed = 1f;
        damage = 25;

        //soundEffect = ?;

    }
}
