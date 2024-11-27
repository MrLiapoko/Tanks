using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [Header ("Bullet attributes")]
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;
    private float elapsedBulletLifeTime;

    [Header("Effects")]
    [SerializeField] private ParticleSystem explosionEffect;

    private Animator anim;
    private Rigidbody2D rb;
    private Health health;
    private Vector3 startingPos;
    private ExplosionEffect explosonEffect;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        explosonEffect = GetComponent<ExplosionEffect>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if(elapsedBulletLifeTime >= lifeTime)
        {
            if(gameObject.tag == "CannonBullet")
            {
                //for cannon bullet if it stop its life it will explode
                //anim.SetTrigger("explode");
                rb.velocity = new Vector2(0f, 0f);
                destroyBullet();
                explosonEffect.explode(transform, explosionEffect);
            }
            else
            {
                destroyBullet();
                elapsedBulletLifeTime = 0;
            }
        }
        elapsedBulletLifeTime += Time.deltaTime;

        if(gameObject.tag == "CannonBullet")
        {
            float scale = Mathf.Lerp(1f, 0.5f, Vector3.Distance(startingPos, transform.position) / 15f);
            transform.localScale = new Vector3(scale, scale, 1f);
        }
        else if(gameObject.tag == "Rocket")
        {
            float scale = Mathf.Lerp(1.5f, 1f, Vector3.Distance(startingPos, transform.position) / 15f);
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //set explode animation
        //anim.SetTrigger("explode");
        rb.velocity = new Vector2(0f, 0f);
        explosonEffect.explode(transform, explosionEffect);
        destroyBullet();

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyRanged" || collision.gameObject.tag == "RocketLuncher"
            || collision.gameObject.tag == "Suicide")
        {
            health = collision.gameObject.GetComponent<Health>();
            health.takeDamage(damage);
        }
    }


    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void addDamage(float damage)
    {
        this.damage += damage;
    }

    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}
