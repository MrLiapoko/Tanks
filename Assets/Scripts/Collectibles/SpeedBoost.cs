using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float newSpeed;
    [SerializeField] private float effectDuration;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;

    private TankMovement tank;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float originalSpeed = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Play sound
            SoundManager.instance.playSound(pickupSound);

            tank = collision.gameObject.GetComponent<TankMovement>();
            StartCoroutine(speedBoostCoroutine());
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

            PowerUpSpawner.currentPowerUps--; //decrease the amount of power ups that exists
        }
    }

    private IEnumerator speedBoostCoroutine()
    {
        tank.setSpeed(newSpeed);
        tank.startTrail(true);

        yield return new WaitForSeconds(effectDuration);

        tank.setSpeed(originalSpeed);
        tank.startTrail(false);
        Destroy(gameObject);
    }
}
