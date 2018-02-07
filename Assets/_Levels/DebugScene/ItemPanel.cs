using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour {

    [SerializeField] Transform weaponContex;
    [SerializeField] Transform shieldContex;
    [SerializeField] GameObject weaponContexItem;
    [SerializeField] GameObject shieldContexItem;

    void Start()
    {
        InventoryMenu.Instance.notifyPlayerChange += ChangePlayerItems;
        ChangePlayerItems(1);
    }

    private void ChangePlayerItems(int no)
    {
        var data = PlayerDatabase.Instance;
        if (no == 1) SetPanelItems(data.playerOneWeaponsItem, data.playerOneShieldItem);
        if (no == 2) SetPanelItems(data.playerTwoWeaponsItem, data.playerTwoShieldItem);
    }

    private void SetPanelItems(List<Items.WeaponData> weapons, List<Items.ShieldData> shields)
    {
        ClearContent();

        foreach (var item in weapons)
        {
            GameObject obj = Instantiate(weaponContexItem, weaponContex);
            var weapon = obj.GetComponent<Weapons.WeaponItem>();
            weapon.weaponItemData = item;
            weapon.SetItemInfo();
        }
        foreach (var item in shields)
        {
            GameObject obj = Instantiate(shieldContexItem, shieldContex);
            var shield = obj.GetComponent<Shields.ShieldItem>();
            shield.shieldItemData = item;
            shield.SetItemInfo();
        }
    }

    private void ClearContent()
    {
        foreach (Transform t in weaponContex){
            Destroy(t.gameObject);
        }
        foreach (Transform t in shieldContex){
            Destroy(t.gameObject);
        }

    }
}
