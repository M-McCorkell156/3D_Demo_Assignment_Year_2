using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaveHandler : MonoBehaviour
{

    private float waveNo;
    public float enemyCount;
    public float killCount;

    private float spawnTime;
    [SerializeField] private float spawnBetween;

    [SerializeField] private float introDelay;

    [SerializeField] private List<Transform> activeSpawnPos;
    [SerializeField] private List<Transform> easySpawns;
    [SerializeField] private List<Transform> mediSpawns;
    [SerializeField] private List<Transform> hardSpawns;



    public GameObject enemyPrefab;

    public delegate void EventHandler();
    public static event EventHandler OnWaveChange;

    private Enemy Enemy;

    private AudioEventSender_BGM waveTune;



    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(activeSpawnPos.Count);

        waveNo = 0.0f;
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

    //increase wave + calculate no of enemies
    private void WaveChange()
    {
        if (OnWaveChange != null)
        {
            //Debug.Log("Call Wave Change event");
            OnWaveChange();
        }

        waveNo++;

        if (spawnBetween > 1)
        {
            spawnBetween -= 0.3f;
        }


        enemyCount = MathF.Round(waveNo * 3.5f);
        //Debug.Log(enemyCount);
        if (enemyCount > 40)
        {
            enemyCount = 40;
        }
        //Debug.Log(waveNo);


        //Sets spawn areas depending on current round  
        activeSpawnPos.Clear();
        switch (waveNo)
        {
            case > 0 and <= 3:
                //Debug.Log("Easy waves");
                activeSpawnPos.AddRange(easySpawns);
                break;
            case > 3 and <= 6:
                //Debug.Log("Medium waves");
                activeSpawnPos.AddRange(mediSpawns);
                break;
            case > 6:
                //Debug.Log("Hard waves");
                activeSpawnPos.AddRange(hardSpawns);
                break;
        }

        Debug.Log($"WaveHanlder Display: \n Wave No : {waveNo} \n No of Enemies spawning : {enemyCount} \n No of avaible spawn areas : {activeSpawnPos.Count} \n Time between each Enemy spawn : {spawnBetween} \n Wave rest time : {introDelay}");

        Invoke("SpawnDelay", introDelay);
    }

    public float WaveGetter()
    {
        //Debug.Log(waveNo);
        return waveNo;
    }

    public float KillCountGetter()
    {
        return killCount;
    }


    private void SpawnDelay()
    {
        spawnTime = 0;
        for (int i = 0; i < enemyCount; i++)
        {
            //Debug.Log($"Spawn for {i} : {spawnTime}");
            if (activeSpawnPos != null)
            {
                Invoke("Spawn", spawnTime);
                spawnTime += spawnBetween;
            }
        }
    }

    private void Spawn()
    {
        int ranSpawnPos = UnityEngine.Random.Range(0, activeSpawnPos.Count);
        float ranSpawnDir = UnityEngine.Random.rotation.y;
        Quaternion quaternion = Quaternion.identity;
        quaternion.y = ranSpawnDir;

        //Debug.Log($"Random no: {i} spawn: {ranSpawnPos}");
        //Debug.Log($"Random dir {ranSpawnDir}");

        Instantiate(enemyPrefab, activeSpawnPos[ranSpawnPos].position, quaternion);
    }


    //Event method when enemy dies
    private void OnEnemyDeath()
    {
        //Debug.Log("event on enemy death");
        killCount++;
        enemyCount--;
        //Debug.Log($"ene count now : {enemyCount}");

    }
}
