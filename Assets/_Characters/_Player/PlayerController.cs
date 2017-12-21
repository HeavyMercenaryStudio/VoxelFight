using Audio;
using CameraUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Characters { 

    /// <summary>
    /// Control player behaviors
    /// </summary>
    public class PlayerController : MonoBehaviour, IDamageable {

        [SerializeField] int playerNumber; // player nubmer
        [SerializeField] float maxHealth; // max player health
        [SerializeField] GameObject bloodPrefab; // after hit prefab 
        [SerializeField] SoundMenager audioClips; //sounds of player 

        public delegate void OnPlayerDead();
        public static event OnPlayerDead notifyPlayerDead;

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
            weapon.Realod (ammoPerSecond);
            playerGUI.UpdateAmmoText (weapon.GetCurrentAmmo());
        } // fill player ammo 

        public int GetPlayerNumber()
        {
            return playerNumber;
        }

        Weapon weapon; // current weapon used by player

        PlayerGUI playerGUI; // gui of player
        bool isDestroyed; // FLAG if players is destoryed 
        float currentHealth; // current health of player

        float bloodDestroyTime = 5f;

        private void Start()
        {
            currentHealth = maxHealth;
            playerGUI = GetComponent<PlayerGUI> ();
            weapon = GetComponent<Weapon> ();
        }

        private void Update()
        {
            if (isDestroyed) { return; } // if player is destroyed dont take any action..

            if (Input.GetButton ("Fire" + playerNumber)) // if FIRE button clicked...
            {
                
                if (weapon.TryShoot ()) // try to shoot...
                    UpdateAmmo (); // if sucess update ammo 
            }

        }

        public void TakeDamage(float damage, GameObject bullet) // take damage atfer hit
        {
            if (isDestroyed) return; // if is destroy return...

            Destroy (bullet);//destroy hit bullet after hit...

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
            if (AudioMenager.Instance != null) AudioMenager.Instance.PlayClip (audioClips.GetShootClip ());
            playerGUI.UpdateAmmoText (weapon.GetCurrentAmmo ()); // if its player
        }
    }
}
