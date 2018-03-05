using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent(typeof(IDamageable));
        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(99999, null);
        }
    }

}
