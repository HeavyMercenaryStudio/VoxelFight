using Audio;
using CameraUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Shields;
using Data;

namespace Characters { 

    /// <summary>
    /// Control player behaviors
    /// </summary>
    public class PlayerController : MonoBehaviour, IDamageable {

        [SerializeField] int playerNumber; // player nubmer
        [SerializeField] float maxHealth; // max player health
        [SerializeField] GameObject bloodPrefab; // after hit prefab 

        public delegate void OnPlayerDead();
        public static event OnPlayerDead notifyPlayerDead;
        private bool isPlayerDisabled;

        public float GetHealthAsPercentage()
        {
            return currentHealth / maxHealth;
        } 
        public void SetHealthAsPercentage(float percentage)
        {
            currentHealth = maxHealth * percentage/100.0f;
            playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());
        }
        public void HealMe(float healPercentageAmount)
        {
            float healing = currentHealth + (maxHealth * healPercentageAmount / 100.0f);
            currentHealth = Mathf.Clamp (healing, 0, maxHealth);
            playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());

        } // heal player by amount of health percentage
        public void ReloadMe(int ammoPerSecond)
        {
            Weapon.Realod (ammoPerSecond);
            playerGUI.UpdateAmmoText (Weapon.GetCurrentAmmo());
        } // fill player ammo 
        public void RenownEnergy(float value)
        {
            shield.Renown(value);
            playerGUI.UpdateAmmoText(Weapon.GetCurrentAmmo());
            playerGUI.UpdateEnergyInfo(shield.CurrentEnergy);
        } // fill player ammo 

        public int GetPlayerNumber()
        {
            return playerNumber;
        }

        Weapon weapon; // current weapon used by player
        Shield shield; // current shield used by player

        PlayerGUI playerGUI; // gui of player
        bool isDestroyed; // FLAG if players is destoryed 
        float currentHealth; // current health of player

        float bloodDestroyTime = 5f;

        public Weapon Weapon
        {
            get
            {
                return weapon;
            }

            set
            {
                weapon = value;
            }
        }
        public Shield Shield
        {
            get
            {
                return shield;
            }

            set
            {
                shield = value;
            }
        }

        public bool IsPlayerDisabled
        {
            get
            {
                return isPlayerDisabled;
            }

            set
            {
                isPlayerDisabled = value;
                shield.SetFireButtonDown(false);
                weapon.SetFireButtonDown(false);
            }
        }

        private void Start()
        {
            playerGUI = GetComponent<PlayerGUI> ();
            Weapon = GetComponent<Weapon> ();
            Shield = GetComponent<Shield>();

            SetHealthAsPercentage(WorldData.PlayerHealth[playerNumber]);
            StartCoroutine(UpdateGUI());

            weapon.soundEnabled = true;
        }
        IEnumerator UpdateGUI()
        {
            yield return new WaitForSeconds(0.1f);
            playerGUI.UpdateAmmoText(Weapon.GetCurrentAmmo());
            playerGUI.UpdateHealthInfo(GetHealthAsPercentage());
        }

        private void Update()
        {
            if (isPlayerDisabled) return;
            if (isDestroyed) { return; } // if player is destroyed dont take any action..

            if (Input.GetButton("Fire" + playerNumber)) // if FIRE button clicked...
            {
                if (Weapon.TryShoot()) // try to shoot...
                    UpdateAmmo(); // if sucess update ammo 

                Weapon.SetFireButtonDown(true);
            }
            else if (Input.GetButtonUp("Fire" + playerNumber))
                Weapon.SetFireButtonDown(false);

            if (Input.GetButton("Defense" + playerNumber)) // if Defense button clicked...
            {
                playerGUI.UpdateEnergyInfo(Shield.CurrentEnergy);

                Shield.DefenseUp();
                Shield.SetFireButtonDown(true);
            }
            else if (Input.GetButtonUp("Defense" + playerNumber))
                Shield.SetFireButtonDown(false);

            if(!Shield.Active)
                playerGUI.UpdateEnergyInfo(Shield.CurrentEnergy);
        }
        public void TakeDamage(float damage, GameObject bullet) // take damage atfer hit
        {
            if (isDestroyed) return; // if is destroy return...

            float damagedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth); // clamp current health between 0 and max health

            playerGUI.UpdateHealthInfo (GetHealthAsPercentage()); // update health bar

            //Spawn blood particle
            var offset = this.GetComponent<Collider> ().bounds.extents.y; 
            GameObject blood1 = Instantiate (bloodPrefab, transform.position + new Vector3 (0, offset), bloodPrefab.transform.rotation);
            Destroy (blood1, bloodDestroyTime);  //and destory it

            if (currentHealth == 0) // if player hit points equeal zero 
            {
                isDestroyed = true; // set flag 
                notifyPlayerDead (); //and notify player dead
            }
        }
    
        public bool IsDestroyed()
        {
            return isDestroyed;
        }

        private void UpdateAmmo()
        {
            playerGUI.UpdateAmmoText (Weapon.GetCurrentAmmo ()); // if its player
        }

        private void OnDestroy()
        {
            PlayerDatabase.Instance.PlayersItemList[playerNumber - 1].PlayerEquipedWeapon.Ammo = 
            GetComponent<Weapons.Weapon>().GetCurrentAmmo();
        }
    }
}
