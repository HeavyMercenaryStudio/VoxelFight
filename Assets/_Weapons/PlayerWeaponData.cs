using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

[CreateAssetMenu(menuName = "PlayerWeaponData")]
public class PlayerWeaponData : ScriptableObject {

    [SerializeField] WeaponData currentPlayerWeapon;
    public WeaponData CurrentPlayerWeapon
    {
        get
        {
            return currentPlayerWeapon;
        }

        set
        {
            currentPlayerWeapon = value;
        }
    }
    [SerializeField] AvailableWeapons availableWeapons;

    [Serializable]
    public class AvailableWeapons
    {
        public WeaponData laserData;
        public WeaponData rifleData;
        public WeaponData rocketData;
        public WeaponData shotgunData;
    }
    public AvailableWeapons AvailableWeaponsP
    {
        get
        {
            return availableWeapons;
        }
    }
}
