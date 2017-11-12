using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Wave")]
public class Wave : ScriptableObject {

    public GameObject[] enemies;
    public int[] enemiesAmount;
    public float spawnDelayTime;

    public int R = 100;

}
