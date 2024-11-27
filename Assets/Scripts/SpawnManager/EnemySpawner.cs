using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemeisPrefabs;

    [Header("Effects")]
    [SerializeField] private ParticleSystem teleportEffect;
    
    private readonly int FIRST_WAVE_DIFFICULTY = 2;
    private readonly int SECOND_WAVE_DIFFICULTY = 3;
    private readonly int THIRD_WAVE_DIFFICULTY = 4;

    /*
    order if the enemies int the prefab
    1. Enemy
    2. Suicide tank
    3. Enemy ranged
    4. rocket luncher
     */
    private int currentEnemyDifficulty = 2; //order in the array of enemies prefab
    private int enemiesPerWave = 2; //starting value
    private int waveNumber = 1; //starting value


    //delay between wave
    private float delayBetweenWaves = 1f;
    private bool isSpawningWave = false;

    public List<GameObject> activeEnemies = new List<GameObject>();


    private void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null); //lambda expresion for removing destroyed enemies

        if (activeEnemies.Count <= 0 && !isSpawningWave)
        {
            StartCoroutine(SpawnNextWaveWithDelay());
        }
    }

    // Coroutine to spawn the next wave after a delay
    private IEnumerator SpawnNextWaveWithDelay()
    {
        isSpawningWave = true;

        // Wait for the specified delay time before spawning
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            ParticleSystem particlePrefab = Instantiate(teleportEffect, spawnPoints[i].position, Quaternion.identity);
            particlePrefab.Play();
            Destroy(particlePrefab.gameObject, 2f);
        }
        yield return new WaitForSeconds(delayBetweenWaves);

        // Spawn enemies for the next wave
        for (int i = 0; i < enemiesPerWave; i++)
        {
            spawn();
        }

        // Increment wave number and increase difficulty
        waveNumber++;
        enemiesPerWave = Mathf.FloorToInt(waveNumber * 1.2f);

        isSpawningWave = false;
    }

    private void spawn()
    {
        if (waveNumber < 4)
        {
            currentEnemyDifficulty = FIRST_WAVE_DIFFICULTY; //include enemy and suicide

        }
        else if (waveNumber > 4 && waveNumber < 7)
        {
            currentEnemyDifficulty = SECOND_WAVE_DIFFICULTY; //include enemy ranged 
        }
        else if (waveNumber > 7)
        {
            currentEnemyDifficulty = THIRD_WAVE_DIFFICULTY; //include rocket luncher
        }

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, currentEnemyDifficulty);

        GameObject enemyClone = Instantiate(enemeisPrefabs[enemyIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        activeEnemies.Add(enemyClone);
    }
}
