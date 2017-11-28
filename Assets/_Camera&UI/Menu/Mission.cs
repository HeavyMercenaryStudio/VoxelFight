using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu (menuName = "Mission")]
public class Mission : ScriptableObject {

    [SerializeField] GameObject missionPrefab;
    [SerializeField] Object missionLevel;
    [SerializeField] bool completed;
    [SerializeField] string missionDescription;
    [SerializeField] Image missionImage;


    public GameObject GetMissionPrefab()
    {
        missionPrefab.GetComponentInChildren<Text> ().text = missionDescription;

        return missionPrefab;
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
