using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SniperRiffle : Weapon
{

    public SniperRiffle()
    {
        name = "Sniper Riffle";

        dispersion = 0;
        range = 50;
        damage = 25;

        //soundEffect = ?;

    }
}
