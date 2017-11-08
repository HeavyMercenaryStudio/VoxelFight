using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : Weapon
{
    public Riffle()
    {
        name = "Riffle";

        dispersion = 10;
        range = 25;
        maxAmmo = 100;
        attackSpeed = 0.2f;
        damage = 5;

        //soundEffect = ?;
    }


}
