using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.SceneManagement;

public class MazeInfinityMode : MonoBehaviour {

    
	// Use this for initialization
	void Awake () {

        var generator = FindObjectOfType<MazeGen>();
        generator.mazeHeight = WorldData.InifinityModeSize;
        generator.mazeWidht = WorldData.InifinityModeSize;
        WorldData.InifinityModeSize += 2;
    }

    public void LoadNextLvl()
    {
        StartCoroutine(NextLvl());
    }

    IEnumerator NextLvl()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MazeModeInfinity");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
