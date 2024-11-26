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

    public delegate void EventHandler();
    public static event EventHandler OnWaveChange;

    private Enemy Enemy;

    private AudioEventSender_BGM waveTune;



    // Start is called before the first frame update
    void Start()
    {
        waveNo = 0;
        WaveChange();

        Enemy.OnEnemyDeath += OnEnemyDeath;

        waveTune = GetComponent<AudioEventSender_BGM>();


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
            WaveChange();
        }
    }

    //increase wave and calculate no of enemies
    private void WaveChange()
    {

        //waveTune.Play();
        //Debug.Log("WaveChange");
        if (OnWaveChange != null)
        {
            //Debug.Log("Call Wave Change event");
            OnWaveChange();
        }
        waveNo++;
        enemyCount = waveNo * (waveNo + 2) + 2;

        Invoke("SpawnDelay", 8.0f);

    }


    private void SpawnDelay()
    {
        float spawnTime = 2.0f;
        for (int i = 0; i < enemyCount; i++)
        {
            //Debug.Log($"Spawn for {i} : {spawnTime}");
            if (spawnPos != null)
            {
                Invoke("Spawn", spawnTime);
                spawnTime++;
            }
        }
    }

    private void Spawn()
    {
        int ranSpawnPos = UnityEngine.Random.Range(0, 4);
        float ranSpawnDir = UnityEngine.Random.rotation.y;
        Quaternion quaternion = Quaternion.identity;
        quaternion.y = ranSpawnDir;

        //Debug.Log($"Random no: {i} spawn: {ranSpawnPos}");
        //Debug.Log($"Random dir {ranSpawnDir}");

        Instantiate(enemyPrefab, spawnPos[ranSpawnPos].position, quaternion);
    }


    //Event method when enemy dies
    private void OnEnemyDeath()
    {
        //Debug.Log("event on enemy death");
        enemyCount--;
        //Debug.Log($"ene count now : {enemyCount}");

    }
}
