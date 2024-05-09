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
                waveNumber++;
                spawn = true;
                timer = timeBetweenWaves;
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




}


