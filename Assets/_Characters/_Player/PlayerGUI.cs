using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {

    [SerializeField] Text ammoText;
    [SerializeField] Text weaponName;
    [SerializeField] Image healthBar;

    public void UpdateWeaponInfo(float ammoAmout, string name)
    {
        ammoText.text = "Ammo: " + ammoAmout.ToString ();
        weaponName.text = name;
    }

    public void UpdateHealthInfo(float healthAmount)
    {
        healthBar.fillAmount = healthAmount;
    }
   


}
