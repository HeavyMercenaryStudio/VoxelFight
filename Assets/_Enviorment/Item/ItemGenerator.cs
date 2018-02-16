using UnityEngine;
using Weapons;
using Shields;
using System.IO;

namespace Items
{
    public static class ItemGenerator {

        public static GameObject LoadItemPrefab(string itemType)
        {
            var resource = Resources.LoadAll<GameObject>(itemType);
            int n = UnityEngine.Random.Range(0, resource.Length);
            return resource[n];
        }

        public static ItemData GenerateRandomItem()
        {
            int n = UnityEngine.Random.Range(0, 2);

            ItemData item = new ItemData();

            if (n==0)
                item = GenerateWeapon();
            else
                item = GenerateShield();

                return item;
        }

        private static ShieldData GenerateShield()
        {
            ShieldData shield = new ShieldData();
            ShieldType shieldType = (ShieldType)Random.Range(0, 4);
            shield.ShieldType = shieldType;
            switch (shieldType)
            {
                case ShieldType.AbsorbShield:
                    shield.Name = "ABSORBING SHIELD";
                    shield.ShieldPrefab = LoadItemPrefab("Shields/AbsorbShield");
                    break;
                case ShieldType.ReflectShield:
                    shield.Name = "REFLECTIVE SHIELD";
                    shield.ShieldPrefab = LoadItemPrefab("Shields/ReflectShield");
                    break;
                case ShieldType.HealShield:
                    shield.Name = "HEALING SHIELD";
                    shield.ShieldPrefab = LoadItemPrefab("Shields/HealShield");
                    break;
                case ShieldType.BoostShield:
                    shield.Name = "SPEED SHIELD";
                    shield.ShieldPrefab = LoadItemPrefab("Shields/BoostShield");
                    break;
            }
            shield.Energy = Random.Range(100, 250);
            return shield;
        }
        public static ShieldData DefaultShield()
        {
            ShieldData shield = new ShieldData();
            shield.ShieldType = ShieldType.AbsorbShield;
            shield.Energy = 100;
            shield.Name = "ABSORBING SHIELD";
            shield.ShieldPrefab = LoadItemPrefab("Shields/AbsorbShield");
            return shield;
        }
        private static WeaponData GenerateWeapon()
        {
            WeaponData weapon = new WeaponData();
            WeaponType weaponType = (WeaponType)Random.Range(0, 4);
            weapon.WeaponType = weaponType;

            switch (weaponType)
            {
                case WeaponType.Rifle:
                    weapon.Name = "RIFLE";
                    weapon.Ammo = Random.Range(1200, 2000); // TODO Consider about ranges of weapons
                    weapon.Damage = Random.Range(10, 20); // TODO Consider about ranges of weapons
                    weapon.Dispersion = Random.Range(0, 10); // TODO Consider about ranges of weapons
                    weapon.Range = Random.Range(10, 125); // TODO Consider about ranges of weapons
                    weapon.TimeBetweenShoot = Random.Range(0.1f, 0.3f); // TODO Consider about ranges of weapons
                    weapon.Speed = 75;
                    weapon.Projectile = LoadItemPrefab("Weapons/Rifle");
                    weapon.Muzzle = LoadItemPrefab("Weapons/Muzzle");
                    break;
                case WeaponType.Laser:
                    weapon.Name = "LASER";
                    weapon.Ammo = Random.Range(3000, 10000); // TODO Consider about ranges of weapons
                    weapon.Damage = Random.Range(0.5f, 5f); // TODO Consider about ranges of weapons
                    weapon.Dispersion = Random.Range(0, 0); // TODO Consider about ranges of weapons
                    weapon.Range = Random.Range(10, 125); // TODO Consider about ranges of weapons
                    weapon.TimeBetweenShoot = Random.Range(0.01f, 0.04f); // TODO Consider about ranges of weapons
                    weapon.Speed = 0;
                    weapon.Projectile = LoadItemPrefab("Weapons/Laser");
                    weapon.Muzzle = LoadItemPrefab("Weapons/Muzzle");
                    break;
                case WeaponType.Shotgun:
                    weapon.Name = "SHOTGUN";
                    weapon.Ammo = Random.Range(100, 1000); // TODO Consider about ranges of weapons
                    weapon.Damage = Random.Range(0.5f, 15f); // TODO Consider about ranges of weapons
                    weapon.Dispersion = Random.Range(0, 15); // TODO Consider about ranges of weapons
                    weapon.Range = Random.Range(10, 125); // TODO Consider about ranges of weapons
                    weapon.TimeBetweenShoot = Random.Range(0.1f, 0.5f); // TODO Consider about ranges of weapons
                    weapon.Speed = 100;
                    weapon.Projectile = LoadItemPrefab("Weapons/Shotgun");
                    weapon.Muzzle = LoadItemPrefab("Weapons/Muzzle");
                    break;
                case WeaponType.RocketLuncher:
                    weapon.Name = "ROCKET LUNCHER";
                    weapon.Ammo = Random.Range(20, 250); // TODO Consider about ranges of weapons
                    weapon.Damage = Random.Range(20f, 40f); // TODO Consider about ranges of weapons
                    weapon.Dispersion = Random.Range(0, 0); // TODO Consider about ranges of weapons
                    weapon.Range = Random.Range(25, 125); // TODO Consider about ranges of weapons
                    weapon.TimeBetweenShoot = Random.Range(0.5f, 2f); // TODO Consider about ranges of weapons
                    weapon.Speed = 30;
                    weapon.Projectile = LoadItemPrefab("Weapons/Rocket");
                    weapon.Muzzle = LoadItemPrefab("Weapons/Muzzle");
                    break;
            }
           
            return weapon;  
        }
        public static WeaponData DefaultWeapon()
        {
            WeaponData weapon = new WeaponData();
            weapon.WeaponType = WeaponType.Rifle;

            weapon.Name = "RIFLE";
            weapon.Ammo = 750;
            weapon.Damage = 10;
            weapon.Dispersion = 5;
            weapon.Range = 25;
            weapon.TimeBetweenShoot = 0.15f;
            weapon.Speed = 75;
            weapon.Projectile = LoadItemPrefab("Weapons/Rifle");
            weapon.Muzzle = LoadItemPrefab("Weapons/Muzzle");
            return weapon;
        }
    }

    public class ItemData
    {
        public string Name;
    }
    public class WeaponData : ItemData
    {
        public WeaponType WeaponType;
        public float TimeBetweenShoot;
        public int Ammo;
        public float Damage;
        public float Range;
        public int Dispersion;
        public int Speed;
        public GameObject Projectile;
        public GameObject Muzzle;
        public bool equiped;
    }
    public class ShieldData : ItemData
    {
        public ShieldType ShieldType;
        public float Energy;
        public GameObject ShieldPrefab;
        public bool equiped;
    }
}
