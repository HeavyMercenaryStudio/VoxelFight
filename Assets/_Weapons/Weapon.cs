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
        [SerializeField] private float damage; // current damage
        [SerializeField] private float range; // range of bullets
        [SerializeField] private int dispersion; // dispersion of shoots
        [SerializeField] private int bulletSpeed; // speed of bullet

        protected Transform gunEndPoint; // spawn bullet at this position
        [SerializeField] private GameObject bullet;  // bulet prefab
        [SerializeField] private GameObject muzzle;  // muzzle effect prefab
        [SerializeField] protected Audio.SoundMenager audioClips; //sounds of player 

        int currentAmmo;
        float lastShoot;
        bool fireButtonDown; // if fire button is clicked ///

        public float SecondsBetweenShoot
        {
            get
            {
                return secondsBetweenShoot;
            }

            set
            {
                secondsBetweenShoot = value;
            }
        }
        public int MaxAmmo
        {
            get
            {
                return maxAmmo;
            }

            set
            {
                maxAmmo = value;
            }
        }
        public float Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }
        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }
        public int Dispersion
        {
            get
            {
                return dispersion;
            }

            set
            {
                dispersion = value;
            }
        }
        public int BulletSpeed
        {
            get
            {
                return bulletSpeed;
            }

            set
            {
                bulletSpeed = value;
            }
        }
        public GameObject Bullet
        {
            get
            {
                return bullet;
            }

            set
            {
                bullet = value;
            }
        }
        public GameObject Muzzle
        {
            get
            {
                return muzzle;
            }

            set
            {
                muzzle = value;
            }
        }

        public bool soundEnabled;
        public virtual void SetFireButtonDown(bool value){
            fireButtonDown = value;
        }
        public bool GetFireButtonDown(){
            return fireButtonDown;
        }

        public int GetCurrentAmmo(){
            return currentAmmo;
        }

        public void Realod(int ammoPercentage)
        {

            float addedAmmo = currentAmmo + (ammoPercentage/100f * maxAmmo);
            currentAmmo = (int)Mathf.Clamp ((float)addedAmmo, 0, (float)maxAmmo);
        }
        public void Start()
        {
            currentAmmo = maxAmmo;
            gunEndPoint = GetComponentInChildren<GunEndPointMark>().transform;

            audioClips = Resources.Load<Audio.SoundMenager>("WeaponSounds");
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
