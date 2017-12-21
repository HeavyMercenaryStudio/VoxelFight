using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDetection : MonoBehaviour {

    public List<Vector2> contactObjects = new List<Vector2>();

    private void OnTriggerEnter(Collider other)
    {
        if (!contactObjects.Contains(new Vector2(other.transform.position.x,other.transform.position.z)))
            contactObjects.Add(new Vector2(other.transform.position.x,other.transform.position.z));
        Debug.Log("Detect: "+contactObjects.Count);
    }

    public List<Vector2> getConObj()
    {
        return contactObjects;
    }
}
