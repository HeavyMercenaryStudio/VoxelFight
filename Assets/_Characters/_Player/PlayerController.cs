using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float seconsBetweenShot = 0.5f;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileSpawnPoint;

    private void Update()
    {
        if (Input.GetKey (KeyCode.Mouse0))
            SpawnProjectile ();
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate (projectile, projectileSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        Projectile projectileComponent = newProjectile.GetComponent<Projectile> ();

       projectileComponent.setDamage (damagePerShot);
       // projectileComponent.SetShooter (gameObject);

        Vector3 unitVector = (this.transform.forward).normalized;
        newProjectile.GetComponent<Rigidbody> ().velocity = unitVector * projectileComponent.GetDefaultProjectileSpeed ();
    }
}
