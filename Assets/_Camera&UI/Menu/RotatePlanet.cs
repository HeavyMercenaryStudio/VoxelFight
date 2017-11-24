using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour {

    [SerializeField] GameObject planet;
    [SerializeField] float rotateSpeed;

    Rigidbody rigibody;
    Vector3 start;
    Vector3 end;

    // Use this for initialization
    void Start () {
        rigibody = planet.GetComponent<Rigidbody> ();
    }

	void Update ()
    {
        MovePlanet ();
    }

    private void MovePlanet()
    {
        if (Input.GetKeyDown (KeyCode.Mouse0))
        {
            start = Input.mousePosition;
        }
        else if (Input.GetKey (KeyCode.Mouse0))
        {
            //Check mouse input
            Vector3 move = new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);

            //if mouse are moving...
            if (move.magnitude > 0)
            {

                //Make vector from start to end point...
                end = Input.mousePosition;

                var vec = (end - start).normalized;
                vec *= Vector3.Distance (end, start);

                //Chance directions
                var x = vec.x;
                vec.x = vec.y;
                vec.y = -x;

                //add torque
                rigibody.AddTorque (vec);
            }
        }
    }
}
