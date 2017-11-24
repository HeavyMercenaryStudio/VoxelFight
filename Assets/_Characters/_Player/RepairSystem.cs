using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairSystem : MonoBehaviour {

    [SerializeField] Image repairBar;

    Transform[] players;
    CameraFollow cameraFollow;
    RobotControler controller;
    static float maxRessDistance = 6f;
    static float maxRessTime = 2.5f;

    float currentRessTime;

    // Use this for initialization
    void Start () {

        controller = GetComponent<RobotControler> ();
        cameraFollow = FindObjectOfType<CameraFollow> ();
        players = cameraFollow.GetPlayers ();

        repairBar.transform.parent.parent.gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {

       // if (controller.IsDestroyed()) WaitForRes ();
	}

    private void WaitForRes()
    {
        float distance = (players[0].position - players[1].position).magnitude;

        // ...if it its bigger than max distance 
        if (distance < maxRessDistance)
        {
            TryRess ();
        }
    }

    private void TryRess()
    {
       
        if (Input.GetButton ("Ress" + controller.GetPlayerNumber ()))
        {
            repairBar.transform.parent.parent.gameObject.SetActive (true);
            currentRessTime += Time.deltaTime;
            repairBar.fillAmount = currentRessTime / maxRessTime;

            if (currentRessTime > maxRessTime)
            {
                repairBar.transform.parent.parent.gameObject.SetActive (false);
                controller.SetHealthAsPercentage (50f);
                controller.SetDestroyed(false);
                //play animation
            }
        }
        else
        {
            repairBar.transform.parent.parent.gameObject.SetActive (false);
            currentRessTime = 0;
        }
    }
}
