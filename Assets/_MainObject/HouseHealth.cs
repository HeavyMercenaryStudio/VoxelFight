using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHealth : MonoBehaviour {

    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead;


    HouseGUI houseGUI;

    public float GetHealthAsPercentage()
    {
        return currentHealth / maxHealth;
    }
    public void SetHealthAsPercentage(float percentage)
    {
        currentHealth = maxHealth * percentage / 100.0f;
        houseGUI.UpdateHealthInfo(GetHealthAsPercentage());
    }

    private void Start()
    {
        currentHealth = maxHealth;
        houseGUI = GetComponent<HouseGUI>();
    }

    public void TakeDamage(float damage)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp(damagedHealth, 0, maxHealth);

        houseGUI.UpdateHealthInfo(GetHealthAsPercentage());

        if (currentHealth == 0)
            isDead = true;
    }



}
