using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons { 
    public class RocketProjectile :  Projectile{

        float explosionRadius;
        public void SetExplosionRadius(float radius)
        {
            explosionRadius = radius;
        }

        private new void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == shooter.layer || other.gameObject.layer == this.gameObject.layer || other.gameObject.layer == INTERACTIVE_OBJECT_LAYER)
                return; // friend fire off

            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, explosionRadius);

            foreach (Collider c in hitColliders)
            {
                Component destroyable = c.GetComponent(typeof(IDamageable));
                if (destroyable)
                { // if target is destroyable
                    (destroyable as IDamageable).TakeDamage(damageAmount, this.gameObject);//hit him
                }
            }

            if (other.gameObject.layer != Layers.INTERACTIVE_OBJECT){
                Hit();
                Destroy(this.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(this.transform.position, explosionRadius);
        }

    }
}
