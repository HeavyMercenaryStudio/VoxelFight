using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour {

    [SerializeField] GameObject target;

    private Enemy enemy;
    private HouseHealth targetHealth;
    private Collider targetCollider;
    private NavMeshAgent agent;
    private IEnumerator coroutine;

    void Start () {

        target = GameObject.FindObjectOfType<HouseHealth> ().gameObject;

        agent = GetComponent<NavMeshAgent>();
        targetCollider = target.GetComponent<Collider>();
        targetHealth = target.GetComponent<HouseHealth> ();
        enemy = GetComponent<Enemy> ();

        agent.destination = target.transform.position;
        coroutine = Attack (enemy.GetZombieAttackSpeed());
        agent.isStopped = false;
    }

    public void MoveToLocation()
    {
        agent.destination = target.transform.position;
        enemy.animatorControler.SetFloat ("Run", agent.velocity.magnitude);
    }

    private void Update()
    {
        if (enemy.isDeath)
        {
            agent.isStopped = true;
            return;
        }

        MoveToLocation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            agent.isStopped = true;
            StartCoroutine (coroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
        {
            agent.isStopped = false;
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator Attack(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (enemy.isDeath){
                StopCoroutine (coroutine);
            }   

            enemy.animatorControler.SetTrigger ("Attack");
            targetHealth.TakeDamage (enemy.GetZombieDamage());
        }
    }
}

