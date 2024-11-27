using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header ("Spawn attributes")]
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private Vector2 minArenaBounds;
    [SerializeField] private Vector2 maxArenaBounds;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxPowerUps;

    [Header ("Collision checks")]
    [SerializeField] private LayerMask obstcaleLayer;
    [SerializeField] private float radiusCheck;


    private bool destroyed;
    public static int currentPowerUps = 0;
    public int currentPowerupsDebug = currentPowerUps;

    private void Awake()
    {
        currentPowerUps = 0;
        StartCoroutine(spawnPowerUps());
    }


    private void Update()
    {
        currentPowerupsDebug = currentPowerUps;
    }

    private IEnumerator spawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            spawn();
        }
    }

    private void spawn()
    {
        if (currentPowerUps >= maxPowerUps) return;

        float randX = Random.Range(minArenaBounds.x, maxArenaBounds.x);
        float randY = Random.Range(minArenaBounds.y, maxArenaBounds.y);
        Vector2 spawnPos = new Vector2(randX, randY);
        
        if(!isBlocked(spawnPos))
        {
            GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
            Instantiate(powerUp, spawnPos, Quaternion.identity);

            currentPowerUps++;
        }
    }


    private bool isBlocked(Vector2 pos)
    {
        Collider2D posCheck = Physics2D.OverlapCircle(pos, radiusCheck, obstcaleLayer);
        return posCheck != null;
    }
}
