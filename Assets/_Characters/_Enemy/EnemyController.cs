using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Weapons;
using WorldObjects;

namespace Characters { 

    //This script control enemy movement, attack etc.

    /// <summary>
    /// Control Enemy AI
    /// </summary>
    public class EnemyController : MonoBehaviour {

        List<Target> targets; //list of potentially targets to attack
        Transform currentTarget; //current followed target
        [SerializeField] GameObject gun; // weapon using by enemy

        [SerializeField] float movementSpeed; // movement speed of enemy
        [SerializeField] float attackRadius; // radius area to start attacking target


        private Weapon enemyWeapon; // weapons used by enemy
        private NavMeshAgent agent; // nav mesh agent used to navigate 

        void Start ()
        {
            SetupEnemyTargets (); //Find all targets on map

            agent = GetComponent<NavMeshAgent> (); // get nav mesh
            agent.speed = movementSpeed;
            
            enemyWeapon = GetComponent<Weapon> (); // get weapon

            StartCoroutine(ThinkTime());
        }

        IEnumerator ThinkTime()
        {
            yield return new WaitForSeconds(2f);
            agent.enabled = true;
        }

        private void SetupEnemyTargets()
        {
            targets = new List<Target> (); // create new list

            var objectives = GameObject.FindObjectsOfType<MissionObjective> (); // if there any mission objects on map...
            var players = GameObject.FindObjectsOfType<PlayerController> ();// or players

            foreach (MissionObjective ob in objectives) // add them to list...
            {
                var tar = new Target (ob.transform);
                targets.Add (tar);
            }
            foreach (PlayerController pl in players) //same.
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
            if (agent.enabled) { 
                FindCloseTarget ();
                MoveToLocation();
            }
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

    // Potentially target
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

}
