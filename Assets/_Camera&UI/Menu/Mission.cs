using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu (menuName = "Mission")]
public class Mission : ScriptableObject {

    [SerializeField] GameObject missionPrefab;
    [SerializeField] string missionLevelName;
    [SerializeField] bool completed;
    [SerializeField] string missionDescription;
    [SerializeField] Image missionImage;
    [SerializeField] Mission nextMission;

    public GameObject GetMissionPrefab()
    {
        missionPrefab.GetComponentInChildren<Text> ().text = missionDescription;
        return missionPrefab;
    }
    public string GetMissionScene(){
        return missionLevelName;
    }
    public Mission GetNextMission(){
        return nextMission;
    }

    public bool IsMissionComplete()
    {
        return completed;
    }
    public void SetCompleted()
    {
        completed = true;
    }
}
