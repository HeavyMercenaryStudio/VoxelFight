using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {

    [SerializeField] float smoothTime = 0.3F;
    [SerializeField] float maxAlongDistance = 35f;
    [SerializeField] Transform[] players;

    Vector3 moveVelocity;
    Vector3 avaragePosition;

    void FixedUpdate(){

        Move ();

        CalculateDistance (); //TODO make this for more than Two players
	}

    private void CalculateDistance()
    {

        //check distance to avarage position of players
        for (int i = 0; i < players.Length; i++) {
             
            float distance = (players[i].position - avaragePosition).magnitude;

            // ...if it its bigger than max distance 
            if (distance > maxAlongDistance)
            {
                //... "block" player position to his current position
                players[i].position = avaragePosition + (players[i].position - avaragePosition).normalized * maxAlongDistance;

            }
        }
    }

    public void Move()
    {
        //check distance to avarage position of player
        for (int i = 0; i < players.Length; i++)
        {
            float distance = (players[i].position - avaragePosition).magnitude;

            //...if it its bigger than max distance - offset don't calculate new camera position 
            if (distance > maxAlongDistance - 0.5f)
                return;
        }

        //Calculate acarage position of players
        avaragePosition = new Vector3 ();
        for (int i = 0; i < players.Length; i++)
            avaragePosition += players[i].position;

        avaragePosition /= players.Length;
        avaragePosition.y = 0f;

        //follow this position
        transform.position = Vector3.SmoothDamp (transform.position, avaragePosition, ref moveVelocity, smoothTime);
         
    }

}
