using System.Collections;
using UnityEngine;

public class DamageBoost : MonoBehaviour
{
    [Header ("Attributes")]
    [SerializeField] private float damageBoost; 
    [SerializeField] private float duration;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;

    private TankShooting tank;
    private SpriteRenderer collectibleSprite;
    private BoxCollider2D boxCollider;
    private float originalDamage = 30f;

    private void Awake()
    {
        collectibleSprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Play sound
            SoundManager.instance.playSound(pickupSound);

            tank = collision.gameObject.GetComponentInChildren<TankShooting>();
            StartCoroutine(damageBoostI());

            collectibleSprite.enabled = false;
            boxCollider.enabled = false;
            PowerUpSpawner.currentPowerUps--; //decrease the amount of power ups that exists
        }
    }

    private IEnumerator damageBoostI()
    {
        tank.addDamageToBullet(damageBoost, true);
        yield return new WaitForSeconds(duration);
        tank.addDamageToBullet(originalDamage, false);
    }
}
