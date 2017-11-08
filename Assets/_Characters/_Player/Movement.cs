     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    

   [SerializeField] float movementSpeed = 10f;
   [SerializeField] bool alternativeMovement = false;

    Rigidbody rigibody;
    Animator anim;
    PlayerController controller;

    Quaternion oldRotation = Quaternion.identity;
    string horizontalAxisName;
    string verticalAxisName;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody> ();
        anim = GetComponent<Animator> ();
        controller = GetComponent<PlayerController> ();

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

            //Vector3 viewPos = Camera.main.WorldToViewportPoint (transform.position);
            //if (viewPos.x > 0.9f || viewPos.x < 0.1f || viewPos.y > 0.9f || viewPos.y < 0.1f)
            //{
            //    transform.position = oldPostion;
            //}               
            //else
            //{
            //    
            //    
            //}    

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
