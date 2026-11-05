using UnityEngine;
using UnityEngine.UI;

public class CarControllerUI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 60f;

    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;

    private bool moveForward, moveBackward, turnLeft, turnRight;

    void Start()
    {
        // Assign button events
        forwardButton.onClick.AddListener(() => MoveOnce(Vector3.forward));
        backwardButton.onClick.AddListener(() => MoveOnce(Vector3.back));
        leftButton.onClick.AddListener(() => RotateOnce(-1));
        rightButton.onClick.AddListener(() => RotateOnce(1));
    }

    void MoveOnce(Vector3 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void RotateOnce(float dir)
    {
        transform.Rotate(Vector3.up * dir * turnSpeed * Time.deltaTime);
    }
}
