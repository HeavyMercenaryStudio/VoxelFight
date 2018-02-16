using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

    [SerializeField] Transform weaponContent;
    [SerializeField] Transform shieldContent;
    [SerializeField] GameObject weaponContexItem;
    [SerializeField] GameObject shieldContexItem;

    int selectedPlayerNo;
    void Start()
    {
        InventoryMenu.Instance.notifyPlayerChange += ChangePlayerItems;
        ChangePlayerItems(1);
    }

    private void ChangePlayerItems(int no)
    {
        selectedPlayerNo = no;
        var data = PlayerDatabase.Instance.PlayersItemList[no - 1];
        SetPanelItems(data.PlayerWeapons, data.PlayerShields);
    }
    private void SetPanelItems(List<Items.WeaponData> weapons, List<Items.ShieldData> shields)
    {
        ClearContent();

        foreach (var item in weapons)
        {
            GameObject obj = Instantiate(weaponContexItem, weaponContent);
            var weapon = obj.GetComponentInChildren<Weapons.WeaponItem>();
            weapon.WeaponItemData = item;
            CheckSelectedWeapon(weapon);
            weapon.SetItemInfo();
        }
        foreach (var item in shields)
        {
            GameObject obj = Instantiate(shieldContexItem, shieldContent);
            var shield = obj.GetComponentInChildren<Shields.ShieldItem>();
            shield.ShieldItemData = item;
            CheckSelectedShield(shield);
            shield.SetItemInfo();
        }
    }

    private void CheckSelectedWeapon(Weapons.WeaponItem weapon)
    {
        var data = PlayerDatabase.Instance.PlayersItemList[selectedPlayerNo - 1];
        if (weapon.WeaponItemData == data.PlayerEquipedWeapon)
        {
            ResetWeaponContent();
            weapon.SetItemHighlight();
        }
    }

    public void ResetWeaponContent()
    {
        foreach (Transform t in weaponContent){
            t.GetComponent<Image>().color = Color.grey;
        }
    }
    public void ResetShieldContent()
    {
        foreach (Transform t in shieldContent) {
            t.GetComponent<Image>().color = Color.grey;
        }
    }

    private void CheckSelectedShield(Shields.ShieldItem shield)
    {
        var data = PlayerDatabase.Instance.PlayersItemList[selectedPlayerNo - 1];
        if (shield.ShieldItemData == data.PlayerEquipedShield) {
            shield.SetItemHighlight();
        }
    }

    private void ClearContent()
    {
        foreach (Transform t in weaponContent){
            Destroy(t.gameObject);
        }
        foreach (Transform t in shieldContent){
            Destroy(t.gameObject);
        }

    }
}
