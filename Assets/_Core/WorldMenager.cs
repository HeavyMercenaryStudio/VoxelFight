using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WorldMenager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RobotControler.notifyPlayerDead += OnPlayerDead;
        MissionObjective.notifyOnObjectiveDestroy += OnObjectiveDestroyed;
	}

    void OnPlayerDead()
    {
        var players = FindObjectsOfType<RobotControler> ();

        bool allDead = false;
        foreach (RobotControler p in players)
        {
            if (p.IsDestroyed () == false)
            {
                allDead = false;
                break;
            }
            allDead = true;
        }

        if (allDead)
            GameOver ();

    }

    void OnObjectiveDestroyed()
    {
        GameOver ();
    }

    private void GameOver()
    {
        SceneManager.LoadScene (0); // TODO ogólnie gameOver
    }
}
