using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Characters
{
    /// <summary>
    /// Control player movement
    /// </summary>
    [RequireComponent (typeof (Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 10f; // speed of player

        float currentMovementSpeed;
        public void BoostMe(float boostAmount)
        {
            movementSpeed = currentMovementSpeed + boostAmount;
        } // boost player 

        Rigidbody rigibody;
        PlayerController controller;

        string horizontalAxisName;
        string verticalAxisName;

        float screenBlockValue = 25f; //stop player when reach distance of to edge of screen

        private void Start()
        {
            currentMovementSpeed = movementSpeed;

            rigibody = GetComponent<Rigidbody> ();
            controller = GetComponent<PlayerController> ();

            horizontalAxisName = "Horizontal" + controller.GetPlayerNumber ();
            verticalAxisName = "Vertical" + controller.GetPlayerNumber ();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (controller.IsDestroyed ()) return; // if character is destroy dont take any action..

            float h = Input.GetAxis (horizontalAxisName); // get input of horizontal Axis..
            float v = Input.GetAxis (verticalAxisName); // get input of vertical Axis

            ControlPlayer (h, v); 
        }

        void ControlPlayer(float horizontal, float vertical)
        {
            if (controller.GetPlayerNumber () == 1) //if it is player one
                RotateWithMouse (); // rotate with mouse
            else // otherwise..
                RotateWithJoy (horizontal, vertical); // rotate using analog 

            if (CheckScreenEdge (horizontal, vertical)) // check if character reach screen edge ...
                MovePlayer (horizontal, vertical); // if dont... enable movement

        }

        private void MovePlayer(float horizontal, float vertical) 
        {
            Vector3 moveVector = new Vector3 (horizontal, 0, vertical); // create vector using inputs

            moveVector *= Time.deltaTime * movementSpeed; // clamp it by FPS 
            moveVector += transform.position; // update position
            rigibody.MovePosition (moveVector); // set the player's position to this new position
        }

        private void RotateWithJoy(float horizontal, float vertical)
        {
            // get input of analog
            Vector3 turnDir = new Vector3 (Input.GetAxis("JoystickHorizontal"), 0f, Input.GetAxis("JoystickVertical"));

            if (turnDir.magnitude > 0.1f) //if magniture of input vector ...
            {
                //create rotation vector
                Vector3 playertomouse = (transform.position + turnDir) - transform.position;
                playertomouse.y = 0f; // dont rotate by y axis

                // create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newrotatation = Quaternion.LookRotation (playertomouse);

                // set the player's rotation to this new rotation.
                rigibody.MoveRotation (newrotatation);
            }
        }

        private void RotateWithMouse()
        {
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); // create raycast depends on mouse position
            RaycastHit hit;

            if (Physics.Raycast (camRay, out hit)) // if raycast hit something
            {
                Vector3 playerToMouse = hit.point - transform.position; //create rotation vector
                playerToMouse.y = 0f; // dont rotate with y axis
                Quaternion newRotation = Quaternion.LookRotation (playerToMouse); // create new rotation
                rigibody.MoveRotation (newRotation); // set player's rotation to new rotation
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
                return false; // don't move
            }

            return true; // move
        }

    }
}