using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shields
{

    public enum ShieldType
    {
        AbsorbShield,
        ReflectShield,
        HealShield
    };

    public class ShieldItem : MonoBehaviour
    {
        [SerializeField] Button equipButton;
        [SerializeField] Button buyButton;
        [SerializeField] Text craftCostText;

        public ShieldType shieldType;
        ShieldData shieldData;

        // Use this for initialization
        void Start()
        {
            equipButton.onClick.AddListener(EquipShield);
            buyButton.onClick.AddListener(BuyShield);

            InventoryMenu.Instance.notifyPlayerChange += SetVisibleButtons;
            SetVisibleButtons();

            SetPurchaseCost();
        }

        private void SetPurchaseCost()
        {
            var selectedShield = GetPlayeShieldData();
            var thisShield = GetSelectedShield(selectedShield);

            craftCostText.text = thisShield.ShieldCost.ToString();
        }
        private void SetVisibleButtons()
        {
            var selectedShield = GetPlayeShieldData();
            var thisShield = GetSelectedShield(selectedShield);

            if (thisShield.Available)
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

        void EquipShield()
        {
            var selectedShield = GetPlayeShieldData();
            selectedShield.CurrentPlayerShield = GetSelectedShield(selectedShield);

            SetVisibleButtons();

            InventoryMenu.Instance.ChangeEquipedShieldText(shieldData);
        }

        private PlayerShieldData GetSelectedPlayerShield(int player, PlayerDatabase playerdata)
        {
            switch (player)
            {
                case 0:
                    return playerdata.PlayerOneShieldData;

                case 1:
                    return playerdata.PlayerTwoShieldData;

                case 2:
                    return playerdata.PlayerThreeShieldData;

                case 3:
                    return playerdata.PlayerFourShieldData;
            }
            return null;
        }
        private ShieldData GetSelectedShield(PlayerShieldData playerdata)
        {
            switch (shieldType)
            {
                case ShieldType.AbsorbShield:
                    shieldData = playerdata.AvailableShieldsP.absorbData;
                    break;
                case ShieldType.HealShield:
                    shieldData = playerdata.AvailableShieldsP.healingData;
                    break;
                case ShieldType.ReflectShield:
                    shieldData = playerdata.AvailableShieldsP.reflectData;
                    break;
            }

            return shieldData;
        }

        void BuyShield()
        {
            //Check for enought points
            var selectedShield = GetPlayeShieldData();
            var thisShield = GetSelectedShield(selectedShield);

            var colectedCrystals = PlayerDatabase.Instance.PlayersCrystals;
            var shieldCost = thisShield.ShieldCost;

            if (colectedCrystals >= shieldCost)
            {
                PlayerDatabase.Instance.PlayersCrystals -= shieldCost;
                thisShield.Available = true;
                InventoryMenu.Instance.SetPlayerCrystalsValueText();
            }

            SetVisibleButtons();
        }

        private PlayerShieldData GetPlayeShieldData()
        {
            int player = InventoryMenu.Instance.CurrentPlayer;
            var playerdata = PlayerDatabase.Instance;

            var selectedShield = GetSelectedPlayerShield(player, playerdata);
            return selectedShield;
        }

        private void OnDestroy()
        {
            InventoryMenu.Instance.notifyPlayerChange -= SetVisibleButtons;
        }
    }
}
