using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBotControler : RobotControler
{
    [SerializeField] Transform gunEndPoint1;

    public override void Shoot()
    {
        MuzzleEffect ();

        //CAST RAY
        Ray gun1Ray = new Ray (gunEndPoint1.position, gunEndPoint1.forward);
        RaycastHit hit;

        Debug.DrawRay (gun1Ray.origin, gun1Ray.direction * gunMaxRange);

        if (Physics.Raycast (gun1Ray, out hit, gunMaxRange))
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
        GameObject muzzle2 = Instantiate (gunMuzzeFlash, gunEndPoint1) as GameObject;
        Destroy (muzzle2, 0.15f);
    }
}
