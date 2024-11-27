using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    public void explode(Transform explodePos, ParticleSystem explosionEffect)
    {
        ParticleSystem particles = Instantiate(explosionEffect, explodePos.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, 1f); // Destroy after playing
    }
}
