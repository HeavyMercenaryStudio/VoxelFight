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

    [SerializeField] List<Wave> waveList;
    Wave currentWave;
    int waveNumber;

    //StoreSystem storeSystem;
    int enemiesInWave;
    float nextWaveTime;
    GameGui gameGui;

    public void Start()
    {
        // storeSystem = FindObjectOfType<StoreSystem> ();
        //storeSystem.ToogleStore (false);
        gameGui = GameObject.FindObjectOfType<GameGui> ();

        Enemy.onEnemyDeath += DecreseEnemiesCount;
    }
    public void Update()
    {
        if (Time.time < nextWaveTime)
        {
            var t = (int)(nextWaveTime - Time.time);
            nextWaveText.text = ("NEXT WAVE : " + t);

            //storeSystem.ToogleStore (true);

            if (t == 0) {
               // storeSystem.ToogleStore (false);
                nextWaveTime = 0;
                NextWave ();
            }
        }
    }

    public void DecreseEnemiesCount()
    {
        enemiesInWave--;
        waveEnemies.text = "ENEMIES TO KILL : " + enemiesInWave;

        if (enemiesInWave == 0) {
            nextWaveTime = Time.time + timeBetweenWaves;

            waveNumber++;
            if (waveNumber == waveList.Count)
            {
                Enemy.onEnemyDeath -= DecreseEnemiesCount;
                gameGui.Victory ();
            }
        }
    }
    void StartWave()
    {
        waveText.text = "WAVE : " + (waveNumber + 1).ToString();

        for (int i = 0; i < currentWave.enemiesAmount.Length; i++)
            enemiesInWave += currentWave.enemiesAmount[i];

        waveEnemies.text = "ENEMIES TO KILL : " + enemiesInWave;

        StartCoroutine (SpawnEnemy (0, 0));
    }
    public void NextWave()
    {
        if (waveNumber == waveList.Count) return;

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
