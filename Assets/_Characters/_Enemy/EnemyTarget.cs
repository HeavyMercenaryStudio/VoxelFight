using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour {

    
    [SerializeField] GameObject target;


    private Collider targetCollider;
    private NavMeshAgent agent;
    private IEnumerator coroutine;
    ZombieCloseAttack zombieCloseAttack;


    void Awake () {
        agent = GetComponent<NavMeshAgent>();
        coroutine = Attack(1.0f);
        targetCollider = target.GetComponent<Collider>();
        zombieCloseAttack = GetComponent<ZombieCloseAttack>();
        agent.isStopped = false;
    }

    public void MoveToLocation()
    {
        agent.destination = target.transform.position;
    }

    private void Update()
    {
        MoveToLocation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            agent.isStopped = true;
            StartCoroutine(coroutine);
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
            zombieCloseAttack.TakeDamage();
            Debug.Log("Uderzono");
        }
    }
}

