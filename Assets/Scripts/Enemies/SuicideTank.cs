using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuicideTank : MonoBehaviour
{
    [Header("Enemy tank attributes")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Obsacles attributes")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float avoidDistance = 2f;

    [Header ("iFrames")]
    [SerializeField] private SpriteRenderer towerRendrer;
    private float activationDelay = 0.5f;

    private float fireCooldown = 0f;
    private Rigidbody2D bulletRb;
    private Health enemyTankHealth;

    private Transform player;
    private Health playerHealth;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TanksLava") this.enabled = false;

        enemyTankHealth = GetComponent<Health>();
        StartCoroutine(flashingTank()); //flashing effect

        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        if(temp != null)
        {
            player = temp.transform;
            playerHealth = temp.GetComponent<Health>();
        }
    }

    void Update()
    {

        if (playerHealth.currentHealth > 0)
        {
            Vector2 direction = player.position - transform.position;
            bool isObstacleBlocking = Physics2D.Raycast(transform.position, direction, direction.magnitude, obstacleLayer);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (isObstacleBlocking)
            {
                avoidObstacle(direction);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            }
            
            fireCooldown -= Time.deltaTime;
        }
    }

    private void avoidObstacle(Vector2 direction)
    {
        // Cast rays to the left and right of the current direction
        Vector2 leftRayDirection = Quaternion.Euler(0, 0, 45) * direction.normalized;
        Vector2 rightRayDirection = Quaternion.Euler(0, 0, -45) * direction.normalized;

        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position, leftRayDirection, avoidDistance, obstacleLayer);
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position, rightRayDirection, avoidDistance, obstacleLayer);

        // Draw rays for debugging
        Debug.DrawRay(transform.position, leftRayDirection * avoidDistance, Color.red);
        Debug.DrawRay(transform.position, rightRayDirection * avoidDistance, Color.red);


        // Determine which direction is more open
        if (leftRayHit.collider == null && rightRayHit.collider == null)
        {
            // No obstacles detected on either side, move straight
            MoveInDirection(direction.normalized);
        }
        else if (leftRayHit.collider == null)
        {
            // No obstacle on the left, steer left
            MoveInDirection(leftRayDirection);
        }
        else if (rightRayHit.collider == null)
        {
            // No obstacle on the right, steer right
            MoveInDirection(rightRayDirection);
        }
        else
        {
            // Obstacles on both sides, determine which side has more space
            if (leftRayHit.distance > rightRayHit.distance)
            {
                // More space on the left, steer left
                MoveInDirection(leftRayDirection);
            }
            else
            {
                // More space on the right, steer right
                MoveInDirection(rightRayDirection);
            }
        }
    }


    void MoveInDirection(Vector2 direction)
    {
        // Move in the calculated direction
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.takeDamage(100f);
            enemyTankHealth.takeDamage(20f);
        }
    }

    private IEnumerator flashingTank()
    {
        while(true)
        {
            towerRendrer.color = Color.red;
            yield return new WaitForSeconds(activationDelay);

            towerRendrer.color = Color.white;
            yield return new WaitForSeconds(activationDelay);
        }
    }
}
