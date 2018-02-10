using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Riffle weapon.
    /// </summary>
    public class RocketLuncher : Weapon
    {
        [SerializeField] float explosionRadius = 8;
   
        public override void Shoot()
        {
            MuzzleEffect ();

            gunEndPoint.localRotation = Quaternion.identity;
            GameObject bulet = Instantiate (Bullet, gunEndPoint.position, Quaternion.identity) as GameObject;
            var proj = bulet.GetComponent<RocketProjectile> ();
            proj.SetDamage (Damage);
            proj.SetDestroyRange (Range);
            proj.SetShooter (this.gameObject);
            proj.SetExplosionRadius(explosionRadius);
            proj.GetComponent<Rigidbody> ().velocity = gunEndPoint.forward * BulletSpeed;

        }

        private void MuzzleEffect()
        {
            GameObject muzzle2 = Instantiate (Muzzle, gunEndPoint) as GameObject;
            Destroy (muzzle2, 0.15f);
        }
    }
}
