using CameraUI;
using Characters;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExit : MonoBehaviour {

    List<PlayerController> players = new List<PlayerController>();

    GameGui gameGUI;

    void Start()
    {
        gameGUI = FindObjectOfType<GameGui>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player) //only every second if player stay on platform
        {
            if (!players.Contains(player))
                players.Add(player);

            if (players.Count == WorldData.NumberOfPlayers) { 
   
                var infM = FindObjectOfType<MazeInfinityMode>();
                if (infM) { gameGUI.VictoryWithNoReturn(); infM.LoadNextLvl(); }
                else gameGUI.Victory();

            }
        }
    }
}
