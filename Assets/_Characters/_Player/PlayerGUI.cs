using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {

    [SerializeField] Text ammoText;
    [SerializeField] Image healthBar;

    public void UpdateWeaponInfo(float ammoAmout)
    {
        ammoText.text = "Ammo: " + ammoAmout.ToString ();
    }

    public void UpdateHealthInfo(float healthAmount)
    {
        healthBar.fillAmount = healthAmount;
    }
   


}
