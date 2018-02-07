using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorldObjects{
    
    /// <summary>
    /// Describe mission object.
    /// </summary>
    public class MissionObjective : MonoBehaviour, IDamageable {

        [SerializeField] Image healthBar; // health bar
        [SerializeField] float maxHealth; // max health of object
        [SerializeField] float healthRepairPercentageAmount;

        [SerializeField] GameObject barrier;
        //        public delegate void OnObjectiveDestroyed();
        //      public static event OnObjectiveDestroyed notifyOnObjectiveDestroy;

        float currentHealth; // current health 
        bool isDestroyed;
        bool waitForRepair;

        public float GetHealthAsPercentage()
        {
            return currentHealth / maxHealth;
        }
        public void SetHealthAsPercentage(float percentage)
        {
            currentHealth = maxHealth * percentage / 100.0f;
            UpdateHealthInfo (GetHealthAsPercentage ());
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void UpdateHealthInfo(float healthAmount)
        {
            healthBar.fillAmount = healthAmount;
        }

        public void TakeDamage(float damage, GameObject bullet)
        {
            if (isDestroyed) return;
            if (bullet.layer == Layers.PLAYER) return; //Player layer

            float damagedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth); // clamp heath between 0 and max

            UpdateHealthInfo (GetHealthAsPercentage ());

            if(!waitForRepair)
                StartCoroutine(DelayUntilRepair());

            if (currentHealth == 0) // is something destory object
            {
                // notifyOnObjectiveDestroy (); //notify it
                isDestroyed = true; // and set flag

                barrier.SetActive(false); // disable barier
                healthBar.GetComponentInParent<Canvas>().gameObject.SetActive(false); // disable healthinfo

                GetComponent<SphereCollider>().enabled = false; //disable colider
                GetComponent<RepairStation>().enabled = false; // disable repair function

                var renderers = GetComponentsInChildren<Renderer>(); // disable colors
                foreach (Renderer r in renderers){
                    r.material.color = Color.red;
                }

            }
        }

        IEnumerator DelayUntilRepair()
        {
            waitForRepair = true;
            yield return new WaitForSeconds(5f);
            waitForRepair = false;

            StartCoroutine(RepairSelf());
        }

        IEnumerator RepairSelf()
        {
            while (!waitForRepair)
            {
                yield return new WaitForSeconds(0.25f);

                var heal = GetHealthAsPercentage() * 100f + healthRepairPercentageAmount;
                SetHealthAsPercentage(heal);
            }
        }

        public bool IsDestroyed()
        {
            return isDestroyed;
        }

    }

}