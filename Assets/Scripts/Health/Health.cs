using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health attributes")]
    [SerializeField] private float startingHealth;
    [SerializeField] private Image healthBarFill;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;


    [Header("Effects")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem shieldEffect;
    [SerializeField] private SpriteRenderer[] dissolveMat;
    private float fadeTime = 1f;
    private bool isDissolving = false;
    private bool isShieldActive = false;


    public float currentHealth { get; private set; }
    private Animator anim;
    private Rigidbody2D rb;
    private ExplosionEffect exp;
    private ScoreManager scoreManager;
    private TankShooting tankShooting;
    private TankMovement tankMovement;
    private ScreenShake screenShake;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        exp = GetComponent<ExplosionEffect>();
        scoreManager = FindObjectOfType<ScoreManager>();
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    public void takeDamage(float damage)
    {
        if (isShieldActive) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            rb.velocity = Vector2.zero;

            destroyTank();

            //play death sound and particle effect
            SoundManager.instance.playSound(deathSound);
            exp.explode(transform, explosionEffect);

            //anim.SetTrigger("die");
        }
        else
        {
            SoundManager.instance.playSound(hurtSound);
        }
        updateHealthBar();
    }

    public void addHealth(float healthAdd)
    {
        currentHealth += healthAdd;
        if (currentHealth >= startingHealth)
            currentHealth = startingHealth;

        updateHealthBar();
    }

    private void Update()
    {
        //makding the dissovle shader effect and dissabling the tank object
        if(isDissolving)
        {

            fadeTime -= Time.deltaTime;

            if(fadeTime <= 0f)
            {
                fadeTime = 0f;
                isDissolving = false;
                gameObject.SetActive(false);
            }

            for (int i = 0; i < dissolveMat.Length; i++)
            {
                dissolveMat[i].material.SetFloat("_Fade", fadeTime);
            }
        }
    }

    private void updateHealthBar()
    {
        float healthPrecentage = currentHealth / startingHealth;
        healthBarFill.rectTransform.localScale = new Vector3(healthPrecentage, healthBarFill.rectTransform.localScale.y, healthBarFill.rectTransform.localScale.z);
    }

    public void destroyTank()
    {

        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            scoreManager.score += 10;
            scoreManager.animatePop();
        }
        else if(gameObject.tag == "Suicide")
        {
            Destroy(gameObject);
            scoreManager.score += 5;
            scoreManager.animatePop();
        }
        else if(gameObject.tag == "RocketLuncher")
        {
            Destroy(gameObject);
            scoreManager.score += 20;
            scoreManager.animatePop();
        }
        else if (gameObject.tag == "EnemyRanged")
        {
            Destroy(gameObject);
            scoreManager.score += 15;
            scoreManager.animatePop();
        }
        else
        {
            tankShooting = GetComponentInChildren<TankShooting>();
            tankShooting.addDamageToBullet(30, false);

            tankMovement = GetComponent<TankMovement>();
            tankMovement.setSpeed(0);

            //screen shake
            screenShake.triggerShake();

            isDissolving = true;
        }
    }


    public void setShield(bool status)
    {
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        Vector2 originalSize = new Vector2(1.784599f, 2.520773f);

        this.isShieldActive = status; // for damage taking in the "takeDamage" function 

        //set the shield effect and collider
        if (status)
        {
            ParticleSystem shieldEffectInstance = Instantiate(shieldEffect, transform.position, Quaternion.identity);
            shieldEffectInstance.transform.parent = transform; // Attach it to the player
            shieldEffectInstance.transform.localScale = new Vector3(0.44315f, 0.44315f, 0.44315f); //make hime the original size
            shieldEffectInstance.Play();
            Destroy(shieldEffectInstance, 10f);

            capsuleCollider.size = new Vector2(capsuleCollider.size.x * 2, capsuleCollider.size.y);
        }
        else
        {
            if(shieldEffect.isPlaying) shieldEffect.Stop();
            capsuleCollider.size = originalSize;
        }
    }
}


