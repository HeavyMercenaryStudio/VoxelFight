using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiPanel : MonoBehaviour {

    [SerializeField] Text ammoText;
    PlayerController playerController;

    public void Start()
    {
        playerController = GetComponent<PlayerController> ();
        playerController.notifyOnShoot += OnShoot;
    }

    void OnShoot(float ammoAmout)
    {
        ammoText.text = "Ammo: " + ammoAmout.ToString ();
    }
   


}
