using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    GameObject mainTarget;
    PlayerController[] playerTargets;
    GameObject currentTarget;

    [SerializeField] float movementSpeed;

    [SerializeField] float chaseRadius;
    [SerializeField] float attackRadius;

    private Enemy enemy;
    private Weapon enemyWeapon;
    private NavMeshAgent agent;

    private bool isAttacking;

    void Start () {

        mainTarget = GameObject.FindObjectOfType<MissionObjective> ().gameObject;
        playerTargets = GameObject.FindObjectsOfType<PlayerController> ();

        currentTarget = mainTarget;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.destination = mainTarget.transform.position;

        enemy = GetComponent<Enemy> ();
        enemyWeapon = GetComponent<Weapon> ();

    }

    public void MoveToLocation()
    {
        agent.destination = currentTarget.transform.position;
        enemy.animatorControler.SetFloat ("Run", agent.velocity.magnitude);
    }

    private void Update()
    {
        if (enemy.isDestroyed)
        {
            agent.isStopped = true;
            return;
        }

        FindCloseTarget ();

        MoveToLocation();
    }

    private void FindCloseTarget()
    {
        float p1Dist = Vector3.Distance (transform.position, playerTargets[0].transform.position); //TODO MAKE THIS FOR MORE THAN TWO PLAYERS
        float p2Dist = Vector3.Distance (transform.position, playerTargets[1].transform.position);
        float mainDist = Vector3.Distance (transform.position, mainTarget.transform.position);

        SelectTargetToFollow (p1Dist, p2Dist);

        AttackTarget (p1Dist, p2Dist, mainDist);

    }

    private void AttackTarget(float p1Dist, float p2Dist, float maDistance)
    {
        //Attack target
        if (p2Dist <= attackRadius || p1Dist <= attackRadius || maDistance <= attackRadius)
        {
            enemyWeapon.TryShoot ();
        }
    }

    private void SelectTargetToFollow(float p1Dist, float p2Dist)
    {
        //Chase target
        var p1 = playerTargets[0].GetComponent<IDamageable> ();
        var p2 = playerTargets[1].GetComponent<IDamageable> ();

        if (p1Dist <= chaseRadius && !p1.IsDestroyed ())
            currentTarget = playerTargets[0].gameObject;
        else if (p2Dist <= chaseRadius && !p2.IsDestroyed ())
            currentTarget = playerTargets[1].gameObject;
        else
            currentTarget = mainTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, chaseRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, attackRadius);
    }

}

