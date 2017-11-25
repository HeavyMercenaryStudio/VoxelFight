using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    GameObject shooter;
    float damageAmount;
    float destroyRange;

    public void SetDamage(float damage){
		damageAmount = damage;
	}
    public void SetShooter(GameObject shooter){
        this.shooter = shooter;
    }
    public void SetDestroyRange(float range){
        destroyRange = range;
    }


	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == shooter.layer)
            return;

        Component destroyable = other.GetComponent (typeof (IDamageable));

        if (destroyable){
            (destroyable as IDamageable).TakeDamage (damageAmount);
        }

        Destroy (this.gameObject);
    }

    private void Update()
    {
        if (shooter == null)
        {
            Destroy (gameObject);
            return;
        }
        //Check distance to shooter and destroy if above
        float distance =  (this.transform.position - shooter.transform.position).magnitude;
        if (distance > destroyRange)
            Destroy (gameObject);
    }

}
