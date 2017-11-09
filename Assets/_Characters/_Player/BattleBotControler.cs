﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBotControler : RobotControler {

    [SerializeField] Transform gunEndPoint1;
    [SerializeField] Transform gunEndPoint2;

   
    public override void Shoot()
    {
        MuzzleEffect ();

        //CAST RAY
        Ray gun1Ray = new Ray (gunEndPoint1.position, gunEndPoint1.forward);
        Ray gun2Ray = new Ray (gunEndPoint2.position, gunEndPoint2.forward);
        RaycastHit hit;

        Debug.DrawRay (gun1Ray.origin, gun1Ray.direction * gunMaxRange);
        Debug.DrawRay (gun2Ray.origin, gun2Ray.direction * gunMaxRange);

        if (Physics.Raycast (gun1Ray, out hit, gunMaxRange))
        {
            var enemy = hit.collider.GetComponent<Enemy> ();
            if (enemy)
            {
                enemy.TakeDamage (gunDamage);
            }
        }

        if (Physics.Raycast (gun2Ray, out hit, gunMaxRange))
        {
            var enemy = hit.collider.GetComponent<Enemy> ();
            if (enemy)
            {
                enemy.TakeDamage (gunDamage);
            }
        }


    }

    private void MuzzleEffect()
    {
        GameObject muzzle1 = Instantiate (gunMuzzeFlash, gunEndPoint1) as GameObject;
        Destroy (muzzle1, 0.15f);

        GameObject muzzle2 = Instantiate (gunMuzzeFlash, gunEndPoint2) as GameObject;
        Destroy (muzzle2, 0.15f);
    }
}
