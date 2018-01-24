using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IDamageable {

    [SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] float startDealay;
    [SerializeField] float maxHealthPoints;
    [SerializeField] GameObject bloodPrefab;


    float currentHealhPoints;

    void Start()
    {
        currentHealhPoints = maxHealthPoints;
        StartCoroutine(WaitForGameStart());
    }
    public bool IsDestroyed()
    {
        return false;
    }

    public void TakeDamage(float damage, GameObject bullet)
    {
        currentHealhPoints -= damage;
        if (currentHealhPoints <= 0)
        {

            //Spawn Blood Particle
            bloodPrefab.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject blood1 = Instantiate(bloodPrefab);
            var particles = blood1.GetComponent<ParticleSystem>().main;
            particles.maxParticles = (int)(maxHealthPoints / 5);
            Destroy(blood1, 5f); // and destroy it with delay

            Destroy(this.gameObject);
        }
    }

    private IEnumerator WaitForGameStart()
    {
        yield return new WaitForSeconds(startDealay);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (currentHealhPoints > 0)
        {
            GameObject newEnemy = Instantiate(enemy, transform.localPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

}
