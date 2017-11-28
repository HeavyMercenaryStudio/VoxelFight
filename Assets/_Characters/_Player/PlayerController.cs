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
    public void HealMe(float healPercentageAmount)
    {
        float healing = currentHealth + (maxHealth * healPercentageAmount / 100.0f);
        currentHealth = Mathf.Clamp (healing, 0, maxHealth);
        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());

    }
    public void ReloadMe(int ammoPerSecond)
    {
        weapon.Realod (ammoPerSecond);
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

    public void TakeDamage(float damage, GameObject bullet)
    {
        Destroy (bullet);

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
