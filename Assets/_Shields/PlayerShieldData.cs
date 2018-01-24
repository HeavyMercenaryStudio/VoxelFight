using Shields;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerShieldData")]
public class PlayerShieldData : ScriptableObject
{
    [SerializeField] ShieldData currentPlayerShield;
    public ShieldData CurrentPlayerShield
    {
        get
        {
            return currentPlayerShield;
        }

        set
        {
            currentPlayerShield = value;
        }
    }
    [SerializeField] AvailableShields availableShields;

    [Serializable]
    public class AvailableShields
    {
        public ShieldData absorbData;
        public ShieldData reflectData;
        public ShieldData healingData;
    }
    public AvailableShields AvailableShieldsP
    {
        get
        {
            return availableShields;
        }
    }

    public void ResetData()
    {
        availableShields.absorbData.ResetData();
        availableShields.reflectData.ResetData();
        availableShields.healingData.ResetData();
    }
}
