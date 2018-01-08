using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CameraUI { 

    /// <summary>
    /// Player interfejs in game 
    /// </summary>
    public class PlayerGUI : MonoBehaviour {

        [SerializeField] Text ammoText; // player canvas ammo text 
        [SerializeField] Image healthBar;// bar of player health
        [SerializeField] Image energyBar;// bar of player energy

        public void UpdateAmmoText(float ammoAmout) // update player ammo text
        {
            ammoText.text = "Ammo: " + ammoAmout.ToString ();
        }

        public void UpdateHealthInfo(float healthAmount)//update player health bar
        {
            healthBar.fillAmount = healthAmount;
        }

        public void UpdateEnergyInfo(float energyAmount)//update player health bar
        {
            energyBar.fillAmount = energyAmount;
        }
    }
}
