using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shields { 
    [CreateAssetMenu(menuName = "ShieldData")]
    public class ShieldData : ScriptableObject
    {
        [SerializeField] float defaultEnergy;

        [SerializeField] ShieldType shieldType;
        [SerializeField] float maxEnergy;
        [SerializeField] GameObject shieldPrefab;
        [SerializeField] bool available;
        [SerializeField] int shieldCost;

        public float DefaultEnergy
        {
            get
            {
                return defaultEnergy;
            }

            set
            {
                defaultEnergy = value;
            }
        }
        public float MaxEnergy
        {
            get
            {
                return maxEnergy;
            }

            set
            {
                maxEnergy = value;
            }
        }
        public GameObject ShieldPrefab
        {
            get
            {
                return shieldPrefab;
            }

            set
            {
                shieldPrefab = value;
            }
        }
        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                available = value;
            }
        }
        public int ShieldCost
        {
            get
            {
                return shieldCost;
            }

            set
            {
                shieldCost = value;
            }
        }
        public ShieldType ShieldType
        {
            get
            {
                return shieldType;
            }

            set
            {
                shieldType = value;
            }
        }
    }
}
