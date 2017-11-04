using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Weapon weapon;
    [SerializeField] GameObject projectileSpawnPoint;

    int currentWeaponAmmo;

    public delegate void OnShoot(float ammo);
    public OnShoot notifyOnShoot;

    float lastShoot;
    private void Start()
    {
        currentWeaponAmmo = weapon.GetWeaponMaxAmmo ();
    }
    private void Update()
    {
        if (Input.GetKey (KeyCode.Mouse0))
            Shoot ();
    }

    void Shoot()
    {
        //Handle attack speed
        if(Time.time > lastShoot + weapon.GetWeaponSpeed ())
        {
            if (currentWeaponAmmo == 0) return;
            UpdateAmmo ();
            SpawnProjectile ();
           
            lastShoot = Time.time;
        }
    }

    private void UpdateAmmo()
    {
        //Default weapon has unlimited ammo
        if (weapon.GetWeaponProjectile ().name.Contains ("Default")){
            notifyOnShoot (999);
        }
        else{
            currentWeaponAmmo--;
            notifyOnShoot (currentWeaponAmmo);
        }

        
    }

    private void SpawnProjectile()
    {
        //Spawn projectile
        GameObject newProjectile = Instantiate (weapon.GetWeaponProjectile(), 
                                                projectileSpawnPoint.transform.position,
                                                Quaternion.identity) as GameObject;

        Projectile projectileComponent = newProjectile.GetComponent<Projectile> ();

        //Set shooter,damage,range,projectilespeed
        projectileComponent.SetShooter (this.gameObject);
        projectileComponent.SetDamage (weapon.GetWeaponDamage ());
        projectileComponent.SetDestroyRange (weapon.GetWeaponRange ());
        projectileComponent.SetProjectileSpeed (weapon.GetWeaponProjectileSpeed ());

        //Add velocity to rpojectile
        projectileComponent.GetComponent<Rigidbody> ().velocity = this.transform.forward * weapon.GetWeaponProjectileSpeed ();
       
    }
}
