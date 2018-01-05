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


    private static InventoryMenu instance = null;
    public static InventoryMenu Instance
    {
        get
        {
            return instance;
        }
    }

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
        UpdateInventoryGUI();
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

        UpdateInventoryGUI();
    }
    private void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer == WorldData.NumberOfPlayers)
            currentPlayer = 0;

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


