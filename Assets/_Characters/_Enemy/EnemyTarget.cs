using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject target;
    private Collider targetCollider;

	void Awake () {
        agent = GetComponent<NavMeshAgent>();
        targetCollider = target.GetComponent<CapsuleCollider>();
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
            agent.isStopped=true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
            agent.isStopped = false;
    }


}
