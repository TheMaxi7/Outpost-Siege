using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    public static int waveNumber = 0;
    public static int waveType = 2;
    public static bool spawn = false;
    private float timeBetweenWaves = 15f;
    private float timer;
    private bool isSpawnPaused = false;
    private float gameSpeedMultiplier = 1f;
    private int enemyValue;
    public static int nextEnemyType;
    public static int enemiesInWave;
    public static int nextWaveEnemies;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject bigEnemyPrefab;
    private Spawner[] spawners;
    public static GameObject enemyToIstantiate;
    public static GameObject nextEnemyToIstantiate;

    private void Awake()
    {
        spawners = Object.FindObjectsOfType<Spawner>();
        nextWaveEnemies = CreateWave();
    }
    private void Start()
    {
       
        timer = 5f;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        Time.timeScale = 1f * gameSpeedMultiplier;
        if (!isSpawnPaused)
        {
            if (timer <= 0)
            {
                enemiesInWave = nextWaveEnemies;
                enemyToIstantiate = nextEnemyToIstantiate;
                waveNumber++;
                spawn = true;
                timer = timeBetweenWaves;
                nextWaveEnemies = CreateWave();
            }
            else
            {
                spawn = false;
            }
        }
        timer -= Time.deltaTime;

    }

    public void ToggleSpawn()
    {
        if (isSpawnPaused)
            isSpawnPaused = false;
        else
            isSpawnPaused = true;

    }

    public void ToggleMultiplier()
    {
        if (gameSpeedMultiplier >= 4)
            gameSpeedMultiplier = 1;
        else
            gameSpeedMultiplier *= 2;
    }


    public int CreateWave()
    {
        if (waveNumber <= 10)
            nextEnemyType = Random.Range(1, 3);
        else
            nextEnemyType = Random.Range(1, 4);

        switch (nextEnemyType)
        {
            case 1:
                enemyValue = basicEnemyPrefab.GetComponent<Enemy>().value;
                nextEnemyToIstantiate = basicEnemyPrefab;
                break;
            case 2:
                enemyValue = fastEnemyPrefab.GetComponent<Enemy>().value;
                nextEnemyToIstantiate = fastEnemyPrefab;
                break;
            case 3:
                enemyValue = bigEnemyPrefab.GetComponent<Enemy>().value;
                nextEnemyToIstantiate = bigEnemyPrefab;
                break;
        }
        return (UiManager.totalCoinsInGame / enemyValue) / spawners.Length;
    }



}


