using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons { 
[CreateAssetMenu(menuName = "WeaponData")]
public class WeaponData : ScriptableObject {

        [SerializeField] float defaultSecondBetweenShoot;
        [SerializeField] int defaultMaxAmmo;
        [SerializeField] int defaultRange;
        [SerializeField] float defaultDamagePerBullet;

        [SerializeField] WeaponType weaponType;
        [SerializeField] float secondsBetweenShoot;
        [SerializeField] int maxAmmo;
        [SerializeField] float damagePerBullet;
        [SerializeField] float range;
        [SerializeField] int dispersion;
        [SerializeField] int projectileSpeed;
        [SerializeField] GameObject projectile;
        [SerializeField] GameObject muzzle;
        [SerializeField] bool available;
        [SerializeField] int weaponCost;

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
        public float DamagePerBullet
        {
            get
            {
                return damagePerBullet;
            }

            set
            {
                damagePerBullet = value;
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
        public GameObject Projectile
        {
            get
            {
                return projectile;
            }

            set
            {
                projectile = value;
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
        public int ProjectileSpeed
        {
            get
            {
                return projectileSpeed;
            }

            set
            {
                projectileSpeed = value;
            }
        }
        public WeaponType WeaponType
        {
            get
            {
                return weaponType;
            }
        }
        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                available = value;
            }
        }
        public int WeaponCost
        {
            get
            {
                return weaponCost;
            }

            set
            {
                weaponCost = value;
            }
        }

        public int DefaultMaxAmmo
        {
            get
            {
                return defaultMaxAmmo;
            }
        }
        public int DefaultRange
        {
            get
            {
                return defaultRange;
            }
        }
        public float DefaultDamagePerBullet
        {
            get
            {
                return defaultDamagePerBullet;
            }
        }
        public float DefaultSecondBetweenShoot
        {
            get
            {
                return defaultSecondBetweenShoot;
            }
        }
    }
}
