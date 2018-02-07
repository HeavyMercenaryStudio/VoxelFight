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
    public float explosionRadius = 10;
    public void TakeDamage(float damage, GameObject bullet)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            GameObject destroyedFragmensts = Instantiate(destroyedWall, transform.position, Quaternion.identity);
           // var t = destroyedFragmensts.transform.GetChild(0);

            foreach (Transform t in destroyedFragmensts.transform)
            {
                t.GetComponent<Rigidbody>().AddForceAtPosition(bullet.transform.forward * explosionForce, bullet.transform.position);
            }


            Destroy(destroyedFragmensts, 50f);
            Destroy(this.gameObject);
        }


    }

}
