     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

   [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] bool alternativeMovement = false;

    Rigidbody rigibody;
    Animator anim;
    RobotControler controller;

    Quaternion oldRotation = Quaternion.identity;
    string horizontalAxisName;
    string verticalAxisName;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody> ();
        anim = GetComponent<Animator> ();
        controller = GetComponent<RobotControler> ();

        horizontalAxisName = "Horizontal" + controller.GetPlayerNumber ();
        verticalAxisName = "Vertical" + controller.GetPlayerNumber ();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isDead) return;

        float h = Input.GetAxis (horizontalAxisName);
        float v = Input.GetAxis (verticalAxisName);

        Move (h, v);
    }

   
    void Move(float horizontal, float vertical)
    {

        if (alternativeMovement)
        {

            float degeres = horizontal * rotationSpeed * Time.deltaTime;
            transform.RotateAround (transform.position, transform.up, degeres);
    
//            if(Mathf.Abs(horizontal) < 0.5f) {

                Vector3 moveVector = transform.forward * vertical * Time.deltaTime * movementSpeed;
               anim.SetFloat ("Run", moveVector.normalized.magnitude);

                moveVector += transform.position;
                rigibody.MovePosition (moveVector);

  //          }



            /*
            //ROTATION
            Vector3 rotation = new Vector3 (horizontal, 0, vertical){
                y = 0f
            };

            if (rotation.magnitude > 0){
                Quaternion newRotation = Quaternion.LookRotation (rotation);
                rigibody.MoveRotation (newRotation);
                oldRotation = transform.rotation;
            }
            else{
               transform.rotation = oldRotation;
            }


            //MOVEMENT
            Vector3 moveVector = new Vector3 (horizontal, 0, vertical);
            anim.SetFloat ("Run", moveVector.normalized.magnitude);

            moveVector *= Time.deltaTime * movementSpeed;
            moveVector += transform.position;

            rigibody.MovePosition (moveVector);   
            */
        }
        else
        {
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (camRay, out hit))
            {
                Vector3 playerToMouse = hit.point - transform.position;
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
                rigibody.MoveRotation (newRotation);

                Vector3 moveVector = new Vector3(horizontal,0,vertical);

                anim.SetFloat ("Run", moveVector.normalized.magnitude);

                moveVector *= Time.deltaTime * movementSpeed;
                moveVector += transform.position;
                rigibody.MovePosition (moveVector);
            }
        }

        
    }
}
