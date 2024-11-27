using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private ParticleSystem trail;
    [SerializeField] private AudioClip spawnClip;

    private void Start()
    {
        movSpeed = 5f;

        SoundManager.instance.playSound(spawnClip);
    }
    private void Update()
    {
        float moveDirection = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;
        float rotateDirection = -Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        transform.Translate(Vector3.up * moveDirection);
        transform.Rotate(Vector3.forward * rotateDirection);
    }

    public void setSpeed(float newSpeed)
    {
        movSpeed = newSpeed;
    }

    public void startTrail(bool start) //used in other class
    {
        if(start)
            trail.Play();
        else
            trail.Stop();
    }
}
