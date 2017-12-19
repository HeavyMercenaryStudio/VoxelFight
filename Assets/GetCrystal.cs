using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCrystal : MonoBehaviour {

    public ParticleSystem blood;
    public Transform player;
    public ParticleSystem.Particle[] ParticleList;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<GameObject> players=new List<GameObject>();
    List<float> distances;


    //private float particleSize;

    private void OnEnable()
    {
        blood = GetComponent<ParticleSystem>();
        //particleSize = blood.startSize;
    }

    void Start () {

        //player = GameObject.Find("Player1(Clone)").transform;
        //this.blood.trigger.SetCollider(0,GameObject.Find("Player1(Clone)").GetComponent<BoxCollider>());
        //players = GameObject.Find("_CORE_").GetComponent<WorldMenager>().getPlayers();
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Debug.Log("Number of players: "+players.Count);
        blood.Emit(5);

    }

    //void OnParticleTrigger()
    //{
    //    // get the particles which matched the trigger conditions this frame
    //    int numEnter = blood.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    //    // iterate through the particles which entered the trigger and make them red
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];
    //        p.remainingLifetime = -1;
    //        score.addCrystals(1);
    //        Debug.Log("test");
    //        enter[i] = p;
    //    }

    //    // re-assign the modified particles back into the particle system
    //    blood.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //}



    void Update () {
        if (blood != null)
        {
            ParticleList = new ParticleSystem.Particle[blood.particleCount];
            blood.GetParticles(ParticleList);
            for (int z = 0; z < GetComponent<ParticleSystem>().particleCount; z++)
            {
                float closer = 10000;
                int id = -1;
                for (int i = 0; i < players.Count; i++)
                {

                    float dist = (ParticleList[z].position - players[i].transform.position).magnitude;

                    if (dist < closer)
                    {
                        closer = dist;
                        id = i;
                    }
                }

                if (closer < 1.5f && id != -1)
                {
                    ParticleList[z].remainingLifetime = -1;
                    players[id].GetComponent<PlayerScore>().addCrystals(1);
                }

                 if (closer <= 10 && id != -1)
                {
                    ParticleList[z].position = Vector3.MoveTowards(ParticleList[z].position, players[id].transform.position, 20 * Time.fixedDeltaTime);
                    //particleSize =Mathf.Lerp(particleSize,0f,Time.deltaTime/4);
                    //ParticleList[z].startSize = particleSize;
                }
            }

            blood.SetParticles(ParticleList, blood.particleCount);
        }

    }


}
