using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    public Shotgun()
    {
        name = "Shotgun";

        dispersion = 25;
        range = 15;
        maxAmmo = 200;
        attackSpeed = 0.2f;
        damage = 10;

        //soundEffect = ?;
    }
}
