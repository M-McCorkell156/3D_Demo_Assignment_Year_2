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

    public event EventHandler OnWaveChange;
    private Enemy Enemy;


    // Start is called before the first frame update
    void Start()
    {
        waveNo = 0;
        WaveChange();

        //Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;

        //Debug.Log($"Start wave No:{waveNo} enemyNo: {enemyCount}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Check for remaining enemies
    public void WaveCheck()
    {
        if (enemyCount == 0)
        {
            OnWaveChange(this, EventArgs.Empty);
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

    //Method to call when enemy dies
    private void Enemy_OnEnemyDeath(object sender, EventArgs e)
    {
        Debug.Log("event on enemy death");
        enemyCount--;
    }
}
