using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float cdWaveTimer = 5f;
    private float cd = 2f;
    public Transform[] spawnPoint;
    private int waveNumber = 1;
    public Transform target;
    private NavMeshAgent agent;
    private void Update()
    {
        if (cd <= 0)
        {
            StartCoroutine(SpawnWave());
            cd = cdWaveTimer;
        }

        cd -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        for (int i = 0;i < spawnPoint.Length;i++)
        {
            GameObject enemyToIstantiate = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            GameObject enemyInstance = Instantiate(enemyToIstantiate, spawnPoint[i].position, enemyToIstantiate.transform.rotation);
            agent = enemyInstance.GetComponent<NavMeshAgent>();
            agent.destination = target.position;
        }
        
    }
}


