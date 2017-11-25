using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class EnemyController : MonoBehaviour {

    List<Target> targets;
    Transform currentTarget;
    [SerializeField] GameObject gun;

    [SerializeField] float movementSpeed;
    [SerializeField] float attackRadius;

    private Enemy enemy;
    private Weapon enemyWeapon;
    private NavMeshAgent agent;

    void Start ()
    {
        SetupEnemyTargets ();

        agent = GetComponent<NavMeshAgent> ();
        agent.speed = movementSpeed;

        enemy = GetComponent<Enemy> ();
        enemyWeapon = GetComponent<Weapon> ();

    }

    private void SetupEnemyTargets()
    {
        targets = new List<Target> ();

        var objectives = GameObject.FindObjectsOfType<MissionObjective> ();
        var players = GameObject.FindObjectsOfType<PlayerController> ();

        foreach (MissionObjective ob in objectives)
        {
            var tar = new Target (ob.transform);
            targets.Add (tar);
        }
        foreach (PlayerController pl in players)
        {
            var tar = new Target (pl.transform);
            targets.Add (tar);
        }

    }

    public void MoveToLocation()
    {
        //set destination
        agent.destination = currentTarget.position;

        //set look rotation
        var targetPos = currentTarget.position;
        gun.transform.LookAt (targetPos);

        targetPos.y = 0;
        this.transform.LookAt (targetPos);
    }

    private void Update()
    {
        FindCloseTarget ();

        MoveToLocation();
    }

    private void FindCloseTarget()
    {
        //Calculate distance foreach target
        foreach (Target t in targets){

            var da = t.Transf.GetComponent<IDamageable> ();

            if (da.IsDestroyed ()) t.Distance = 9999;
            else t.CalculateDistance (this.transform);
        }

        //Select closer
        List<Target> sorted = targets.OrderBy (o => o.Distance).ToList ();
        currentTarget = sorted[0].Transf;

        //Attack target
        if (sorted[0].Distance <= attackRadius)
        {
            enemyWeapon.TryShoot ();
            agent.isStopped = true;
        }
        else
            agent.isStopped = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, attackRadius);
    }

}

class Target
{
    public Transform Transf;
    public float Distance;

    public Target(Transform t)
    {
        Transf = t;
    }

    public void CalculateDistance(Transform e)
    {
        Distance = Vector3.Distance (Transf.position, e.position);
    }

}

