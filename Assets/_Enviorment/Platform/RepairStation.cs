using Characters;
using System.Collections;
using UnityEngine;
using Weapons;

namespace WorldObjects { 

    /// <summary>
    /// Use this for reapair player.
    /// </summary>
    public class RepairStation : MonoBehaviour, IDamageable
     {
        [SerializeField] float interruptTime; // disable time
        [SerializeField] float healthPercentagePerSecond; // health to fill per second
        [SerializeField] Material materialToChange;
        [SerializeField] int repairCostPerSecond = 10;
        [SerializeField] GameObject popupText;

        float lastHealth; // 
        bool isInterruped;

        Canvas canvas;
        
        void Awake()
        {
            canvas = FindObjectOfType<CameraUI.GameGui>().GetComponentInChildren<Canvas>();
        }

        public bool IsDestroyed()
        {
            return false;
        }

        public void TakeDamage(float damage, GameObject bullet)
        {
            var proj = bullet.GetComponent<Projectile> (); // get projectile
            if (proj == null) return;

            var shooterIsEnemy = proj.GetShooter ().GetComponent<Enemy> ();
            if (shooterIsEnemy) // if shooter was enemy..
            {
                if(!isInterruped)
                    StartCoroutine(Interruped());
            }
        }

        IEnumerator Interruped()
        {
            isInterruped = true;
            materialToChange.color = Color.red;// and change color to red
            yield return new WaitForSeconds(interruptTime);
            isInterruped = false;
            materialToChange.color = Color.white;// and change color to red
        }

        void OnTriggerStay(Collider other)
        {
            var data = PlayerDatabase.Instance;
            if(data.PlayersCrystals <= 0){
                return;
            }

            var player = other.GetComponent<PlayerController> (); 
            if (player && Time.time > lastHealth && !isInterruped) //only every second if player stay on platform
            {
                player.HealMe(healthPercentagePerSecond); // ADD percentage health
                player.ReloadMe(); //add ammo
               // player.RenownEnergy(energyPercentagePerSecond);

                data.PlayersCrystals -= repairCostPerSecond;
                SpawnPopupText(other);

                lastHealth = Time.time + 2f; //heal every 2second
                materialToChange.color = Color.green; //set color of platform
            }
        }

        private void SpawnPopupText(Collider other)
        {
            GameObject popup = Instantiate(popupText, canvas.transform);
        }

        void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerController> ();
            if (player && !isInterruped)
                materialToChange.color = Color.white; //reset platform color
        }
    }
}
