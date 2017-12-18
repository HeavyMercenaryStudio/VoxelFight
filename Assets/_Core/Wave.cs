using UnityEngine;

namespace Core { 

    //IMPORTATNT ! ! ! 
    //Enemies array must be same lenght as enemiesAmount array

    /// <summary>
    /// Describes wave's.
    /// </summary>
    [CreateAssetMenu (menuName = "Wave")]
    public class Wave : ScriptableObject {

        public GameObject[] enemies; // enemies prefabs  
        public int[] enemiesAmount; // enemies amouse
        public float spawnDelayTime; // time between spawns

        public int R = 100; // how far spawn enemies

    }
}