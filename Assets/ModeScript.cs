using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeScript : MonoBehaviour {

    public enum Modes
    {
        Survival,
        Maze
    };
    [SerializeField] Modes gameMode;

    public void LoadScene()
    {
        switch (gameMode)
        {
            case Modes.Survival:
                SceneManager.LoadScene("Mission#1");
                break;
            case Modes.Maze:
                SceneManager.LoadScene("MazeModeInfinity");
                break;
            default:
                break;
        }
    }
}
