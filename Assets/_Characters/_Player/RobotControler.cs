using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotControler : MonoBehaviour {

    public bool isDead;

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
    

    float lastShoot;
    float currentHealth;
    int currentAmmo;

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
        playerGUI.UpdateWeaponInfo (currentAmmo);
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
        weaponAudioSource.clip = weaponAudioClip;

         playerGUI.UpdateWeaponInfo (currentAmmo);
    }

    private void Update()
    {
        if (isDead) { return; }

        if (Input.GetButton ("Fire" + playerNumber))
            TryShoot ();

        if (Input.GetKey (KeyCode.X))
            TakeDamage (5);

    }

    private void TryShoot()
    {
        //Handle attack speed
        if (Time.time > lastShoot + shootingSpeed)
        {
            if (currentAmmo != 0)
            {
                currentAmmo--;
                playerGUI.UpdateWeaponInfo (currentAmmo);


                if (!weaponAudioSource.isPlaying)
                    weaponAudioSource.Play ();

                Shoot ();

                lastShoot = Time.time;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());

        if (currentHealth == 0)
            isDead = true;
    }

}
