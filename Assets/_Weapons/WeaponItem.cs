using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

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
        [SerializeField] Button buyButton;
        [SerializeField] Text craftCostText;

        public WeaponType weaponType;
        WeaponData weaponData;

        // Use this for initialization
        void Start ()
        {
            equipButton.onClick.AddListener(EquipWeapon);
            buyButton.onClick.AddListener(BuyWeapon);

            InventoryMenu.Instance.notifyPlayerChange += SetVisibleButtons;
            SetVisibleButtons();

            SetPurchaseCost();
        }

        private void SetPurchaseCost()
        {
            var selectedWeapon = GetPlayerWeaponData();
            var thisWeapon = GetSelectedWeapon(selectedWeapon);

            craftCostText.text = thisWeapon.WeaponCost.ToString();
        }
        private void SetVisibleButtons()
        {
            var selectedWeapon = GetPlayerWeaponData();
            var thisWeapon = GetSelectedWeapon(selectedWeapon);

            if (thisWeapon.Available)
            {
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false);
            }
        }

        void EquipWeapon()
        {
            var selectedWeapon = GetPlayerWeaponData();
            selectedWeapon.CurrentPlayerWeapon = GetSelectedWeapon(selectedWeapon);

            SetVisibleButtons();

            InventoryMenu.Instance.ChangeEquipedWeaponText(weaponData);
        }

        private PlayerWeaponData GetSelectedPlayerWeapon(int player, PlayerDatabase playerdata)
        {
            switch (player)
            {
                case 0:
                    return playerdata.PlayerOneWeaponData;

                case 1:
                    return playerdata.PlayerTwoWeaponData;

                case 2:
                    return playerdata.PlayerThreeWeaponData;

                case 3:
                    return playerdata.PlayerFourWeaponData;
            }
            return null;
        }
        private WeaponData GetSelectedWeapon(PlayerWeaponData playerdata)
        {
            switch (weaponType)
            {
                case WeaponType.Rifle:
                    weaponData = playerdata.AvailableWeaponsP.rifleData;
                    break;
                case WeaponType.Laser:
                    weaponData = playerdata.AvailableWeaponsP.laserData;
                    break;
                case WeaponType.Shotgun:
                    weaponData = playerdata.AvailableWeaponsP.shotgunData;
                    break;
                case WeaponType.RocketLuncher:
                    weaponData = playerdata.AvailableWeaponsP.rocketData;
                    break;
            }

            return weaponData; 
        }

        void BuyWeapon()
        {
            //Check for enought points
            var selectedWeapon = GetPlayerWeaponData();
            var thisWeapon = GetSelectedWeapon(selectedWeapon);

            var colectedCrystals = PlayerDatabase.Instance.PlayersCrystals;
            var weaponCost = thisWeapon.WeaponCost;

            if(colectedCrystals >= weaponCost)
            {
                PlayerDatabase.Instance.PlayersCrystals -= weaponCost;
                thisWeapon.Available = true;
                InventoryMenu.Instance.SetPlayerCrystalsValueText();
            }

            SetVisibleButtons();
        }

        private PlayerWeaponData GetPlayerWeaponData()
        {
            int player = InventoryMenu.Instance.CurrentPlayer;
            var playerdata = PlayerDatabase.Instance;

            var selectedWeapon = GetSelectedPlayerWeapon(player, playerdata);
            return selectedWeapon;
        }

        private void OnDestroy()
        {
            InventoryMenu.Instance.notifyPlayerChange -= SetVisibleButtons;
        }
    }

}