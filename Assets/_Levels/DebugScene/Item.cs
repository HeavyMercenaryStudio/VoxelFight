using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Characters;
using UnityEngine.UI;
using System;
using System.IO;

public class Item : MonoBehaviour {

    [SerializeField] Transform equipedWeaponLabels;
    [SerializeField] Transform dropedWeaponLabels;

    Canvas canvas;
    ItemData itemData;
    PlayerController connectedPlayer;

    // Use this for initialization
    void Start ()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);

        itemData = ItemGenerator.GenerateRandomItem();
    }

    private void SetItemsData(Collider other)   
    {
        ItemData eqipedItem = new ItemData();
        if (itemData is WeaponData)
        {
            var weapon = other.GetComponent<Weapons.Weapon>();
            WeaponData weaponData = new WeaponData();
            weaponData.Ammo = weapon.MaxAmmo;
            weaponData.Damage = weapon.Damage;
            weaponData.Range = weapon.Range;
            weaponData.TimeBetweenShoot = weapon.SecondsBetweenShoot;
            weaponData.Dispersion = weapon.Dispersion;
            weaponData.Name = weapon.GetType().ToString();
            eqipedItem = weaponData;
        }
        else if (itemData is ShieldData)
        {
            var shield = other.GetComponent<Shields.Shield>();
            ShieldData shieldData = new ShieldData();
            shieldData.Energy = shield.MaxEnergy;
            shieldData.Name = shield.GetType().ToString();
            eqipedItem = shieldData;
        }

        SetItemCanvas(itemData, eqipedItem, dropedWeaponLabels);
        SetItemCanvas(eqipedItem, itemData, equipedWeaponLabels);
    }
    private void SetItemCanvas(ItemData item1, ItemData item2, Transform parent)
    {
        if (item1 is WeaponData)
        {
            var firstItem = (WeaponData)item1;
            var secondItem = (WeaponData)item2;
            SeItemCanvasText(parent, 0, firstItem.Name, true, Color.white);

            var color = GetBiggerValueColor(firstItem.Damage, secondItem.Damage);
            SeItemCanvasText(parent, 1, "DAMAGE : " + firstItem.Damage.ToString("0.00"), true, color);

            color = GetBiggerValueColor(firstItem.Ammo, secondItem.Ammo);
            SeItemCanvasText(parent, 2, "AMMO : " + firstItem.Ammo.ToString("0.00"), true, color);

            color = GetSmallerValueColor(firstItem.Dispersion, secondItem.Dispersion);
            SeItemCanvasText(parent, 3, "DISP : " + firstItem.Dispersion.ToString("0.00"), true, color);

            color = GetBiggerValueColor(firstItem.Range, secondItem.Range);
            SeItemCanvasText(parent, 4, "RANGE : " + firstItem.Range.ToString("0.00"), true, color);

            color = GetSmallerValueColor(firstItem.TimeBetweenShoot, secondItem.TimeBetweenShoot);
            SeItemCanvasText(parent, 5, "SPEED : " + firstItem.TimeBetweenShoot.ToString("0.00"), true, color);
        }
        else if (item1 is ShieldData)
        {
            var droppedShield = (ShieldData)item1;
            var equipedShield = (ShieldData)item2;

            SeItemCanvasText(parent, 0, droppedShield.Name, true, Color.white);

            var color = GetBiggerValueColor(droppedShield.Energy, equipedShield.Energy);
            SeItemCanvasText(parent, 1, "ENERGY : " + droppedShield.Energy.ToString("0.00"), true, color);
            SeItemCanvasText(parent, 2, "", false, color);
            SeItemCanvasText(parent, 3, "", false, color);
            SeItemCanvasText(parent, 4, "", false, color);
            SeItemCanvasText(parent, 5, "", false, color);
        }
    }
    private Color GetBiggerValueColor(float value1, float value2)
    {
        if (value1 > value2)
            return Color.green;
        else if (value1 == value2)
            return Color.white;
        else
            return Color.red;
    }
    private Color GetSmallerValueColor(float value1, float value2)
    {
        if (value1 < value2)
            return Color.green;
        else if (value1 == value2)
            return Color.white;
        else
            return Color.red;
    }
    private void SeItemCanvasText(Transform parent, int i, string value, bool active, Color textColor)
    {
        var child = parent.GetChild(i);
        child.gameObject.SetActive(active);
        var text = child.GetComponent<Text>();
        text.text = value;
        textColor.a = 0.75f; // TODO NOT bright color
        text.color = textColor;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layers.PLAYER)
        {
            if (!connectedPlayer) return;

            var no = connectedPlayer.GetPlayerNumber();
            if (Input.GetButtonDown("Equip" + no))
            { 
                EquipItem(other);
                connectedPlayer.isPlayerDisabled = true;
            }
        }
    }

    private void EquipItem(Collider other)
    {
        if (connectedPlayer.isPlayerDisabled) return;

        var no = connectedPlayer.GetPlayerNumber();
        if (itemData is WeaponData)
        {
            PlayerDatabase.Instance.PlayersItemList[no - 1].PlayerEquipedWeapon.Ammo = 
                other.GetComponent<Weapons.Weapon>().GetCurrentAmmo();
            EquipWeapon();
        }
        else if (itemData is ShieldData)
        {
            EquipShield();
        }

        Destroy(this.gameObject, 0.2f);
    }

    void EquipShield()
    {
        Destroy(connectedPlayer.GetComponent<Shields.Shield>());

        StartCoroutine(AddShield());
    }
    IEnumerator AddShield()
    {
        yield return new WaitForSeconds(0.01f);

        var shield = (ShieldData)itemData;
        var data = PlayerDatabase.Instance;
        var no = connectedPlayer.GetPlayerNumber() - 1;

        data.PlayersItemList[no].PlayerShields.Add(shield);
        data.PlayersItemList[no].PlayerEquipedShield = shield;

        switch (shield.ShieldType)
        {
            case Shields.ShieldType.AbsorbShield:
                connectedPlayer.gameObject.AddComponent<Shields.AbsorbShield>();
                break;
            case Shields.ShieldType.ReflectShield:
                connectedPlayer.gameObject.AddComponent<Shields.ReflectShield>();
                break;
            case Shields.ShieldType.HealShield:
                connectedPlayer.gameObject.AddComponent<Shields.HealShield>();
                break;
            case Shields.ShieldType.BoostShield:
                connectedPlayer.gameObject.AddComponent<Shields.BoostShield>();
                break;

        }
        var shieldComponent = connectedPlayer.GetComponent<Shields.Shield>();

        shieldComponent.MaxEnergy = shield.Energy;
        shieldComponent.ShieldPrefab = shield.ShieldPrefab;

        connectedPlayer.Shield = shieldComponent;
        connectedPlayer.isPlayerDisabled = false;
    }
    void EquipWeapon()
    {
        Destroy(connectedPlayer.GetComponent<Weapons.Weapon>());

        StartCoroutine(AddWeapon());
    }
    IEnumerator AddWeapon()
    {
        yield return new WaitForSeconds(0.01f);

        var weapon = (WeaponData)itemData;
        var data = PlayerDatabase.Instance;
        var no = connectedPlayer.GetPlayerNumber() - 1;

        data.PlayersItemList[no].PlayerWeapons.Add(weapon);
        data.PlayersItemList[no].PlayerEquipedWeapon = weapon;


        switch (weapon.WeaponType)
        {
            case Weapons.WeaponType.Rifle:
                connectedPlayer.gameObject.AddComponent<Weapons.Riffle>();
                break;
            case Weapons.WeaponType.Laser:
                connectedPlayer.gameObject.AddComponent<Weapons.Laser>();
                break;
            case Weapons.WeaponType.Shotgun:
                connectedPlayer.gameObject.AddComponent<Weapons.Shotgun>();
                break;
            case Weapons.WeaponType.RocketLuncher:
                connectedPlayer.gameObject.AddComponent<Weapons.RocketLuncher>();
                break;
        }

        var weaponComponet = connectedPlayer.GetComponent<Weapons.Weapon>();

        weaponComponet.Damage = weapon.Damage;
        weaponComponet.SecondsBetweenShoot = weapon.TimeBetweenShoot;
        weaponComponet.MaxAmmo = weapon.Ammo;
        weaponComponet.Range = weapon.Range;
        weaponComponet.Dispersion = weapon.Dispersion;
        weaponComponet.BulletSpeed = weapon.Speed;
        weaponComponet.Bullet = weapon.Projectile;
        weaponComponet.Muzzle = weapon.Muzzle;
        weaponComponet.soundEnabled = true;
        connectedPlayer.Weapon = weaponComponet;

        connectedPlayer.isPlayerDisabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.PLAYER)
        {
            connectedPlayer = other.GetComponent<PlayerController>();
            if (!connectedPlayer) return;

            canvas.gameObject.SetActive(true);
            SetItemsData(other);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.PLAYER)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
