using Characters;
using UnityEngine;
using Weapons;

namespace WorldObjects { 

    /// <summary>
    /// Use this for reapair player.
    /// </summary>
    public class RepairStation : MonoBehaviour, IDamageable
     {
        [SerializeField] float interruptTime; // disable time
        [SerializeField] int ammoPerSecond; // ammo to fill per second
        [SerializeField] float healthPercentagePerSecond; // health to fill per second

        float lastHealth; // 
        float interrupt; 

        Material material;
        void Start()
        {
            material = GetComponentInChildren<Renderer> ().material;
        }

        void Update()
        {
            if(interrupt >= 0) // count interupt to zero
                interrupt -= Time.deltaTime;
        }

        public bool IsDestroyed()
        {
            return false;
        }

        public void TakeDamage(float damage, GameObject bullet)
        {
            var proj = bullet.GetComponent<Projectile> (); // get projectile
            var shooterIsEnemy = proj.GetShooter ().GetComponent<Enemy> ();
            
            if (shooterIsEnemy) // if shooter was enemy..
            {
                interrupt = interruptTime;//disable heal function
                material.color = Color.red;// and change color to red
            }
        }

        void OnTriggerStay(Collider other)
        {
            var player = other.GetComponent<PlayerController> (); 
            if (player && Time.time > lastHealth && interrupt <= 0) //only every second if player stay on platform
            {
                player.HealMe (healthPercentagePerSecond); // ADD percentage health
                player.ReloadMe (ammoPerSecond); //add ammo

                lastHealth = Time.time + 1f; //heal every second
                material.color = Color.green; //set color of platform
            }
        }

        void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerController> ();
            if (player && interrupt <= 0)
                material.color = Color.white; //reset platform color
        }
    }
}
