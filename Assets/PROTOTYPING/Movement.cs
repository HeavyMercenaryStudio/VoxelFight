using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    Rigidbody rigibody;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move (h, v);
    }

    void Move(float horizontal, float vertical)
    {
        Vector3 rotationVector = new Vector3 (0, horizontal, 0);
        //rotationVector *= Time.deltaTime * maxRotationSpeed;

        Quaternion deltaRotation = Quaternion.Euler (rotationVector);
        rigibody.MoveRotation (rigibody.rotation * deltaRotation);

        Vector3 moveVector = (vertical) * transform.forward;
        //moveVector *= Time.deltaTime * maxCarSpeed;

        moveVector += transform.position;
        rigibody.MovePosition (moveVector);
    }
}
