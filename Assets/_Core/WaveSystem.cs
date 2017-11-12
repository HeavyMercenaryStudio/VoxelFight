using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour {

    [SerializeField] Text waveText;
    [SerializeField] Text waveEnemies;
    [SerializeField] Text nextWaveText;

    [SerializeField] float timeBetweenWaves;

    public List<Wave> waveList;
    Wave currentWave;
    int waveNumber;

    int enemiesInWave;
    float nextWaveTime;

    public void Start()
    {
        Enemy.onEnemyDeath += DecreseEnemiesCount;

        currentWave = waveList[waveNumber];

        StartWave ();
    }

    public void Update()
    {
        if (Time.time < nextWaveTime)
        {
            var t = (int)(nextWaveTime - Time.time);
            nextWaveText.text = ("Next Wave : " + t);

            if (t == 0) {
                nextWaveTime = 0;
                NextWave ();
            }
        }
    }

    public void DecreseEnemiesCount()
    {
        enemiesInWave--;
        waveEnemies.text = "Enemies to kill : " + enemiesInWave;

        if (enemiesInWave == 0) {
            nextWaveTime = Time.time + timeBetweenWaves;
        }
    }

    void StartWave()
    {
        waveText.text = "Wave : " + waveNumber + 1;

        for (int i = 0; i < currentWave.enemiesAmount.Length; i++)
            enemiesInWave += currentWave.enemiesAmount[i];

        waveEnemies.text = "Enemies to kill : " + enemiesInWave;

        StartCoroutine (SpawnEnemy (0, 0));
    }

    void NextWave()
    {
        waveNumber++;
        currentWave = waveList[waveNumber];
        StartWave ();
    }

    IEnumerator SpawnEnemy(int numberOfEnemyTypes, int numberOfEnemies)
    {
        while (numberOfEnemyTypes < currentWave.enemies.Length)
        {

            yield return new WaitForSeconds (currentWave.spawnDelayTime);

            int n = UnityEngine.Random.Range (0, 360);
            var rad = n * Mathf.PI / 180f;

            var x = currentWave.R * Mathf.Cos (rad);
            var y = currentWave.R * Mathf.Sin (rad);

            Vector3 pos = new Vector3 (x, 0, y);

            GameObject enemy = Instantiate (currentWave.enemies[numberOfEnemyTypes], pos, Quaternion.identity) as GameObject;

            if (numberOfEnemies >= currentWave.enemiesAmount[numberOfEnemyTypes] - 1)
            {
                numberOfEnemies = 0;
                numberOfEnemyTypes++;
            }
            else
                numberOfEnemies++;
        }
    }
   
}
