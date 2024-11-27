using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public CameraFolllow cameraFollow;

    private float shakeDuration = 10f;
    private float shakeMagnitude = 0.7f;

    private TankMovement tankMovement;

    private void OnEnable()
    {
        tankMovement = FindAnyObjectByType<TankMovement>();
        cameraFollow = GetComponent<CameraFolllow>();
    }

    public void triggerShake()
    {

        StartCoroutine(shake());
    }

    private IEnumerator shake()
    {
        float elapsed = 0f;

        while(elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(tankMovement.transform.position.x + xOffset, tankMovement.transform.position.y + yOffset, tankMovement.transform.position.z);
            elapsed += Time.deltaTime;

            shakeMagnitude = Mathf.Lerp(shakeMagnitude, 0f, elapsed / shakeDuration);
            yield return null;
        }

        transform.localPosition = tankMovement.transform.position;
    }
}
