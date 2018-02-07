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

    public Items.WeaponData playerOneEquipedWeapon;
    public Items.ShieldData playerOneEquipedShield;
    public Items.WeaponData playerTwoEquipedWeapon;
    public Items.ShieldData playerTwoEquipedShield;

    public List<Items.WeaponData> playerOneWeaponsItem = new List<Items.WeaponData>();
    public List<Items.ShieldData> playerOneShieldItem = new List<Items.ShieldData>();
    public List<Items.WeaponData> playerTwoWeaponsItem = new List<Items.WeaponData>();
    public List<Items.ShieldData> playerTwoShieldItem = new List<Items.ShieldData>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

       
        

        if (playerOneWeaponsItem.Count == 0)
        {
            playerOneWeaponsItem.Add(Items.ItemGenerator.DefaultWeapon());
            playerOneEquipedWeapon = playerOneWeaponsItem[0];
        }
        if (playerOneShieldItem.Count == 0)
        {
            playerOneShieldItem.Add(Items.ItemGenerator.DefaultShield());
            playerOneEquipedShield = playerOneShieldItem[0];
        }
        if (playerTwoWeaponsItem.Count == 0)
        {
            playerTwoWeaponsItem.Add(Items.ItemGenerator.DefaultWeapon());
            playerTwoEquipedWeapon = playerTwoWeaponsItem[0];
        }
        if (playerTwoShieldItem.Count == 0)
        {
            playerTwoShieldItem.Add(Items.ItemGenerator.DefaultShield());
            playerTwoEquipedShield = playerTwoShieldItem[0];
        }


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
        }
    }

    public void AddWeapon(GameObject player, int index)
    {
        switch (index)
        {
            case 0:
                SelectWeapon(playerOneEquipedWeapon, player);
                break;

            case 1:
                SelectWeapon(playerTwoEquipedWeapon, player);
                break;
        }
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
        switch (index)
        {
            case 0:
                SelectShield(playerOneEquipedShield, player);
                break;

            case 1:
                SelectShield(playerTwoEquipedShield, player);
                break;
        }
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
