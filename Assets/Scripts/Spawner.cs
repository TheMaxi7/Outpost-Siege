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

    private Transform spawnLocation;
    private Transform randomSpawnLocation;
    //Something from spawnmanager to decide what to spawn
    void Start()
    {
        spawnLocation = GetComponent<Transform>();
        //spawnLocation.position -= new Vector3(0f, 0.5f, 0f);
        randomSpawnLocation = spawnLocation;
        
    }

    void Update()
    {
        if ((WaveManager.spawn == true) && (WaveManager.waveNumber <=30))
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < WaveManager.enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            
        }
    }

    void SpawnEnemy()
    {
        randomSpawnLocation.transform.position = new Vector3 (Random.Range(spawnLocation.transform.position.x - 0.25f, spawnLocation.transform.position.x + 0.25f), spawnLocation.transform.position.y, Random.Range(spawnLocation.transform.position.z - 0.25f, spawnLocation.transform.position.z + 0.25f));
        GameObject enemyInstance = Instantiate(WaveManager.enemyToIstantiate, randomSpawnLocation.position, WaveManager.enemyToIstantiate.transform.rotation);
        enemyInstance.GetComponent<Enemy>().roadToExit = waypoints;
    }
    
}
