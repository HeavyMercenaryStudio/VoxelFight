using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Weapons;
using Shields;

public class PlayerItemData
{
    public WeaponData PlayerEquipedWeapon;
    public ShieldData PlayerEquipedShield;
    public List<WeaponData> PlayerWeapons = new List<WeaponData>();
    public List<ShieldData> PlayerShields = new List<ShieldData>();

    public void FirstLoad()
    {
        var weapon = ItemGenerator.DefaultWeapon();
        PlayerEquipedWeapon = weapon;
        PlayerWeapons.Add(weapon);

        var shield = ItemGenerator.DefaultShield();
        PlayerShields.Add(shield);
        PlayerEquipedShield = shield;
    }
}



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

    public PlayerItemData[] PlayersItemList;

    public delegate void CrystalValueChange(int crystals);
    public CrystalValueChange crystalValueChanged;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private int playersCrystals;
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
            if(crystalValueChanged != null)
                crystalValueChanged(value);
        }
    }

    public void Start()
    {
       if(crystalValueChanged != null)
            crystalValueChanged(playersCrystals);
    }
    public void AddWeapon(GameObject player, int index)
    {
        SelectWeapon(PlayersItemList[index].PlayerEquipedWeapon, player);
    }
    private void SelectWeapon(Items.WeaponData weaponData, GameObject player)
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

        weaponComponet.Damage = weaponData.Damage;
        weaponComponet.SecondsBetweenShoot = weaponData.TimeBetweenShoot;
        weaponComponet.MaxAmmo = weaponData.Ammo;
        weaponComponet.Range = weaponData.Range;
        weaponComponet.Dispersion = weaponData.Dispersion;
        weaponComponet.BulletSpeed = weaponData.Speed;
        weaponComponet.Bullet = weaponData.Projectile;
        weaponComponet.Muzzle = weaponData.Muzzle;
    }

    public void AddShield(GameObject player, int index)
    {
        SelectShield(PlayersItemList[index].PlayerEquipedShield, player);
    }
    private void SelectShield(Items.ShieldData shieldData, GameObject player)
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
            case ShieldType.BoostShield:
                player.AddComponent<BoostShield>();
                break;
            default:
                break;
        }
        var shieldComp = player.GetComponent<Shield>();

        shieldComp.MaxEnergy = shieldData.Energy;
        shieldComp.ShieldPrefab = shieldData.ShieldPrefab;
    }

    public void ResetData()
    {
       // playerOneShieldData.ResetData();
        //playerTwoShieldData.ResetData();
    }
}
