using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaveHandler : MonoBehaviour
{

    private float waveNo;
    public float enemyCount;

    public List<Transform> spawnPos;
    public GameObject enemyPrefab;

    public Enemy Enemy;


    // Start is called before the first frame update
    void Start()
    {
        waveNo = 0;
        WaveChange();

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;


        //Debug.Log($"Start wave No:{waveNo} enemyNo: {enemyCount}");
    }

    // Update is called once per frame
    void Update()
    {
        WaveCheck();
    }

    //Check for remaining enemies
    public void WaveCheck()
    {
        //Debug.Log("WaveCheck");
        if (enemyCount == 0)
        {
            //Debug.Log("no ene, next wave");
            //OnWaveChange(this, EventArgs.Empty);
            WaveChange();
        }
    }

    //increase wave and calculate no of enemies
    private void WaveChange()
    {
        waveNo++;
        enemyCount = waveNo * (waveNo + 2) + 2;
        //Debug.Log($"eneCount{enemyCount}");

        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPos != null)
            {
                int ranSpawnPos = UnityEngine.Random.Range(1, 4);
                //Debug.Log($"Random no: {i} spawn: {ranSpawnPos}");
                Instantiate(enemyPrefab, spawnPos[ranSpawnPos].position, Quaternion.identity);
            }
        }

    }

    //Event method when enemy dies
    private void Enemy_OnEnemyDeath()
    {
        //Debug.Log("event on enemy death");
        enemyCount--;
        //Debug.Log($"ene count now : {enemyCount}");

    }
}
