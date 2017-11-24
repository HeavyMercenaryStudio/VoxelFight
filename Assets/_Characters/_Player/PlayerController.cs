using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

    [SerializeField] int playerNumber;
    [SerializeField] float maxHealth;

    

    public float GetHealthAsPercentage()
    {
        return currentHealth / maxHealth;
    }
    public void SetHealthAsPercentage(float percentage)
    {
        currentHealth = maxHealth * percentage/100.0f;
        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    Weapon weapon;
    PlayerGUI playerGUI;
    bool isDestroyed;
    float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
        playerGUI = GetComponent<PlayerGUI> ();
        weapon = GetComponent<Weapon> ();

    }

    private void Update()
    {
        if (isDestroyed) { return; }

        if (Input.GetButton ("Fire" + playerNumber))
            weapon.TryShoot ();

    }
    //private void UpdateWeaponGUI()
    //{
    //    //Default weapon has unlimited ammo
    //    if (currentWeapon.name == "Pistol")
    //        playerGUI.UpdateWeaponInfo (999);
    //    else
    //        playerGUI.UpdateWeaponInfo (currentWeapon.currentAmmo);

    //    weaponAudio.clip = soundMenager.GetWeaponSound (currentWeapon);
    //}

    public void TakeDamage(float damage)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        playerGUI.UpdateHealthInfo (GetHealthAsPercentage());

        if (currentHealth == 0)
            isDestroyed = true;
    }
    
    public bool IsDestroyed()
    {
        return isDestroyed;
    }
}
