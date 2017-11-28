using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionObjective : MonoBehaviour, IDamageable {

    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth;

    public delegate void OnObjectiveDestroyed();
    public static event OnObjectiveDestroyed notifyOnObjectiveDestroy;

    float currentHealth;
    bool isDestroyed;

    public float GetHealthAsPercentage()
    {
        return currentHealth / maxHealth;
    }
    public void SetHealthAsPercentage(float percentage)
    {
        currentHealth = maxHealth * percentage / 100.0f;
        UpdateHealthInfo (GetHealthAsPercentage ());
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthInfo(float healthAmount)
    {
        healthBar.fillAmount = healthAmount;
    }

    public void TakeDamage(float damage, GameObject bullet)
    {
        Destroy (bullet);

        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        UpdateHealthInfo (GetHealthAsPercentage ());

        if (currentHealth == 0)
        {
            notifyOnObjectiveDestroy ();
            isDestroyed = true;
        }
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }

}
