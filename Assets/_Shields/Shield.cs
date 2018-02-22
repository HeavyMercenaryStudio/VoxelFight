using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shields { 
    public class Shield : MonoBehaviour {

        [SerializeField] float maxEnergy;
        [SerializeField] GameObject shieldPrefab;

        public string Name = "SHIELD";

        float currentEnergy;
        float energyLostPerSecond = 0.25f;
        float energyGetPerSecond = 0.5f;
        GameObject shield;

        bool fireButtonDown; // if fire button is clicked ///
        public float CurrentEnergy
        {
            get
            {
                return currentEnergy/MaxEnergy;
            }
        }
        public float GetCurrentEnergy()
        {
            return currentEnergy;
        }

        bool active;
        public bool Active
        {
            get
            {
                return active;
            }
        }
        public float MaxEnergy
        {
            get
            {
                return maxEnergy;
            }

            set
            {
                maxEnergy = value;
            }
        }
        public GameObject ShieldPrefab
        {
            get
            {
                return shieldPrefab;
            }

            set
            {
                shieldPrefab = value;
            }
        }

        public virtual void SetFireButtonDown(bool value)
        {
            if (!shield) return;

            fireButtonDown = value;

            if (fireButtonDown && currentEnergy != 0)
            {
                shield.SetActive(true);
                active = true;
            }
            else
            {
                shield.SetActive(false);
                active = false;
            }
        }
        public bool GetFireButtonDown()
        {
            return fireButtonDown;
        }

        public void Renown(float value)
        {
            float energy = currentEnergy + (value / 100f * maxEnergy);
            currentEnergy = Mathf.Clamp(energy, 0, maxEnergy);
        }

        // Use this for initialization
        public void Start ()
        {
            currentEnergy = MaxEnergy;

            SpawnShieldObject();
            SetListener();
        }

        private void SetListener()
        {
            var shieldComp = shield.GetComponent<ShieldObject>();
            shieldComp.notifyShieldHit += ShieldHitted;
        }
        private void SpawnShieldObject()
        {
            shield = Instantiate(ShieldPrefab, transform);
            shield.SetActive(false);
        }

        private void Update()
        {
            if (!active)
            {
                RenownEnergy();
            }
        }
        private void RenownEnergy()
        {
            var gettedEnergy = currentEnergy + energyGetPerSecond;
            currentEnergy = Mathf.Clamp(gettedEnergy, 0, MaxEnergy);
        }

        public virtual void ShieldHitted(GameObject projectile)
        {

        }
        public virtual void DefenseUp()
        {
            if (!shield) return;

            if (currentEnergy == 0) { 
                shield.SetActive(false);
                return;
            }


            if(shield) shield.SetActive(true);
            var energyDown = currentEnergy - energyLostPerSecond;
            currentEnergy = Mathf.Clamp(energyDown, 0, MaxEnergy);
        }
        private void OnDestroy()
        {
            Destroy(shield);
            SetFireButtonDown(false);
            var shieldComp = shield.GetComponent<ShieldObject>();
            shieldComp.notifyShieldHit -= ShieldHitted;
        }
    }
}
