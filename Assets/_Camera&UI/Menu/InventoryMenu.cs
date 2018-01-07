using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using UnityEngine.UI;
using System;
using Data;

public class InventoryMenu : MonoBehaviour {

    int currentPlayer = 0;
    public int CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
    }

    [SerializeField] Text weaponNameText;
    [SerializeField] Text damageValueText;
    [SerializeField] Text rangeValueText;
    [SerializeField] Text fireSpeedValueText;
    [SerializeField] Text ammoValueText;

    [SerializeField] Button nextPlayerButton;
    [SerializeField] Button prevPlayerButton;
    [SerializeField] Text playerNameText;
    [SerializeField] Text playersCrystalsValueText;

    [SerializeField] Button updateWeaponButton;

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
        UpdateInventoryGUI();

        SetPlayerCrystalsValueText();
    }

    private void UpdateWeapon()
    {
        int updateCost = 1000;
        int updateScale = 10;
        var crystals = PlayerDatabase.Instance.PlayersCrystals;

        if (crystals > updateCost)
        {
            crystals -= updateCost;

            var pd = PlayerDatabase.Instance.GetPlayerWeaponData(currentPlayer);

            var updatedRange = pd.Range + 5;
            var updatedDamage = pd.DamagePerBullet + 1;
            var updatedAmmo = pd.MaxAmmo + (int)(0.1f * pd.MaxAmmo);
            var updatedSpeed = Math.Round(pd.SecondsBetweenShoot - 0.05f, 3);

            pd.DamagePerBullet = Mathf.Clamp(updatedDamage, 0, pd.DefaultDamagePerBullet * updateScale);
            pd.Range = Mathf.Clamp(updatedRange, 0, pd.DefaultRange * updateScale);
            pd.MaxAmmo = Mathf.Clamp(updatedAmmo, 0, pd.DefaultMaxAmmo * updateScale);
            pd.SecondsBetweenShoot = Mathf.Clamp((float)updatedSpeed, 0.01f, 999);

            ChangeEquipedWeaponText(pd);
        }

       
    }

    public void SetPlayerCrystalsValueText()
    {
        playersCrystalsValueText.text = PlayerDatabase.Instance.PlayersCrystals.ToString();
    }

    private void UpdateInventoryGUI()
    {
        ChangePlayerNameText();
        var pd = PlayerDatabase.Instance.GetPlayerWeaponData(currentPlayer);
        ChangeEquipedWeaponText(pd);
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
}


