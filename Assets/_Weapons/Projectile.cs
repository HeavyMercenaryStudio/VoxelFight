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
        //Collision with enemy, add damage and destroy projectile
        var tag = other.gameObject.tag;
        switch (tag)
        {
            case "Enemy":
                var enemy = other.gameObject.GetComponent<Enemy> ();
                enemy.TakeDamage (damageAmount);
                Destroy (gameObject);
                break;

            case "Enviorment":
                Destroy (gameObject);
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        //Check distance to shooter and destroy if above
        float distance =  (this.transform.position - shooter.transform.position).magnitude;
        if (distance > destroyRange)
            Destroy (gameObject);
    }

}
