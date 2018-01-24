using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Shields;

public class PlayerDatabase : MonoBehaviour
{
    private static PlayerDatabase instance;
    public static PlayerDatabase Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    [Header("Weapon Data")]
    [SerializeField] PlayerWeaponData playerOneWeaponData;
    [SerializeField] PlayerWeaponData playerTwoWeaponData;
    [SerializeField] PlayerWeaponData playerThreeWeaponData;
    [SerializeField] PlayerWeaponData playerFourWeaponData;

    [Header("Shield Data")]
    [SerializeField] PlayerShieldData playerOneShieldData;
    [SerializeField] PlayerShieldData playerTwoShieldData;
    [SerializeField] PlayerShieldData playerThreeShieldData;
    [SerializeField] PlayerShieldData playerFourShieldData;

    private int playersCrystals;

    public PlayerWeaponData PlayerOneWeaponData
    {
        get
        {
            return playerOneWeaponData;
        }

        set
        {
            playerOneWeaponData = value;
        }
    }
    public PlayerWeaponData PlayerTwoWeaponData
    {
        get
        {
            return playerTwoWeaponData;
        }

        set
        {
            playerTwoWeaponData = value;
        }
    }
    public PlayerWeaponData PlayerThreeWeaponData
    {
        get
        {
            return playerThreeWeaponData;
        }

        set
        {
            playerThreeWeaponData = value;
        }
    }
    public PlayerWeaponData PlayerFourWeaponData
    {
        get
        {
            return playerFourWeaponData;
        }

        set
        {
            playerFourWeaponData = value;
        }
    }
    public int PlayersCrystals
    {
        get
        {
            playersCrystals = PlayerPrefs.GetInt("PlayerCrystals");
            return playersCrystals;
        }
        set
        {
            playersCrystals = value;
            PlayerPrefs.SetInt("PlayerCrystals", playersCrystals);
        }
    }

    public PlayerShieldData PlayerOneShieldData
    {
        get
        {
            return playerOneShieldData;
        }

        set
        {
            playerOneShieldData = value;
        }
    }
    public PlayerShieldData PlayerTwoShieldData
    {
        get
        {
            return playerTwoShieldData;
        }

        set
        {
            playerTwoShieldData = value;
        }
    }
    public PlayerShieldData PlayerThreeShieldData
    {
        get
        {
            return playerThreeShieldData;
        }

        set
        {
            playerThreeShieldData = value;
        }
    }
    public PlayerShieldData PlayerFourShieldData
    {
        get
        {
            return playerFourShieldData;
        }

        set
        {
            playerFourShieldData = value;
        }
    }

    public WeaponData GetPlayerWeaponData(int index)
    {
        switch (index)
        {
            case 0:
                return playerOneWeaponData.CurrentPlayerWeapon;
            case 1:
                return playerTwoWeaponData.CurrentPlayerWeapon;
            case 2:
                return playerThreeWeaponData.CurrentPlayerWeapon;
            case 3:
                return playerFourWeaponData.CurrentPlayerWeapon;
        }

        return null;
    }
    public ShieldData GetPlayerShieldData(int index)
    {
        switch (index)
        {
            case 0:
                return playerOneShieldData.CurrentPlayerShield;
            case 1:
                return playerTwoShieldData.CurrentPlayerShield;
            case 2:
                return playerThreeShieldData.CurrentPlayerShield;
            case 3:
                return playerFourShieldData.CurrentPlayerShield;
        }

        return null;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    public void AddWeapon(GameObject player, int index)
    {
        switch (index)
        {
            case 0:
                SelectWeapon(playerOneWeaponData.CurrentPlayerWeapon, player);
                break;

            case 1:
                SelectWeapon(playerTwoWeaponData.CurrentPlayerWeapon, player);
                break;

            case 2:
                SelectWeapon(playerThreeWeaponData.CurrentPlayerWeapon, player);
                break;

            case 3:
                SelectWeapon(playerFourWeaponData.CurrentPlayerWeapon, player);
                break;
        }
    }
    private void SelectWeapon(WeaponData weaponData, GameObject player)
    {
        switch (weaponData.WeaponType)
        {
            case WeaponType.Rifle:
                player.AddComponent<Riffle>();
                break;
            case WeaponType.Laser:
                player.AddComponent<Laser>();
                break;
            case WeaponType.Shotgun:
                player.AddComponent<Shotgun>();
                break;
            case WeaponType.RocketLuncher:
                player.AddComponent<RocketLuncher>();
                break;
        }

        var weaponComponet = player.GetComponent<Weapon>();

        weaponComponet.Damage = weaponData.DamagePerBullet;
        weaponComponet.SecondsBetweenShoot = weaponData.SecondsBetweenShoot;
        weaponComponet.MaxAmmo = weaponData.MaxAmmo;
        weaponComponet.Range = weaponData.Range;
        weaponComponet.Dispersion = weaponData.Dispersion;
        weaponComponet.BulletSpeed = weaponData.ProjectileSpeed;
        weaponComponet.Bullet = weaponData.Projectile;
        weaponComponet.Muzzle = weaponData.Muzzle;
    }

    public void AddShield(GameObject player, int index)
    {
        switch (index)
        {
            case 0:
                SelectShield(PlayerOneShieldData.CurrentPlayerShield, player);
                break;

            case 1:
                SelectShield(PlayerTwoShieldData.CurrentPlayerShield, player);
                break;

            case 2:
                SelectShield(PlayerThreeShieldData.CurrentPlayerShield, player);
                break;

            case 3:
                SelectShield(PlayerFourShieldData.CurrentPlayerShield, player);
                break;
        }
    }
    private void SelectShield(ShieldData shieldData, GameObject player)
    {
        switch (shieldData.ShieldType)
        {
            case ShieldType.AbsorbShield:
                player.AddComponent<AbsorbShield>();
                break;
            case ShieldType.ReflectShield:
                player.AddComponent<ReflectShield>();
                break;
            case ShieldType.HealShield:
                player.AddComponent<HealShield>();
                break;
            default:
                break;
        }
        var shieldComp = player.GetComponent<Shield>();

        shieldComp.MaxEnergy = shieldData.MaxEnergy;
        shieldComp.ShieldPrefab = shieldData.ShieldPrefab;
    }

    public void ResetData()
    {
        playerOneWeaponData.ResetData();
        playerTwoWeaponData.ResetData();
        playerOneShieldData.ResetData();
        playerTwoShieldData.ResetData();
    }
}
