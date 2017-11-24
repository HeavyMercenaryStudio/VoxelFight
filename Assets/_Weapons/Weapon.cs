using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Weapon : MonoBehaviour{
    
    [SerializeField] float secondsBetweenShoot;
    [SerializeField] int maxAmmo;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected int dispersion;
    [SerializeField] protected int bulletSpeed;

    [SerializeField] AudioClip soundEffect;
    [SerializeField] protected Transform gunEndPoint;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject muzzle;

    int currentAmmo;
    float lastShoot;

    private void Start()
    {
        currentAmmo = maxAmmo;
        // UpdateWeaponGUI ();
    }

    public  void TryShoot()
    {
        //Handle attack speed
        if (Time.time > lastShoot + secondsBetweenShoot)
        {
            if (currentAmmo != 0)
            {
                UpdateAmmo ();
                Shoot ();

                lastShoot = Time.time;
            }
        }
    }

    public virtual void Shoot()
    {
        
    }

    private void UpdateAmmo()
    {
            currentAmmo--;
    }

}
