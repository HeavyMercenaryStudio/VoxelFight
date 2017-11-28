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
    PlayerGUI playerGui;

    public void Realod(int ammo)
    {
        float addedAmmo = currentAmmo + ammo;
        currentAmmo = (int)Mathf.Clamp ((float)addedAmmo, 0, (float)maxAmmo);
        playerGui.UpdateAmmoText (currentAmmo);
    }

    private void Start()
    {
        playerGui = GetComponent<PlayerGUI> ();

        currentAmmo = maxAmmo;
        if(playerGui) playerGui.UpdateAmmoText (currentAmmo);// if its player update gui
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
       if (playerGui) playerGui.UpdateAmmoText (currentAmmo); // if its player
    }

}
