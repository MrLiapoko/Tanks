using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(rotZ - 90, Vector3.forward);

        transform.rotation = rot;
    }
}
