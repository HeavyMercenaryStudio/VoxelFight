using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour, IDamageable
 {
    [SerializeField] float interruptTime;
    [SerializeField] int ammoPerSecond;
    [SerializeField] float healthPercentagePerSecond;

    float lastHealth;
    float interrupt;

    Material material;

    void Start()
    {
        material = GetComponent<Renderer> ().material;
    }

    void Update()
    {
        if(interrupt >= 0)
            interrupt -= Time.deltaTime;
    }

    public bool IsDestroyed()
    {
        return false;
    }

    public void TakeDamage(float damage, GameObject bullet)
    {
        var proj = bullet.GetComponent<Projectile> ();
        var shooterIsEnemy = proj.GetShooter ().GetComponent<Enemy> ();

        if (shooterIsEnemy)
        {
            interrupt = interruptTime;
            material.color = Color.red;
        }
    }

    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController> ();
        if (player && Time.time > lastHealth && interrupt <= 0) //only every second if player stay on platform
        {
            player.HealMe (healthPercentagePerSecond); // ADD percentage health
            player.ReloadMe (ammoPerSecond); //add ammo

            lastHealth = Time.time + 1f; //heal every second
            material.color = Color.green; //set color of platform
        }
    }

    void OnTriggerExit(Collider other )
    {
        var player = other.GetComponent<PlayerController> ();
        if (player && interrupt <= 0)
            material.color = Color.white; //reset platform color
    }
}
