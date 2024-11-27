using UnityEngine;

public class HealtBarFollow : MonoBehaviour
{
    [SerializeField] private Transform tankPos;

    private Vector3 offest = new Vector3(0, -0.75f, 0f);

    private void Update()
    {
        transform.position = tankPos.position + offest;
        transform.rotation = Quaternion.identity;
    }
}
