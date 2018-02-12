using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Shields
{

    public enum ShieldType
    {
        AbsorbShield,
        ReflectShield,
        HealShield,
        BoostShield
    };

    public class ShieldItem : MonoBehaviour
    {
        [SerializeField] Button equipButton;
        [SerializeField] Button sellButton;
        [SerializeField] Button fillButton;
        [SerializeField] Text shieldText;

        private Items.ShieldData shieldItemData;
        public ShieldData ShieldItemData
        {
            get
            {
                return shieldItemData;
            }

            set
            {
                shieldItemData = value;
            }
        }

        int shieldCost;

        public void SetItemInfo()
        {
            shieldText.text = ShieldItemData.Name;
        }
        // Use this for initialization
        void Start()
        {
            equipButton.onClick.AddListener(EquipShield);
            sellButton.onClick.AddListener(SellShield);
            fillButton.onClick.AddListener(FillWeapon);


            shieldCost = (int)shieldItemData.Energy;
            var txt = sellButton.GetComponentInChildren<Text>();
            txt.text = "SELL \n" + shieldCost + "$";
        }
        private void FillWeapon()
        {
            var data = PlayerDatabase.Instance;
            if (data.PlayersCrystals <= 0) return;

            data.PlayersCrystals -= 100;
            InventoryMenu.Instance.UpdateInventoryGUI();
            InventoryMenu.Instance.SetPlayerCrystalsValueText();

            var amount = 0.05f * ShieldItemData.Energy;
            amount = Mathf.Clamp(amount, 20, 100);
            ShieldItemData.Energy += (int)amount;
        }
        private void SellShield()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            var data = PlayerDatabase.Instance;
            if (data.PlayersItemList[selectedPlayer - 1].PlayerEquipedShield == shieldItemData)
                return;

            data.PlayersItemList[selectedPlayer - 1].PlayerShields.Remove(shieldItemData);
            data.PlayersCrystals += shieldCost;
            InventoryMenu.Instance.SetPlayerCrystalsValueText();

            Destroy(transform.parent.gameObject);
        }

        void EquipShield()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            var data = PlayerDatabase.Instance;
            data.PlayersItemList[selectedPlayer - 1].PlayerEquipedShield = shieldItemData;

            InventoryMenu.Instance.ChangeEquipedShieldText(ShieldItemData);

            SetItemHighlight();
        }

        public void SetItemHighlight()
        {
            var panel = FindObjectOfType<ItemPanel>();
            panel.ResetShieldContent();
            var bg = equipButton.transform.parent.parent;
            bg.GetComponent<Image>().color = Color.green;
        }
    }
}
