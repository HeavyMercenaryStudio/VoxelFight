using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float projectlieSpeed;

	const float DESTROY_DELAY = 0.01f;

	float damageAmount;
	GameObject shooter;

	public void setDamage(float damage){
		damageAmount = damage;
	}

	public float GetDefaultProjectileSpeed(){
		return projectlieSpeed;
	}

	public void SetShooter(GameObject _shooter){
		shooter = _shooter;
	}

	void OnCollisionEnter(Collision other)
	{

        //Component damageableComponent = other.gameObject.GetComponent (typeof(IDamageable));

        //if (damageableComponent){
        //	(damageableComponent as IDamageable).TakeDamage (damageAmount);
        //}
        if (other.gameObject.CompareTag ("Enemy")) {
            var enemy = other.gameObject.GetComponent<Enemy> ();
            enemy.TakeDamage (damageAmount);
		    Destroy (gameObject, DESTROY_DELAY);
        }
    }

}
