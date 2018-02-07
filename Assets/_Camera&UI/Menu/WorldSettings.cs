using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour {

    [SerializeField] int MAZE_START_SIZE;

    // Use this for initialization
    void Start () {
        
        for (int i = 1; i <= WorldData.NumberOfPlayers; i++) {
            WorldData.PlayerHealth.Add(i, 100);
        }

        WorldData.InifinityModeSize = MAZE_START_SIZE;
	}
	
}
