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
    private float enemyHP;
    public static int nextEnemyType;
    public static int enemiesInWave;
    private float enemies;
    public static int nextWaveEnemies;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject bigEnemyPrefab;
    [SerializeField] private GameObject bossEnemyPrefab;
    private Spawner[] spawners;
    public static GameObject enemyToIstantiate;
    public static GameObject nextEnemyToIstantiate;

    private void Awake()
    {
        spawners = Object.FindObjectsOfType<Spawner>();
        nextWaveEnemies = (int)Mathf.Round(CreateWave());
    }
    private void Start()
    {
        basicEnemyPrefab.GetComponent<Enemy>().health = 250f;
        fastEnemyPrefab.GetComponent<Enemy>().health = 180f;
        bigEnemyPrefab.GetComponent<Enemy>().health = 450f;
        timer = 5f;
        Time.timeScale = 1f;
        waveNumber = 25;
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
                if (waveNumber <= 29)
                    nextWaveEnemies = (int)Mathf.Round(CreateWave());
                else if (waveNumber == 30)
                {
                    nextWaveEnemies = 1;
                    nextEnemyToIstantiate = bossEnemyPrefab;
                }


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


    public float CreateWave()
    {
        if (waveNumber <= 10)
            nextEnemyType = Random.Range(1, 3);
        else
            nextEnemyType = Random.Range(1, 4);

        switch (nextEnemyType)
        {
            case 1:
                enemyHP = basicEnemyPrefab.GetComponent<Enemy>().health;
                nextEnemyToIstantiate = basicEnemyPrefab;
                break;
            case 2:
                enemyHP = fastEnemyPrefab.GetComponent<Enemy>().health;
                nextEnemyToIstantiate = fastEnemyPrefab;
                break;
            case 3:
                enemyHP = bigEnemyPrefab.GetComponent<Enemy>().health;
                nextEnemyToIstantiate = bigEnemyPrefab;
                break;

        }

        enemies = ((((UiManager.totalMoneyConvertedInDps + UiManager.coinsCount) / 2) * 20) / enemyHP) / spawners.Length;

        if (enemies > 10)
        {
            basicEnemyPrefab.GetComponent<Enemy>().health *= 1.5f;
            fastEnemyPrefab.GetComponent<Enemy>().health *= 1.5f;
            bigEnemyPrefab.GetComponent<Enemy>().health *= 1.5f;
        }
        if (UiManager.totalMoneyConvertedInDps < 50)
            return (50 * 10 / enemyHP) / spawners.Length;
        return enemies;
    }



}


