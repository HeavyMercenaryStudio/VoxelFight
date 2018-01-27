using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrictiveWall : MonoBehaviour, IDamageable {

    [SerializeField] float maxHealth;
    [SerializeField] GameObject destroyedWall;

    float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool IsDestroyed()
    {
        return false;
    }

    public float explosionForce = 10;
    public void TakeDamage(float damage, GameObject bullet)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            GameObject destroyedFragmensts = Instantiate(destroyedWall, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            foreach (Transform t in destroyedFragmensts.transform)
            {
                t.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, t.transform.position, 5f);
                Destroy(t.gameObject, 50f);
            }
            
        }


    }

}
