using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        //check for ui iteraction
        if (EventSystem.current.IsPointerOverGameObject ())
        {
            return;
        }
        
        if (Input.GetKey (KeyCode.Mouse0))
        {
            //Check mouse input
            Vector3 move = new Vector3 (Input.GetAxis("Mouse Y"), -Input.GetAxis ("Mouse X"), 0).normalized;
            
            //if mouse are moving...
            if (move.magnitude > 0)
            {

                //Make vector from start to end point...
                //end = Input.mousePosition;

                //var vec = (end - start).normalized;
                //vec *= Vector3.Distance (end, start);

               
                //Chance directions
                //var x = vec.x;
                //vec.x = vec.y;
                //vec.y = -x;

                //vec.z = 0;
                
                //add torque
                rigibody.AddTorque (move * rotateSpeed, ForceMode.Acceleration);

            }
            else
            {
                start = Input.mousePosition;
            }

        }
    }
}
