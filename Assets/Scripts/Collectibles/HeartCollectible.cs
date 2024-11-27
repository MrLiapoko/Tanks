using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [SerializeField] private float healthAdd;
    private Health playerHealth;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Play sound
            SoundManager.instance.playSound(pickupSound);

            //add health to the player
            playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.addHealth(healthAdd);
            Destroy(gameObject);

            PowerUpSpawner.currentPowerUps--; //decrease the amount of power ups that exists
        }
    }
}
