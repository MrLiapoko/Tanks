using UnityEngine;

public class rocketsParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticle;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        fireParticle.Play();
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Rocket_Explode"))
        {
            fireParticle.Stop();
        }
    }
}
