using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    [SerializeField] float movementSpeed = 10f;
    //[SerializeField] float rotationSpeed = 10f;

    Rigidbody rigibody;
    Animator animator;
    PlayerController controller;

    string horizontalAxisName;
    string verticalAxisName;

    float screenBlockValue = 25f;
    private void Start()
    {
        rigibody = GetComponent<Rigidbody> ();
        controller = GetComponent<PlayerController> ();

        horizontalAxisName = "Horizontal" + controller.GetPlayerNumber ();
        verticalAxisName = "Vertical" + controller.GetPlayerNumber ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.IsDestroyed()) return;

        float h = Input.GetAxis (horizontalAxisName);
        float v = Input.GetAxis (verticalAxisName);

        ControlPlayer (h, v);
    }
   
    void ControlPlayer(float horizontal, float vertical)
    {
        if (controller.GetPlayerNumber() == 1)
            RotateWithMouse ();
        else
            RotateWithJoy (horizontal, vertical);

        if(CheckScreenEdge (horizontal, vertical))
            MovePlayer (horizontal, vertical);

    }

    private void MovePlayer(float horizontal, float vertical)
    {
        //Move player
        Vector3 moveVector = new Vector3 (horizontal, 0, vertical);

        moveVector *= Time.deltaTime * movementSpeed;
        moveVector += transform.position;
        rigibody.MovePosition (moveVector);
    }

    private void RotateWithJoy(float horizontal, float vertical)
    {
        Vector3 turnDir = new Vector3 (Input.GetAxisRaw("JoystickHorizontal"), 0f, Input.GetAxisRaw ("JoystickVertical"));

        if (turnDir.magnitude > 0.1f)
        {
            Vector3 playertomouse = (transform.position + turnDir) - transform.position;
            playertomouse.y = 0f;

            // create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newrotatation = Quaternion.LookRotation (playertomouse);

            // set the player's rotation to this new rotation.
            rigibody.MoveRotation (newrotatation);
        }
    }

    private void RotateWithMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast (camRay, out hit))
        {
            //Rotate player
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
            rigibody.MoveRotation (newRotation);
        }
    }

    private bool CheckScreenEdge(float horizontal, float vertical)
    {
        //Transfer position of player into screen point
        Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);

        //Check if position is on edge of camera view 
        if ((screenPos.x < screenBlockValue && horizontal < 0) ||
             (screenPos.x > Screen.width - screenBlockValue && horizontal > 0) ||
             (screenPos.y < screenBlockValue && vertical < 0) ||
             (screenPos.y > Screen.height - screenBlockValue && vertical > 0))
        {
            return false;
        }

        return true;
    }

}
