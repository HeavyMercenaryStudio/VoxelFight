using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

   [SerializeField] float movementSpeed = 10f;
   [SerializeField] float rotationSpeed = 100f;

    Rigidbody rigibody;
    Animator anim;

    [SerializeField] bool alternativeMovement = false;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody> ();
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");

        Move (h, v);

    }

    void Move(float horizontal, float vertical)
    {

        if (alternativeMovement)
        {
            Vector3 rotationVector = new Vector3 (0, horizontal, 0);
            rotationVector *= Time.deltaTime * rotationSpeed;

            Quaternion deltaRotation = Quaternion.Euler (rotationVector);
            rigibody.MoveRotation (rigibody.rotation * deltaRotation);

            Vector3 moveVector = (vertical) * transform.forward;
            moveVector *= Time.deltaTime * movementSpeed;

            moveVector += transform.position;
            rigibody.MovePosition (moveVector);
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
