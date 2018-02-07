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
        HealShield,
        BoostShield
    };

    public class ShieldItem : MonoBehaviour
    {
        [SerializeField] Button equipButton;
        [SerializeField] Text shieldText;

        public Items.ShieldData shieldItemData;

        public void SetItemInfo()
        {
            shieldText.text = shieldItemData.Name;
        }
        // Use this for initialization
        void Start()
        {
            equipButton.onClick.AddListener(EquipShield);
        }

        void EquipShield()
        {
            var selectedPlayer = InventoryMenu.Instance.CurrentPlayer;
            if (selectedPlayer == 1) PlayerDatabase.Instance.playerOneEquipedShield = shieldItemData;
            else if (selectedPlayer == 2) PlayerDatabase.Instance.playerTwoEquipedShield = shieldItemData;

            InventoryMenu.Instance.ChangeEquipedShieldText(shieldItemData);
        }
    }
}
