using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class WorldMenager : MonoBehaviour {

    GameGui GAME_GUI;

    [SerializeField] List<GameObject> players;

    Vector3 playerSpawnPosition = new Vector3 (-20, 4, 0);

    private void Awake()
    {
        List<Transform> targets = new List<Transform> (); // create new list 
        for (int i = 0; i < WorldData.NumberOfPlayers; i++){// instatiate new players... 
          GameObject player = Instantiate (players[i], playerSpawnPosition + new Vector3(i * 5, 0, 0), Quaternion.identity);
            targets.Add (player.transform);
        }
        var CamF = GameObject.FindObjectOfType<CameraFollow> ();
        CamF.SetTransformTargets (targets); // Set targets to camera follow

    }

    void Start () {
        GAME_GUI = GameObject.FindObjectOfType<GameGui> (); // FIND game gui panel on map

        PlayerController.notifyPlayerDead += OnPlayerDead; // add listener whe player dead
        MissionObjective.notifyOnObjectiveDestroy += OnObjectiveDestroyed; //add listener when mission obejctive destoryed 

       //    AudioMenager.Instance.PlayGameMusic ();
    }

    void OnPlayerDead()
    {
        var players = FindObjectsOfType<PlayerController> (); // get all players....

        bool allDead = false; // check if it was las standing player ...
        foreach (PlayerController p in players)
        {
            if (p.IsDestroyed () == false)
            {
                allDead = false;
                break;
            }
            allDead = true;
        }


        if (allDead) // if yes ...
            OnDefeat (); // mission failed
    }

    void OnObjectiveDestroyed()
    {
        OnDefeat (); // when mission object is destroyed ... mission failed
    }

    void OnDefeat()
    {
        GAME_GUI.Defeat (); // Update GUI panel
    }

}
