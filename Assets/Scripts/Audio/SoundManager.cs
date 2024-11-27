using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource soundSource;

    private void Awake()
    {
        instance = this;
        soundSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
}
