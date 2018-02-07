using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;
using System;

namespace Weapons {

    public enum WeaponType
    {
        Rifle,
        Laser,
        Shotgun,
        RocketLuncher
    };
    

    public class WeaponItem : MonoBehaviour {

        [SerializeField] Button equipButton;
        [SerializeField] Text weaponText;

        public Items.WeaponData weaponItemData;

        public void SetItemInfo()
        {
            weaponText.text = weaponItemData.Name;
        }

        // Use this for initialization
        void Start ()
        {
            equipButton.onClick.AddListener(EquipWeapon);
        }

        void EquipWeapon()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            if (selectedPlayer == 1) PlayerDatabase.Instance.playerOneEquipedWeapon = weaponItemData;
            else if (selectedPlayer == 2) PlayerDatabase.Instance.playerTwoEquipedWeapon = weaponItemData;

            InventoryMenu.Instance.ChangeEquipedWeaponText(weaponItemData);
        }

        private void OnDestroy()
        {
        //    InventoryMenu.Instance.notifyPlayerChange -= SetVisibleButtons;
        }
    }

}