using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEngine.UI;
using System;
using Data;
using Shields;

public class InventoryMenu : MonoBehaviour {

    int currentPlayer = 1;
    public int CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
    }

    [Header("Weapon UI")]
    [SerializeField] GameObject weaponPanel;
    [SerializeField] GameObject weaponDetailPanel;
    [SerializeField] Text weaponNameText;
    [SerializeField] Text damageValueText;
    [SerializeField] Text rangeValueText;
    [SerializeField] Text fireSpeedValueText;
    [SerializeField] Text ammoValueText;
    [SerializeField] Button updateWeaponButton;

    [Header("Shield UI")]
    [SerializeField] GameObject shieldPanel;
    [SerializeField] GameObject shieldDetailPanel;
    [SerializeField] Text shieldNameText;
    [SerializeField] Text maxEneryText;
    [SerializeField] Button updateShieldButton;
   

    [Header("Change Player UI")]
    [SerializeField] Button nextPlayerButton;
    [SerializeField] Button prevPlayerButton;
    [SerializeField] Text playerNameText;


    [Header("Change Items UI")]
    [SerializeField] Button nextItemButton;
    [SerializeField] Button prevItemButton;


    [SerializeField] Text playersCrystalsValueText;

    private static InventoryMenu instance = null;
    public static InventoryMenu Instance
    {
        get
        {
            return instance;
        }
    }

    public delegate void OnPlayerChange(int no);
    public OnPlayerChange notifyPlayerChange;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    public void Start()
    {
        nextPlayerButton.onClick.AddListener(NextPlayer);
        prevPlayerButton.onClick.AddListener(PrevPlayer);
       // updateWeaponButton.onClick.AddListener(UpdateWeapon);
      //  updateShieldButton.onClick.AddListener(UpdateShield);
        nextItemButton.onClick.AddListener(NextItem);
        prevItemButton.onClick.AddListener(PrevItem);
        UpdateInventoryGUI();

        SetPlayerCrystalsValueText();
    }

    private void NextItem()
    {
        weaponPanel.SetActive(false);
        weaponDetailPanel.SetActive(false);
        shieldPanel.SetActive(true);
        shieldDetailPanel.SetActive(true);
    }
    private void PrevItem()
    {
        weaponPanel.SetActive(true);
        weaponDetailPanel.SetActive(true);
        shieldPanel.SetActive(false);
        shieldDetailPanel.SetActive(false);

    }

    //private void UpdateWeapon()
    //{
    //    int updateCost = 1000;
    //    int updateScale = 10;
    //    var data = PlayerDatabase.Instance;

    //    if (data.PlayersCrystals > updateCost)
    //    {
    //        data.PlayersCrystals -= updateCost;

    //        var pd = PlayerDatabase.Instance.GetPlayerWeaponData(currentPlayer);

    //        var updatedRange = pd.Range + 5;
    //        var updatedDamage = pd.DamagePerBullet + 1;
    //        var updatedAmmo = pd.MaxAmmo + (int)(0.1f * pd.MaxAmmo);
    //        //var updatedSpeed = Math.Round(pd.SecondsBetweenShoot - 0.05f, 3);

    //        pd.DamagePerBullet = Mathf.Clamp(updatedDamage, 0, pd.DefaultDamagePerBullet * updateScale);
    //        pd.Range = Mathf.Clamp(updatedRange, 0, pd.DefaultRange * updateScale);
    //        pd.MaxAmmo = Mathf.Clamp(updatedAmmo, 0, pd.DefaultMaxAmmo * updateScale);
    //        //pd.SecondsBetweenShoot = Mathf.Clamp((float)updatedSpeed, 0.01f, 999);

    //        ChangeEquipedWeaponText(pd);

    //        SetPlayerCrystalsValueText();
    //    }
    //}
    //private void UpdateShield()
    //{
    //    int updateCost = 1000;
    //    int updateScale = 10;
    //    var data = PlayerDatabase.Instance;

    //    if (data.PlayersCrystals > updateCost)
    //    {
    //        data.PlayersCrystals -= updateCost;

    //        var pd = PlayerDatabase.Instance.GetPlayerShieldData(currentPlayer);

    //        var updatedEnergy = pd.MaxEnergy + 10;
         
    //        pd.MaxEnergy = Mathf.Clamp(updatedEnergy, 0, pd.DefaultEnergy * updateScale);

    //       // ChangeEquipedShieldText(pd);
    //        SetPlayerCrystalsValueText();
    //    }
    //}

    public void SetPlayerCrystalsValueText()
    {
        playersCrystalsValueText.text = PlayerDatabase.Instance.PlayersCrystals.ToString();
    }

    public void UpdateInventoryGUI()
    {
        ChangePlayerNameText();

        var data = PlayerDatabase.Instance.PlayersItemList[currentPlayer-1];
        ChangeEquipedWeaponText(data.PlayerEquipedWeapon);
        ChangeEquipedShieldText(data.PlayerEquipedShield);
    }
    private void PrevPlayer()
    {
        currentPlayer--;
        if (currentPlayer == 0)
            currentPlayer = WorldData.NumberOfPlayers;

        notifyPlayerChange(currentPlayer);
        UpdateInventoryGUI();
    }
    private void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer == WorldData.NumberOfPlayers+1)
            currentPlayer = 1;

        notifyPlayerChange(currentPlayer);
        UpdateInventoryGUI();
    }

    private void ChangePlayerNameText()
    {
        playerNameText.text = "PLAYER " + (currentPlayer).ToString();
    }
    public void ChangeEquipedWeaponText(Items.WeaponData weaponData)
    {
        if (weaponData == null) return;

        damageValueText.text = weaponData.Damage.ToString();
        rangeValueText.text = weaponData.Range.ToString();
        fireSpeedValueText.text = weaponData.TimeBetweenShoot.ToString();
        ammoValueText.text = weaponData.Ammo.ToString();
        weaponNameText.text = weaponData.Name;
    }
    public void ChangeEquipedShieldText(Items.ShieldData shieldData)
    {
        if (shieldData == null) return;

        maxEneryText.text = shieldData.Energy.ToString();
        shieldNameText.text = shieldData.Name;
    }
}


