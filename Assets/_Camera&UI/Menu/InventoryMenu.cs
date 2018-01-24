using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEngine.UI;
using System;
using Data;
using Shields;

public class InventoryMenu : MonoBehaviour {

    int currentPlayer = 0;
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

    public delegate void OnPlayerChange();
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
        updateWeaponButton.onClick.AddListener(UpdateWeapon);
        updateShieldButton.onClick.AddListener(UpdateShield);
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

    private void UpdateWeapon()
    {
        int updateCost = 1000;
        int updateScale = 10;
        var data = PlayerDatabase.Instance;

        if (data.PlayersCrystals > updateCost)
        {
            data.PlayersCrystals -= updateCost;

            var pd = PlayerDatabase.Instance.GetPlayerWeaponData(currentPlayer);

            var updatedRange = pd.Range + 5;
            var updatedDamage = pd.DamagePerBullet + 1;
            var updatedAmmo = pd.MaxAmmo + (int)(0.1f * pd.MaxAmmo);
            //var updatedSpeed = Math.Round(pd.SecondsBetweenShoot - 0.05f, 3);

            pd.DamagePerBullet = Mathf.Clamp(updatedDamage, 0, pd.DefaultDamagePerBullet * updateScale);
            pd.Range = Mathf.Clamp(updatedRange, 0, pd.DefaultRange * updateScale);
            pd.MaxAmmo = Mathf.Clamp(updatedAmmo, 0, pd.DefaultMaxAmmo * updateScale);
            //pd.SecondsBetweenShoot = Mathf.Clamp((float)updatedSpeed, 0.01f, 999);

            ChangeEquipedWeaponText(pd);

            SetPlayerCrystalsValueText();
        }
    }
    private void UpdateShield()
    {
        int updateCost = 1000;
        int updateScale = 10;
        var data = PlayerDatabase.Instance;

        if (data.PlayersCrystals > updateCost)
        {
            data.PlayersCrystals -= updateCost;

            var pd = PlayerDatabase.Instance.GetPlayerShieldData(currentPlayer);

            var updatedEnergy = pd.MaxEnergy + 10;
         
            pd.MaxEnergy = Mathf.Clamp(updatedEnergy, 0, pd.DefaultEnergy * updateScale);

            ChangeEquipedShieldText(pd);
            SetPlayerCrystalsValueText();
        }
    }

    public void SetPlayerCrystalsValueText()
    {
        playersCrystalsValueText.text = PlayerDatabase.Instance.PlayersCrystals.ToString();
    }

    public void UpdateInventoryGUI()
    {
        ChangePlayerNameText();
        var pd = PlayerDatabase.Instance.GetPlayerWeaponData(currentPlayer);
        ChangeEquipedWeaponText(pd);
        var ps = PlayerDatabase.Instance.GetPlayerShieldData(currentPlayer);
        ChangeEquipedShieldText(ps);
    }
    private void PrevPlayer()
    {
        currentPlayer--;
        if (currentPlayer == -1)
            currentPlayer = WorldData.NumberOfPlayers - 1;

        notifyPlayerChange();
        UpdateInventoryGUI();
    }
    private void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer == WorldData.NumberOfPlayers)
            currentPlayer = 0;

        notifyPlayerChange();
        UpdateInventoryGUI();
    }

    private void ChangePlayerNameText()
    {
        playerNameText.text = "PLAYER " + (currentPlayer+1).ToString();
    }
    public void ChangeEquipedWeaponText(WeaponData weaponData)
    {
        if (weaponData == null) return;

        ChangeName(weaponData);

        damageValueText.text = weaponData.DamagePerBullet.ToString();
        rangeValueText.text = weaponData.Range.ToString();
        fireSpeedValueText.text = weaponData.SecondsBetweenShoot.ToString();
        ammoValueText.text = weaponData.MaxAmmo.ToString();
    }
    private void ChangeName(WeaponData weaponData)
    {
        if (weaponData.name.Contains("Rifle"))
            weaponNameText.text = "R I F L E";
        else if (weaponData.name.Contains("Shotgun"))
            weaponNameText.text = "S H O T G U N";
        else if (weaponData.name.Contains("Rocket"))
            weaponNameText.text = "R O C K E T  L U N C H E R";
        else if (weaponData.name.Contains("Laser"))
            weaponNameText.text = "L A S E R";
    }

    public void ChangeEquipedShieldText(ShieldData shieldData)
    {
        if (shieldData == null) return;

        ChangeName(shieldData);

        maxEneryText.text = shieldData.MaxEnergy.ToString();
    }
    private void ChangeName(ShieldData shieldData)
    {
        if (shieldData.name.Contains("Absorb"))
            shieldNameText.text = "A B S O R B  S H I E L D";
        else if (shieldData.name.Contains("Reflect"))
            shieldNameText.text = "R E F L E C T  S H I E L D";
        else if (shieldData.name.Contains("Heal"))
            shieldNameText.text = "H E A L I N G  S H I E L D";
    }
}


