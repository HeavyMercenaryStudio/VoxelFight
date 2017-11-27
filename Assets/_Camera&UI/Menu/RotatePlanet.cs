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
            Vector3 move = new Vector3 (Input.GetAxis ("Mouse Y"), -Input.GetAxis ("Mouse X"), 0).normalized;
            
            //if mouse are moving...
            if (move.magnitude > 0)
            {
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
