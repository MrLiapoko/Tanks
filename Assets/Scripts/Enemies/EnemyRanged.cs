using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [Header("Enemy tank attributes")]
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float shootingRange;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;

    [Header("Sound")]
    [SerializeField] private AudioClip rangedBulletSound; 
    [SerializeField] private AudioClip rocketSound;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleFlash;


    private float fireCooldown = 1f;
    private Rigidbody2D bulletRb;

    private Transform player;
    private Health playerHealth;

    private void Awake()
    {
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
            //get the rotation the player using quaternions
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


            if (Vector2.Distance(transform.position, player.position) <= shootingRange)
            {
                if (fireCooldown <= 0f)
                {
                    Shoot();
                    fireCooldown = 1f / fireRate;
                }
            }

            fireCooldown -= Time.deltaTime;
        }

    }

    private void Shoot()
    {
        //shoot bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.up * shootingSpeed;

        //Play muzzle flash
        ParticleSystem particles = Instantiate(muzzleFlash, firePoint.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, 1f);


        //Shooting Sound
        if (bullet.gameObject.tag == "Rocket")
            SoundManager.instance.playSound(rocketSound);
        else
            SoundManager.instance.playSound(rangedBulletSound);

    }
}
