using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBotControler : RobotControler
{
    [SerializeField] Transform gunEndPoint1;

    public override void Shoot()
    {
        MuzzleEffect ();

        GameObject bulet = Instantiate (bullet, gunEndPoint1.position, Quaternion.identity) as GameObject;
        var proj = bulet.GetComponent<Projectile> ();
        proj.SetDamage (gunDamage);
        proj.SetDestroyRange (gunMaxRange);
        proj.SetShooter (this.gameObject);
        proj.GetComponent<Rigidbody> ().velocity = gunEndPoint1.forward * bulletSpeed;

        ////CAST RAY
        //Ray gun1Ray = new Ray (gunEndPoint1.position, gunEndPoint1.forward);
        //RaycastHit hit;

        //Debug.DrawRay (gun1Ray.origin, gun1Ray.direction * gunMaxRange);

        //if (Physics.Raycast (gun1Ray, out hit, gunMaxRange))
        //{
        //    var enemy = hit.collider.GetComponent<Enemy> ();
        //    if (enemy)
        //    {
        //        enemy.TakeDamage (gunDamage);
        //    }
        //}
    }

    private void MuzzleEffect()
    {
        GameObject muzzle2 = Instantiate (gunMuzzeFlash, gunEndPoint1) as GameObject;
        Destroy (muzzle2, 0.15f);
    }
}
