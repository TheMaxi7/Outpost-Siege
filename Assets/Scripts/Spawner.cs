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
    private GameObject enemyToIstantiate;

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
        for (int i = 0; i < WaveManager.waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        
    }

    void SpawnEnemy()
    {
        switch (WaveManager.waveType)
        {
            case 0:
                enemyToIstantiate = basicEnemyPrefab;
                break;
            case 1:
                enemyToIstantiate = fastEnemyPrefab;
                break;
            case 2:
                enemyToIstantiate = bigEnemyPrefab;
                break;
        }
        GameObject enemyInstance = Instantiate(enemyToIstantiate, spawnLocation.position, enemyToIstantiate.transform.rotation);
        enemyInstance.GetComponent<Enemy>().roadToExit = waypoints;
    }
}
