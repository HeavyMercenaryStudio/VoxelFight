using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Particular weapon
    /// </summary>
    public class Shotgun : Weapon
    {
        int shotgunBullets = 10; // amount of bullets to spawn
        int angle = 30; // area angle to spawn bullets

        int axisXdispersion = 2; // dispersion of X axis
        float anglePerBullet; //

        private new void Start()
        {
            base.Start ();
            anglePerBullet = angle / shotgunBullets; // calculate angle to shoot bullets
        }

        public override void Shoot()
        {
            MuzzleEffect (); // add muzzle effect
            
            gunEndPoint.localRotation = Quaternion.identity; //reset rotation...
            gunEndPoint.Rotate(gunEndPoint.up, -angle/2); //...

            for (int i = 0; i < shotgunBullets; i++) // spawn bulletes
            {
                gunEndPoint.Rotate (gunEndPoint.up, anglePerBullet); // rotate horizotnal axis
                gunEndPoint.Rotate (gunEndPoint.right, Random.Range(-axisXdispersion, axisXdispersion)); // rotate vertical axis

                GameObject bulet = Instantiate (Bullet, gunEndPoint.position, Quaternion.LookRotation(gunEndPoint.forward)) as GameObject;
                var proj = bulet.GetComponent<Projectile> ();
                proj.SetDamage (Damage);
                proj.SetDestroyRange (Range);
                proj.SetShooter (this.gameObject);
                proj.GetComponent<Rigidbody> ().velocity = gunEndPoint.forward * Random.Range(BulletSpeed/2, BulletSpeed);
            }
        }

        private void MuzzleEffect()
        {
            GameObject muzzle2 = Instantiate (Muzzle, gunEndPoint) as GameObject;
            Destroy (muzzle2, 0.15f);
        }
    }
}
