using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Lase weapon
    /// </summary>
    public class Laser : Weapon
    {
        protected int INTERACTIVE_OBJECT_LAYER = 11;

        //Active laser Line if fire button is clicked
        public override void SetFireButtonDown(bool value)
        {
            base.SetFireButtonDown(value);
            bool click = GetFireButtonDown();

            if (click)
                laserLine.gameObject.SetActive(true);
            else
                laserLine.gameObject.SetActive(false);

        }

        LineRenderer laserLine = null;
        private new void Start()
        {
            base.Start();
            GameObject laser = Instantiate(Bullet, gunEndPoint.position, Quaternion.identity) as GameObject;
            laserLine = laser.GetComponent<LineRenderer>();
            laserLine.gameObject.SetActive(false);
        }

        public override void Shoot()
        {
            MuzzleEffect ();
            RaycastHit hit;

            //cast raycast every attack speed second
            laserLine.SetPosition(0, gunEndPoint.position);
            if (Physics.Raycast(gunEndPoint.position, gunEndPoint.forward, out hit, Range, ~(1 << INTERACTIVE_OBJECT_LAYER)))
            {
                Debug.Log(hit.collider.gameObject.layer);
                laserLine.SetPosition(1, hit.point); //set line render point to hit position

                Component destroyable = hit.collider.GetComponent(typeof(IDamageable)); // if hit colider is destroyable 
                if (destroyable)
                { // if target is destroy able
                    (destroyable as IDamageable).TakeDamage(Damage, laserLine.gameObject);//hit him
                }
            }
            else
            {
                laserLine.SetPosition(1, gunEndPoint.position + (gunEndPoint.forward * Range)); // stop laser at range
            }
        }

        private void MuzzleEffect()
        {
            GameObject muzzle2 = Instantiate (Muzzle, gunEndPoint) as GameObject;
            Destroy (muzzle2, 0.15f);
        }
    }
}
