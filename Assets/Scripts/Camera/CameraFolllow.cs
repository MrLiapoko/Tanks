using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

    private void LateUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPos;
    }
}
