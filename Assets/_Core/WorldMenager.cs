using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class WorldMenager : MonoBehaviour {

    GameGui GAME_GUI;

    // Use this for initialization
    void Start () {
        GAME_GUI = GameObject.FindObjectOfType<GameGui> (); // FIND game gui panel on map

        PlayerController.notifyPlayerDead += OnPlayerDead; // add listener whe player dead
        MissionObjective.notifyOnObjectiveDestroy += OnObjectiveDestroyed; //add listener when mission obejctive destoryed 

        AudioMenager.Instance.PlayGameMusic ();
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
