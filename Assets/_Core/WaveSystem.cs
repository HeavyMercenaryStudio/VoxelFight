using CameraUI;
using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core { 

    public class WaveSystem : MonoBehaviour {

        [SerializeField] Text waveText; // wave number text
        [SerializeField] Text waveEnemies; // enemies in wave
        [SerializeField] Text nextWaveText; // text time to next wave

        [SerializeField] float timeBetweenWaves; // rest time between waves

        [SerializeField] List<Wave> waveList; // list of waves
        Wave currentWave; // 
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
            if (Time.time < nextWaveTime) //if its time to next wave...
             {
                var t = (int)(nextWaveTime - Time.time); // update text interface
                nextWaveText.text = ("NEXT WAVE : " + t);

                //storeSystem.ToogleStore (true);

                if (t == 0) { // if rest end
                   // storeSystem.ToogleStore (false);
                    nextWaveTime = 0; // starrt next wave
                    NextWave ();
                }
            }
        }

        public void DecreseEnemiesCount()
        {
            enemiesInWave--; // when enemy dead ..
            waveEnemies.text = "ENEMIES TO KILL : " + enemiesInWave; // update interface

            if (enemiesInWave == 0) {//if players kill all enemies
                nextWaveTime = Time.time + timeBetweenWaves;  //start next wave

                waveNumber++; // if its no more wave in this level
                if (waveNumber == waveList.Count)
                { 
                    Enemy.onEnemyDeath -= DecreseEnemiesCount; // remove listener
                    gameGui.Victory (); // update interface 
                }
            }
        }
        void StartWave()
        {
            waveText.text = "WAVE : " + (waveNumber + 1).ToString(); // update text on UI

            for (int i = 0; i < currentWave.enemiesAmount.Length; i++) // count all enemies in wave
                enemiesInWave += currentWave.enemiesAmount[i];

            waveEnemies.text = "ENEMIES TO KILL : " + enemiesInWave; // update UI

            StartCoroutine (SpawnEnemy (0, 0)); // spawn enemies 
        }

        public void NextWave()
        {
            if (waveNumber == waveList.Count) return; //if no more waves in level do nothing

            currentWave = waveList[waveNumber]; // else start next wave
            StartWave ();
        }
        IEnumerator SpawnEnemy(int numberOfEnemyTypes, int numberOfEnemies)
        {
            //spawn enemies for each type in array "enemies" every Xs time 
            while (numberOfEnemyTypes < currentWave.enemies.Length) 
            {
                yield return new WaitForSeconds (currentWave.spawnDelayTime);

                int n = UnityEngine.Random.Range (0, 360); //calaculate position at circle around players
                var rad = n * Mathf.PI / 180f;

                var x = currentWave.R * Mathf.Cos (rad);
                var y = currentWave.R * Mathf.Sin (rad);
                Vector3 pos = new Vector3 (x, 0, y);

                //instantiate new enemy at this position
                Instantiate (currentWave.enemies[numberOfEnemyTypes], pos, Quaternion.identity);

                //if spawned all enemies of current type
                if (numberOfEnemies >= currentWave.enemiesAmount[numberOfEnemyTypes] - 1)
                {
                    numberOfEnemies = 0; // spawn new enemies
                    numberOfEnemyTypes++;
                }
                else // else continue spawning
                    numberOfEnemies++;
            }
        }
   
    }

}