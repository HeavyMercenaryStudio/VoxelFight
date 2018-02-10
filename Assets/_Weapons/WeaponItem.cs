using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;
using System;
using Items;

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
        [SerializeField] Button sellButton;
        [SerializeField] Text weaponText;

        private Items.WeaponData weaponItemData;
        public WeaponData WeaponItemData
        {
            get
            {
                return weaponItemData;
            }

            set
            {
                weaponItemData = value;
            }
        }

        int weaponCost;

        public void SetItemInfo()
        {
            weaponText.text = WeaponItemData.Name;
        }

        // Use this for initialization
        void Start ()
        {
            equipButton.onClick.AddListener(EquipWeapon);
            sellButton.onClick.AddListener(SellWeapon);

            weaponCost = (int)((weaponItemData.Ammo + weaponItemData.Damage + weaponItemData.Dispersion + weaponItemData.Range +
                         weaponItemData.Speed + weaponItemData.TimeBetweenShoot)/6);

            var txt = sellButton.GetComponentInChildren<Text>();
            txt.text = "SELL \n" + weaponCost + "$";
        }

        void EquipWeapon()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            var data = PlayerDatabase.Instance;
            data.PlayersItemList[selectedPlayer - 1].PlayerEquipedWeapon = weaponItemData;

            InventoryMenu.Instance.ChangeEquipedWeaponText(WeaponItemData);

            SetItemHighlight();
        }

        private void SellWeapon()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            var data = PlayerDatabase.Instance;
            if (data.PlayersItemList[selectedPlayer - 1].PlayerEquipedWeapon == weaponItemData)
                return;

            data.PlayersItemList[selectedPlayer - 1].PlayerWeapons.Remove(weaponItemData);
            data.PlayersCrystals += weaponCost;
            InventoryMenu.Instance.SetPlayerCrystalsValueText();

            Destroy(transform.parent.gameObject);
        }

        public void SetItemHighlight()
        {
            var panel = FindObjectOfType<ItemPanel>();
            panel.ResetWeaponContent();
            var bg = equipButton.transform.parent.parent;
            bg.GetComponent<Image>().color = Color.green;
        }
    }

}