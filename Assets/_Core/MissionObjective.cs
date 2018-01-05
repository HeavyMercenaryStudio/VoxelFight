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
        [SerializeField] GameObject bloodPrefab; 

        public delegate void OnObjectiveDestroyed();
        public static event OnObjectiveDestroyed notifyOnObjectiveDestroy;

        float currentHealth; // current health 
        bool isDestroyed; 

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
            float damagedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth); // clamp heath between 0 and max

            UpdateHealthInfo (GetHealthAsPercentage ());

            var offset = this.GetComponent<Collider> ().bounds.extents.y; //instantiate hit particle
            GameObject blood1 = Instantiate (bloodPrefab, transform.position + new Vector3 (0, offset), bloodPrefab.transform.rotation);
            Destroy (blood1, 5f); 

            if (currentHealth == 0) // is something destory object
            {
                notifyOnObjectiveDestroy (); //notify it
                isDestroyed = true; // and set flag
            }
        }

        public bool IsDestroyed()
        {
            return isDestroyed;
        }

    }

}