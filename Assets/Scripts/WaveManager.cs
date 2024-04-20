using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    public static int waveNumber = 0;
    public static int waveType = 0;
    public static bool spawn = false;
    private float timeBetweenWaves = 15f;
    private float timer;

    private void Start()
    {
        timer = 5f;

    }
    private void Update()
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

        timer -= Time.deltaTime;

    }


}


