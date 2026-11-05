using UnityEngine;

public class CameraFollowTouch : MonoBehaviour
{
    public Transform target;      // The car or player to follow
    public float distance = 5f;   // How far camera stays from the target
    public float height = 2f;     // Height above target
    public float rotationSpeed = 0.3f;  // Touch rotation sensitivity

    private float rotationX = 0f;
    private float rotationY = 10f; // Initial height angle

    void LateUpdate()
    {
        if (target == null) return;

        // Touch input
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationX += touch.deltaPosition.x * rotationSpeed;
                rotationY -= touch.deltaPosition.y * rotationSpeed;
                rotationY = Mathf.Clamp(rotationY, 5f, 60f); // Limit vertical rotation
            }
        }

        // Convert angles to position
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
