using Audio;
using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Riffle weapon.
    /// </summary>
    public class RocketLuncher : Weapon
    {
        [SerializeField] float explosionRadius = 8;
   
        public void Start()
        {
            base.Start();
            Name = "ROCKET LUNCHER";
        }

        public override void Shoot()
        {
            MuzzleEffect ();

            if (AudioMenager.Instance != null && soundEnabled)
                AudioMenager.Instance.PlayClip(audioClips.GetRocketSoundClip());

            gunEndPoint.localRotation = Quaternion.identity;
            GameObject bulet = Instantiate (Bullet, gunEndPoint.position, Quaternion.identity) as GameObject;
            var proj = bulet.GetComponent<RocketProjectile> ();
            proj.SetDamage (Damage);
            proj.SetDestroyRange (Range);
            proj.SetShooter (this.gameObject);
            proj.SetExplosionRadius(explosionRadius);
            proj.GetComponent<Rigidbody> ().velocity = gunEndPoint.forward * BulletSpeed;
            proj.transform.rotation = Quaternion.LookRotation(gunEndPoint.forward);

        }

        private void MuzzleEffect()
        {
            GameObject muzzle2 = Instantiate (Muzzle, gunEndPoint) as GameObject;
            Destroy (muzzle2, 0.15f);
        }
    }
}
