using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [Header ("Shooting attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;


    [Header("Sounds")]
    [SerializeField] private AudioClip shootingSound;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleFlash;

    private Rigidbody2D bulletRb;
    private float attackElspasedTime = Mathf.Infinity;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackElspasedTime >= attackCooldown)
        {
            shoot();
            attackElspasedTime = 0f;
        }
        attackElspasedTime += Time.deltaTime;
    }

    public void addDamageToBullet(float damage, bool color)
    {
        this.bulletPrefab.GetComponent<BulletCollision>().setDamage(damage);

        if(color)
            this.bulletPrefab.GetComponent<SpriteRenderer>().color = Color.red;
        else
            this.bulletPrefab.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void shoot()
    {
        //play sound
        SoundManager.instance.playSound(shootingSound);

        //PlayEffect
        ParticleSystem particles = Instantiate(muzzleFlash, firePoint.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, 1f);

        //shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.up * speed;
    }
}
