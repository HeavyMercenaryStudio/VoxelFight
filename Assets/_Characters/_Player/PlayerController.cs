using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

    [SerializeField] int playerNumber;
    public bool isDead;

    [SerializeField] float maxHealth;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] List<Weapon> playerWeapons;
    [SerializeField] GameObject weaponFireSlot;
    [SerializeField] GameObject shootLine;

    [SerializeField] SoundMenager soundMenager;
    AudioSource weaponAudio;
    float lastShoot;
    float currentHealth;

    PlayerGUI playerGUI;

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

    private void Start()
    {
        currentHealth = maxHealth;
        playerGUI = GetComponent<PlayerGUI> ();
      
        SetupWeapon ();
    }

    private void SetupWeapon()
    {
        playerWeapons = new List<Weapon> ();
        playerWeapons.Add (new DefaultWeapon());
        playerWeapons.Add (new Riffle());
        playerWeapons.Add (new SniperRiffle ());
        playerWeapons.Add (new Shotgun ());
        currentWeapon = playerWeapons[0];

        //Get componentes
        weaponAudio = GetComponentInChildren<AudioSource> ();
        weaponAudio.clip = soundMenager.GetWeaponSound (currentWeapon);

        //setup ammo foreach weapon
        for (int i = 0; i < playerWeapons.Count; i++)
            playerWeapons[i].currentAmmo = playerWeapons[i].GetWeaponMaxAmmo ();

        //update gui
        UpdateWeaponGUI ();
    }

    private void Update()
    {
        if (isDead) { return; }

        if (Input.GetButton ("Fire" + playerNumber))
            TryShoot ();
        if (Input.GetButtonDown ("WeaponUp" + playerNumber))
            ChangeWeaponUp ();
        else if (Input.GetButtonDown ("WeaponDown" + playerNumber))
            ChangeWeaponDown ();

        if (Input.GetKey (KeyCode.X))
            TakeDamage (5);

    }

    private void TryShoot()
    {
        //Handle attack speed
        if(Time.time > lastShoot + currentWeapon.GetWeaponSpeed ())
        {
            if (currentWeapon.currentAmmo != 0)
            {
                UpdateAmmo ();
                Shoot ();

                lastShoot = Time.time;
            }
        }
    }
    private void UpdateAmmo()
    {
        if(!currentWeapon.GetType().Name.Contains ("Default"))
        { 
            currentWeapon.currentAmmo--;
            playerGUI.UpdateWeaponInfo (currentWeapon.currentAmmo);
        }

    }
    private void Shoot()
    {
        var weaponTransform = weaponFireSlot.transform;

        //ADD Dispersion
        weaponTransform.localRotation = Quaternion.identity; //reset transform
        var wD = currentWeapon.GetWeaponDispersion ();
        int n = UnityEngine.Random.Range (-wD, wD); //random dispersion for weapon
        weaponTransform.RotateAround (weaponTransform.position, weaponTransform.up, n);

        //CAST RAY
        Ray ray = new Ray (weaponTransform.position, weaponTransform.forward);
        RaycastHit hit;

        Vector3 hitPointPosition = weaponTransform.forward * currentWeapon.GetWeaponRange () + weaponTransform.position;

        if (Physics.Raycast (ray, out hit, currentWeapon.GetWeaponRange ()))
        {
            var enemy = hit.collider.GetComponent<Enemy> ();
            if (enemy)
            {
                enemy.TakeDamage (currentWeapon.GetWeaponDamage ());
            }

            hitPointPosition = hit.point;
        }

        Effects (hitPointPosition);
    }
    private void Effects(Vector3 hitPointPosition)
    {
        weaponAudio.Play ();

        GameObject line = Instantiate (shootLine) as GameObject;
        LineRenderer lineRender = line.GetComponent<LineRenderer> ();
        lineRender.SetPosition (0, weaponFireSlot.transform.position);
        lineRender.SetPosition (1, hitPointPosition);

        Destroy (line, 0.1f);
    }

    private void ChangeWeaponUp()
    {
        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i].Equals (currentWeapon))
            {
                if (i == playerWeapons.Count - 1)
                    currentWeapon = playerWeapons[0];
                else
                    currentWeapon = playerWeapons[i + 1];

                UpdateWeaponGUI ();
                break;
            }
        }
    }
    private void ChangeWeaponDown()
    {
        for (int i = playerWeapons.Count - 1; i >= 0; i--)
        {
            if (playerWeapons[i].Equals (currentWeapon))
            {
                if (i == 0)
                    currentWeapon = playerWeapons[playerWeapons.Count - 1];
                else
                    currentWeapon = playerWeapons[i - 1];

                UpdateWeaponGUI ();
                break;
            }
        }
    }
    private void UpdateWeaponGUI()
    {
        //Default weapon has unlimited ammo
        if (currentWeapon.name == "Pistol")
            playerGUI.UpdateWeaponInfo (999);
        else
            playerGUI.UpdateWeaponInfo (currentWeapon.currentAmmo);

        weaponAudio.clip = soundMenager.GetWeaponSound (currentWeapon);
    }

    public void TakeDamage(float damage)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        playerGUI.UpdateHealthInfo (GetHealthAsPercentage());

        if (currentHealth == 0)
            isDead = true;
    }
    
}
