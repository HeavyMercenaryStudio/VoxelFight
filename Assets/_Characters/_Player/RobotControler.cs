﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotControler : MonoBehaviour, IDamageable {

    public delegate void OnPlayerDead();
    public static event OnPlayerDead notifyPlayerDead;

    [SerializeField] int playerNumber;

    //Robot Atributes
    [SerializeField] float maxHealth;
    [SerializeField] float shootingSpeed;
    [SerializeField] int maxAmmunition;

    [SerializeField] protected float gunMaxRange;
    [SerializeField] protected float gunDamage;
    [SerializeField] protected float bulletSpeed;

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject gunMuzzeFlash;
    [SerializeField] AudioClip weaponAudioClip;

    AudioSource weaponAudioSource;

    protected Animator shootAnimator;

    float lastShoot;
    float currentHealth;
    int currentAmmo;
    bool isDestroyed;

    protected PlayerGUI playerGUI;

    public abstract void Shoot();

    public float GetHealthAsPercentage()
    {
        return currentHealth / maxHealth;
    }
    public void SetHealthAsPercentage(float percentage)
    {
        currentHealth = maxHealth * percentage / 100.0f;
        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());
    }
    public void FillPlayerAmmunition()
    {
        currentAmmo = maxAmmunition;
        playerGUI.UpdateAmmoText (currentAmmo);
    }
    public void SetDamageUp(float damage)
    {
        gunDamage += damage;
    }
    public void SetMaxHealth(float health)
    {
        maxHealth += health;
    }
    public void SetMaxAmmo(int ammo)
    {
        maxAmmunition += ammo;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmunition;

        playerGUI = GetComponent<PlayerGUI> ();
        weaponAudioSource = GetComponentInChildren<AudioSource> ();
        shootAnimator = transform.Find ("Gun").GetComponent<Animator> ();
        weaponAudioSource.clip = weaponAudioClip;

         playerGUI.UpdateAmmoText (currentAmmo);
    }

    private void Update()
    {
        if (isDestroyed) { return; }

        if (Input.GetButton ("Fire" + playerNumber))
            TryShoot ();

    }

    private void TryShoot()
    {
        //Handle attack speed
        if (Time.time > lastShoot + shootingSpeed)
        {
            if (currentAmmo != 0)
            {
                currentAmmo--;
                playerGUI.UpdateAmmoText (currentAmmo);


                if (!weaponAudioSource.isPlaying)
                    weaponAudioSource.Play ();

                Shoot ();

                lastShoot = Time.time;
            }
        }
    }

    public void TakeDamage(float damage, GameObject bullet)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());

        if (currentHealth == 0)
        {
            isDestroyed = true;
            notifyPlayerDead ();
        }
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }

    public void SetDestroyed(bool v)
    {
        isDestroyed = v;
    }
}