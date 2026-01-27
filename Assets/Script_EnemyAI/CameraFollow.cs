using UnityEngine;

public class CameraFollow : MonoBehaviour
{
      [SerializeField] private Transform player;

    private Vector3 positionOffset;
    private float yRotationOffset;

    void Start()
    {
        // Store initial position offset
        positionOffset = transform.position - player.position;

        // Store only Y rotation offset
        yRotationOffset = transform.eulerAngles.y - player.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Follow position
        transform.position = player.position + positionOffset;

        // Rotate only around Y axis
        float targetYRotation = player.eulerAngles.y + yRotationOffset;
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            targetYRotation,
            0f
        );
    }
}
