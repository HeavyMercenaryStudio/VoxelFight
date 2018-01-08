using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shields { 
    public class Shield : MonoBehaviour {

        [SerializeField] float maxEnergy;
        [SerializeField] GameObject shieldPrefab;

        float currentEnergy;
        float energyLostPerSecond = 0.5f;
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
            if (currentEnergy == 0) { 
                shield.SetActive(false);
                return;
            }


            shield.SetActive(true);
            var energyDown = currentEnergy - energyLostPerSecond;
            currentEnergy = Mathf.Clamp(energyDown, 0, MaxEnergy);
        }
        private void OnDestroy()
        {
            var shieldComp = shield.GetComponent<ShieldObject>();
            shieldComp.notifyShieldHit -= ShieldHitted;
        }
    }
}
