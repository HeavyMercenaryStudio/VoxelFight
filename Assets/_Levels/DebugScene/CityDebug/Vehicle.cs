using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vehicle : MonoBehaviour {

    [SerializeField] float speed;
    NavMeshAgent navMesh;
    [SerializeField] WayPoints waypoints;

    List<GameObject> path = new List<GameObject>();
    int pathCount;

    // Use this for initialization
	void Start () {

        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;

        path = waypoints.GetAllPoints();

        navMesh.SetDestination(path[0].transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(navMesh.remainingDistance < navMesh.stoppingDistance)
        {
            if (pathCount == path.Count-1)
                pathCount = 0;
            else
                pathCount++;

            navMesh.SetDestination(path[pathCount].transform.position);
            Debug.Log("hehe");
        }

	}


    void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent(typeof(IDamageable));
        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(99999, null);
        }
    }
}
