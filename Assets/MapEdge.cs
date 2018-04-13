using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent(typeof(IDamageable));
        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(99999, null);
        }
    }
}
