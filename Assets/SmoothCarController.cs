using UnityEngine;

public class SmoothCarController : MonoBehaviour
{
    public float maxSpeed = 6f;         // Max forward/backward speed
    public float acceleration = 2f;     // How fast to accelerate
    public float deceleration = 3f;     // How fast to slow down
    public float rotationSpeed = 80f;   // Turning speed (degrees per second)

    private float currentSpeed = 0f;
    private float targetSpeed = 0f;
    private float currentTurn = 0f;
    private float targetTurn = 0f;

    private bool moveForward, moveBackward, turnLeft, turnRight;

    void Update()
    {
        // --- Handle target speed ---
        if (moveForward) targetSpeed = maxSpeed;
        else if (moveBackward) targetSpeed = -maxSpeed * 0.6f; // slower reverse
        else targetSpeed = 0f;

        // Smooth acceleration / deceleration
        if (currentSpeed < targetSpeed)
            currentSpeed += acceleration * Time.deltaTime;
        else if (currentSpeed > targetSpeed)
            currentSpeed -= deceleration * Time.deltaTime;

        // --- Handle rotation ---
        if (turnLeft) targetTurn = -rotationSpeed;
        else if (turnRight) targetTurn = rotationSpeed;
        else targetTurn = 0f;

        // Smooth turning
        currentTurn = Mathf.Lerp(currentTurn, targetTurn, Time.deltaTime * 4f);

        // Apply movement & rotation
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * currentTurn * Time.deltaTime);
    }

    // --- Button Events ---
    public void OnForwardDown() { moveForward = true; }
    public void OnForwardUp() { moveForward = false; }

    public void OnBackwardDown() { moveBackward = true; }
    public void OnBackwardUp() { moveBackward = false; }

    public void OnLeftDown() { turnLeft = true; }
    public void OnLeftUp() { turnLeft = false; }

    public void OnRightDown() { turnRight = true; }
    public void OnRightUp() { turnRight = false; }
}
