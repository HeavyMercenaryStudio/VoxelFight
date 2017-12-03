using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionScript : MonoBehaviour {

    Mission objectMission;
    
    public void SetMission(Mission m)
    {
        objectMission = m;
        SetMissionButtonAction ();
    }
    public Mission GetMission()
    {
        return objectMission;
    }
    public void SetMissionButtonAction()
    {
        var button = this.GetComponent<Button> ();
        button.onClick.AddListener (LoadMissionScene);
    }

    public void LoadMissionScene()
    {
        var scene = objectMission.GetMissionScene ();
        WorldData.NextMission = objectMission.GetNextMission();
        SceneManager.LoadScene (scene.name);
    }

}
