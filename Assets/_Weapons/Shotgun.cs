using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    public Shotgun()
    {
        name = "Shotgun";

        dispersion = 25;
        range = 15;
        damage = 10;

        //soundEffect = ?;
    }
}
