using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour {


    [SerializeField] Button playerOneMaxHealthUp;
    [SerializeField] Button playerOneHealSelf;
    [SerializeField] Button playerOneFillAmmo;
    [SerializeField] Button playerOneMaxAmmoUp;
    [SerializeField] Button playerOneDamageUp;

    [SerializeField] Button playerTwoMaxHealthUp;
    [SerializeField] Button playerTwoHealSelf;
    [SerializeField] Button playerTwoFillAmmo;
    [SerializeField] Button playerTwoMaxAmmoUp;
    [SerializeField] Button playerTwoDamageUp;


    [SerializeField] Text playerTwoPointsText;
    [SerializeField] Text playerOnePointsText;

    [SerializeField] GameObject rootPanelTransform;

    [SerializeField] RobotControler playerOne;
    [SerializeField] RobotControler playerTwo;

    private int deadEnemyPoints = 10;
    private int damageUp = 5;
    private int ammoUp = 20;
    private int healthUp = 10;

    private int playerOnePoints;
    private int playerTwoPoints;

    public void IncrementPlayerPoints(int points)
    {
        playerOnePoints += points;
        playerTwoPoints += points;

        UpdatePlayerPointsGUI ();
    }

    private void UpdatePlayerPointsGUI()
    {
        playerOnePointsText.text = playerOnePoints.ToString();
        playerTwoPointsText.text = playerTwoPoints.ToString();
    }

    private void OnEnemyDeath()
    {
        IncrementPlayerPoints (deadEnemyPoints);
    }

    public void ToogleStore(bool toogle)
    {
        if (toogle) rootPanelTransform.SetActive (true);
        else rootPanelTransform.SetActive (false);
    }
    // Use this for initialization
    void Start () {
        
        Enemy.onEnemyDeath += OnEnemyDeath;

        playerOneMaxHealthUp.onClick.AddListener (PlayerOneMaxHealtUp);
        playerOneHealSelf.onClick.AddListener (PlayerOneHealSelf);
        playerOneFillAmmo.onClick.AddListener (PlayerOneFillAmmo);
        playerOneMaxAmmoUp.onClick.AddListener (PlayerOneMaxAmmoUp);
        playerOneDamageUp.onClick.AddListener (PlayerOneDamageUp);

        playerTwoMaxHealthUp.onClick.AddListener (PlayerTwoMaxHealtUp);
        playerTwoHealSelf.onClick.AddListener (PlayerTwoHealSelf);
        playerTwoFillAmmo.onClick.AddListener (PlayerTwoFillAmmo);
        playerTwoMaxAmmoUp.onClick.AddListener (PlayerTwoMaxAmmoUp);
        playerTwoDamageUp.onClick.AddListener (PlayerTwoDamageUp);
    }
	
    private void PlayerOneMaxHealtUp()
    {
        var obj = playerOneMaxHealthUp.transform.Find ("Cost");
        var cost = int.Parse(obj.GetComponent<Text> ().text);

        if(playerOnePoints >= cost)
        {
            playerOnePoints -= cost;
            UpdatePlayerPointsGUI ();
            playerOne.SetMaxHealth (healthUp);
        }

    }

    private void PlayerOneHealSelf()
    {
        var obj = playerOneHealSelf.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerOnePoints >= cost)
        {
            playerOnePoints -= cost;
            UpdatePlayerPointsGUI ();
            playerOne.SetHealthAsPercentage (100);
        }
    }

    private void PlayerOneFillAmmo()
    {
        var obj = playerOneFillAmmo.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerOnePoints >= cost)
        {
            playerOnePoints -= cost;
            UpdatePlayerPointsGUI ();
            playerOne.FillPlayerAmmunition ();
        }

        
    }

    private void PlayerOneMaxAmmoUp()
    {
        var obj = playerOneMaxAmmoUp.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerOnePoints >= cost)
        {
            playerOnePoints -= cost;
            UpdatePlayerPointsGUI ();
            playerOne.SetMaxAmmo (ammoUp);
        }

    }

    private void PlayerOneDamageUp()
    {
        var obj = playerOneDamageUp.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerOnePoints >= cost)
        {
            playerOnePoints -= cost;
            UpdatePlayerPointsGUI ();
            playerOne.SetDamageUp (damageUp);
        }
    }

    private void PlayerTwoMaxHealtUp()
    {
        var obj = playerTwoMaxHealthUp.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerTwoPoints >= cost)
        {
            playerTwoPoints -= cost;
            UpdatePlayerPointsGUI ();
            playerTwo.SetMaxHealth (healthUp);
        }
    }

    private void PlayerTwoHealSelf()
    {
        var obj = playerTwoHealSelf.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerTwoPoints >= cost)
        {

            playerTwoPoints -= cost;
            UpdatePlayerPointsGUI ();
            playerTwo.SetHealthAsPercentage (100);
        }
    }

    private void PlayerTwoFillAmmo()
    {
        var obj = playerTwoFillAmmo.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerTwoPoints >= cost)
        {
            playerTwoPoints -= cost;
            UpdatePlayerPointsGUI ();
            playerTwo.FillPlayerAmmunition ();
        }
    }

    private void PlayerTwoMaxAmmoUp()
    {
        var obj = playerTwoMaxAmmoUp.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerTwoPoints >= cost)
        {
            playerTwoPoints -= cost;
            UpdatePlayerPointsGUI ();
            playerTwo.SetMaxAmmo (ammoUp);
        }
    }

    private void PlayerTwoDamageUp()
    {
        var obj = playerTwoDamageUp.transform.Find ("Cost");
        var cost = int.Parse (obj.GetComponent<Text> ().text);

        if (playerTwoPoints >= cost)
        {
            playerTwoPoints -= cost;
            UpdatePlayerPointsGUI ();
            playerTwo.SetDamageUp (damageUp);
        }

    }



}
