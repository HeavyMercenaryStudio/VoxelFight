using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBotControler : RobotControler {

    [SerializeField] Transform gunEndPoint1;
    [SerializeField] Transform gunEndPoint2;

   
    public override void Shoot()
    {
        MuzzleEffect ();

        shootAnimator.SetTrigger ("Shoot");

        //GUN 1
        GameObject bulet = Instantiate (bullet, gunEndPoint1.position, Quaternion.identity) as GameObject;
        var proj = bulet.GetComponent<Projectile> ();
        SetProjectile (proj);
        proj.GetComponent<Rigidbody> ().velocity = gunEndPoint1.forward * bulletSpeed;

        //GUN 2
        bulet = Instantiate (bullet, gunEndPoint2.position, Quaternion.identity) as GameObject;
        proj = bulet.GetComponent<Projectile> ();
        SetProjectile (proj);
        proj.GetComponent<Rigidbody> ().velocity = gunEndPoint2.forward * bulletSpeed;

    }

    private void SetProjectile(Projectile proj)
    {
        proj.SetDamage (gunDamage);
        proj.SetDestroyRange (gunMaxRange);
        proj.SetShooter (this.gameObject);
        
    }

    private void MuzzleEffect()
    {
        GameObject muzzle1 = Instantiate (gunMuzzeFlash, gunEndPoint1) as GameObject;
        Destroy (muzzle1, 0.15f);

        GameObject muzzle2 = Instantiate (gunMuzzeFlash, gunEndPoint2) as GameObject;
        Destroy (muzzle2, 0.15f);
    }
}
