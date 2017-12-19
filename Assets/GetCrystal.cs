using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCrystal : MonoBehaviour {

    public float speed=15;
    public float moveDistance=10;
    public float collectDistance=0.5f;
    public ParticleSystem blood;
    public ParticleSystem.Particle[] ParticleList;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<GameObject> players=new List<GameObject>();
    List<float> distances;


    private void OnEnable()
    {
        blood = GetComponent<ParticleSystem>();
    }

    void Start () {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Debug.Log("Number of players: "+players.Count);
        blood.Emit(5);

    }



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

                if (closer < collectDistance && id != -1)
                {
                    ParticleList[z].remainingLifetime = -1;
                    players[id].GetComponent<PlayerScore>().addCrystals(1);
                }

                 if (closer <= moveDistance && id != -1)
                    ParticleList[z].position = Vector3.MoveTowards(ParticleList[z].position, players[id].transform.position, speed * Time.fixedDeltaTime);
            }

            blood.SetParticles(ParticleList, blood.particleCount);
        }
    }


}
