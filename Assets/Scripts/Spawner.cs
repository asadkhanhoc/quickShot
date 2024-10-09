using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] hazards;

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public float minTimeBetweenSpawns;
    public float decrease;

    public GameObject player;

    private bool stopSpawning = false; // Add a flag to control spawning

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawning)
        {
            if (player != null)
            {
                if (timeBtwSpawns <= 0)
                {

                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    GameObject randomHazard = hazards[Random.Range(0, hazards.Length)];

                    Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);

                    if (startTimeBtwSpawns > minTimeBetweenSpawns)
                    {
                        startTimeBtwSpawns -= decrease;
                    }

                    timeBtwSpawns = startTimeBtwSpawns;

                }
                else
                {
                    timeBtwSpawns -= Time.deltaTime;
                }
            }
        }

    }

    // Function to stop spawning
    public void StopSpawning()
    {
        stopSpawning = true; // Set the flag to true to stop further spawning
    }

}
