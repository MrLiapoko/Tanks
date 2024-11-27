using System.Collections;
using UnityEngine;

public class TempShield : MonoBehaviour
{
    [SerializeField] private float sheildTime;
    private Health health;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            //Play sound
            SoundManager.instance.playSound(pickupSound);

            health = collision.collider.GetComponent<Health>();
            StartCoroutine(setShield());
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

            PowerUpSpawner.currentPowerUps--; //decrease the amount of power ups that exists
        }
    }

    private IEnumerator setShield()
    {
        health.setShield(true);
        yield return new WaitForSeconds(sheildTime);
        health.setShield(false);
        Destroy(gameObject);
    }
}
