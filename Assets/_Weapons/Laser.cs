using Audio;
using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Lase weapon
    /// </summary>
    public class Laser : Weapon
    {

        GameObject laserHitParticleEffect;

        //Active laser Line if fire button is clicked
        public override void SetFireButtonDown(bool value)
        {
            if (!laserLine) return;

            base.SetFireButtonDown(value);

            if (value && GetCurrentAmmo() > 0)
                laserLine.gameObject.SetActive(true);
            else
                laserLine.gameObject.SetActive(false);

        }

        LineRenderer laserLine = null;
        private new void Start()
        {
            base.Start();
            GameObject laser = Instantiate(Bullet, gunEndPoint.position, Quaternion.identity) as GameObject;
            gunEndPoint.localRotation = Quaternion.identity; 
            laserHitParticleEffect = laser.transform.GetChild(0).gameObject;
            laserLine = laser.GetComponent<LineRenderer>();
            laserLine.gameObject.SetActive(false);
        }

        public override void Shoot()
        {
            if (!laserLine) return;

            MuzzleEffect ();
            RaycastHit hit;

            if (AudioMenager.Instance != null && soundEnabled)
                AudioMenager.Instance.PlayClip(audioClips.GetLaserWeaponClip());

            //cast raycast every attack speed second
            laserLine.SetPosition(0, gunEndPoint.position);
            if (Physics.Raycast(gunEndPoint.position, gunEndPoint.forward, out hit, Range, ~(1 << Layers.INTERACTIVE_OBJECT)))
            {
                SetLasetPosition(hit.point); //set line render point to hit position

                Component destroyable = hit.collider.GetComponent(typeof(IDamageable)); // if hit colider is destroyable 
                if (destroyable)
                { // if target is destroy able
                    (destroyable as IDamageable).TakeDamage(Damage, laserLine.gameObject);//hit him
                }
            }
            else
            {
                SetLasetPosition(gunEndPoint.position + (gunEndPoint.forward * Range)); // stop laser at range
            }
        }

        private void SetLasetPosition(Vector3 position)
        {
            laserLine.SetPosition(1, position);
            laserHitParticleEffect.transform.position = position;
        }

        private void MuzzleEffect()
        {
            GameObject muzzle2 = Instantiate (Muzzle, gunEndPoint) as GameObject;
            Destroy (muzzle2, 0.15f);
        }

        void OnDestroy()
        {
            Destroy(laserLine);
        }
    }
}
