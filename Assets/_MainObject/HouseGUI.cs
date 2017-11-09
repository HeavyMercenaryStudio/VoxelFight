using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseGUI : MonoBehaviour {

    [SerializeField] Image healthBar;

    public void UpdateHealthInfo(float healthAmount)
    {
        healthBar.fillAmount = healthAmount;
    }
}
