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

        public WeaponType weaponType;
        WeaponData weaponData;

        // Use this for initialization
        void Start () {
            equipButton.onClick.AddListener(EquipWeapon);
            buyButton.onClick.AddListener(BuyWeapon);

        }

        void EquipWeapon()
        {
            int player = InventoryMenu.Instance.CurrentPlayer;
            var playerdata = PlayerDatabase.Instance;

            switch (player)
            {
                case 0:
                    SelectWeapon(playerdata.PlayerOneWeaponData);
                    break;

                case 1:
                    SelectWeapon(playerdata.PlayerTwoWeaponData);
                    break;

                case 2:
                    SelectWeapon(playerdata.PlayerThreeWeaponData);
                    break;

                case 3:
                    SelectWeapon(playerdata.PlayerFourWeaponData);
                    break;
            }

            InventoryMenu.Instance.ChangeEquipedWeaponText(weaponData);
        }

        private void SelectWeapon(PlayerWeaponData playerdata)
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

            playerdata.CurrentPlayerWeapon = weaponData;
        }

        void BuyWeapon()
        {

        }

    }

}