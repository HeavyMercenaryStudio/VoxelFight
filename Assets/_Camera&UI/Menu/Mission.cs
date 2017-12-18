using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CameraUI { 

    /// <summary>
    /// Describle scriptable mission object
    /// </summary>
    [CreateAssetMenu (menuName = "Mission")]
    public class Mission : ScriptableObject {

        [SerializeField] GameObject missionPrefab; // Mission panel game object 
        [SerializeField] string missionLevelName; // Scene Name
        [SerializeField] bool completed; // is mission complted
        [SerializeField] string missionDescription; // description of mission
        [SerializeField] Image missionImage; // image of mission
        [SerializeField] Mission nextMission; // next mission after this

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
}
