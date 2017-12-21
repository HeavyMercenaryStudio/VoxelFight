using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Control weapon used by player or enemy
    /// </summary>
    public class Weapon : MonoBehaviour{
    
        [SerializeField] float secondsBetweenShoot; // how fast character shoot
        [SerializeField] int maxAmmo; // max ammo at start
        [SerializeField] protected float damage; // current damage
        [SerializeField] protected float range; // range of bullets
        [SerializeField] protected int dispersion; // dispersion of shoots
        [SerializeField] protected int bulletSpeed; // speed of bullet

        [SerializeField] protected Transform gunEndPoint; // spawn bullet at this position
        [SerializeField] protected GameObject bullet;  // bulet prefab
        [SerializeField] protected GameObject muzzle; // muzzle particle effect

        int currentAmmo;
        float lastShoot;
        
        public int GetCurrentAmmo(){
            return currentAmmo;
        }

        public void Realod(int ammo)
        {
            float addedAmmo = currentAmmo + ammo;
            currentAmmo = (int)Mathf.Clamp ((float)addedAmmo, 0, (float)maxAmmo);
        }
        public void Start()
        {
            currentAmmo = maxAmmo;
        }

        public bool TryShoot()
        {
            //Handle attack speed
            if (Time.time > lastShoot + secondsBetweenShoot)
            {
                if (currentAmmo != 0)
                {
                    Shoot ();
                    currentAmmo--;
                    lastShoot = Time.time;
                    return true; // shoot succesfull

                }
            }

            return false; // shoot failed
        }
        public virtual void Shoot()
        {
        
        }
        

    }
}
