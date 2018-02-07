using CameraUI;
using Characters;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExit : MonoBehaviour {

    List<PlayerController> players = new List<PlayerController>();

    GameGui gameGUI;
    PlayerController[] allPlayers;

    void Start()
    {
        gameGUI = FindObjectOfType<GameGui>();
        GetPlayers();
    }
    private void GetPlayers()
    {
       allPlayers = FindObjectsOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        int alivePlayersCount = 0;
        for (int i = 0; i < allPlayers.Length; i++)
            if (!allPlayers[i].IsDestroyed()) alivePlayersCount++;

        var player = other.GetComponent<PlayerController>();
        if (player) //only every second if player stay on platform
        {
            if (!players.Contains(player))
                players.Add(player);

            if (players.Count == alivePlayersCount) { 
   
                var infM = FindObjectOfType<MazeInfinityMode>();
                if (infM)
                {
                    gameGUI.VictoryWithNoReturn();
                    infM.LoadNextLvl();
                    SetPlayersHealth();
                }
                else gameGUI.Victory();

            }
        }
    }

    private void SetPlayersHealth()
    {
        for (int i = 0; i < WorldData.NumberOfPlayers; i++)
        {
            var player = allPlayers[i];
            player.HealMe(10);
            WorldData.PlayerHealth[player.GetPlayerNumber()] = player.GetHealthAsPercentage() * 100f;
        }
    }
}
