using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWayPoints : MonoBehaviour
{
    [Header ("Attributes")]
    [SerializeField] private float detectRange = 4f;
    [SerializeField] private float movSpeed = 1f;

    [Header("Sounds")]
    [SerializeField] AudioClip shootingSound;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    public int currentWayPoint;
    private Transform player;
    
    private Rigidbody2D bulletRb;
    private EnemyAI enemyAi;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAi = GetComponent<EnemyAI>();

        if(SceneManager.GetActiveScene().name == "TanksLava")
        {
            GameObject waypointObject = GameObject.FindGameObjectWithTag("WayPoint");
            Transform[] waypointObjects = waypointObject.GetComponentsInChildren<Transform>();

            wayPoints = waypointObjects[1..]; //skip the first object because its the parent

            findClosestWaypoint();
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "TanksLava")
        {
            if (isPlayerInRange())
            {
                //attackPlayer();
                enemyAi.enabled = true;
                this.enabled = false;
            }
            else
            {
                moveToNextWaypoint();
            }
        }
    }


    private bool isPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRange, LayerMask.GetMask("Player"));
        return hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);  // Draw a red circle representing the detection range
    }


    private void moveToNextWaypoint()
    {
        if (wayPoints.Length == 0) return;

        Transform targetWayPoint = wayPoints[currentWayPoint];
        Vector3 direction = (targetWayPoint.position - transform.position).normalized;
        transform.position += direction * movSpeed * Time.deltaTime;

        //face the waypoint direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (Vector3.Distance(transform.position, targetWayPoint.position) < 0.3f)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }

    private void findClosestWaypoint()
    {
        float closestDistance = Mathf.Infinity;
        int closestIndex = 0;

        // Loop through all waypoints to find the closest one
        for (int i = 0; i < wayPoints.Length; i++)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, wayPoints[i].position);
            if (distanceToWaypoint < closestDistance)
            {
                closestDistance = distanceToWaypoint;
                closestIndex = i;
            }
        }

        // Set the closest waypoint as the current target
        currentWayPoint = closestIndex;
    }
}
