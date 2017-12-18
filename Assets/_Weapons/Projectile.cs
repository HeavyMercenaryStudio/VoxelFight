using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons { 

    /// <summary>
    /// Spawning projectile.
    /// </summary>
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
        public GameObject GetShooter()
        {
            return shooter;
        }
        public void SetDestroyRange(float range){
            destroyRange = range;
        }


	    void OnTriggerEnter(Collider other)
	    {
            if (other.gameObject.layer == shooter.layer || other.gameObject.layer == this.gameObject.layer)
                return; // friend fire off
        
            Component destroyable = other.GetComponent (typeof (IDamageable));

            if (destroyable){ // if target is destroy able
                (destroyable as IDamageable).TakeDamage (damageAmount, this.gameObject);//hit him
            }
            else
            { // if not destory bullet
                Destroy (this.gameObject);
            }
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
}
