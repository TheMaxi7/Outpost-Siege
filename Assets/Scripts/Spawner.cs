using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject bigEnemyPrefab;
    private Spawner[] spawners;
    private GameObject enemyToIstantiate;
    private int enemyValue;
    private int enemyType;

    private Transform spawnLocation;
    //Something from spawnmanager to decide what to spawn
    void Start()
    {
        spawnLocation = GetComponent<Transform>();
        //spawnLocation.position -= new Vector3(0f, 0.5f, 0f);
    }

    void Update()
    {
        if (WaveManager.spawn == true)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < WaveManager.enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyInstance = Instantiate(WaveManager.enemyToIstantiate, spawnLocation.position, WaveManager.enemyToIstantiate.transform.rotation);
        enemyInstance.GetComponent<Enemy>().roadToExit = waypoints;
    }

}
