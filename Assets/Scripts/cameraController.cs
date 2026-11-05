using UnityEngine;
using UnityEngine.InputSystem.XR;

public class cameraController : MonoBehaviour
{
    public GameObject attachedVehicle;
    public GameObject CameraFolder;
    public Transform[] camLocations;
    public float locationIndicator = 2;
    public controller controllerRef;

    [Range(0, 1)] public float smoothTime = 0.5f;

    void Start()
    {
        attachedVehicle = GameObject.FindGameObjectWithTag("Player");
        CameraFolder = attachedVehicle.transform.Find("CAMERA").gameObject;
        camLocations = CameraFolder.GetComponentsInChildren<Transform>();
        controllerRef = attachedVehicle.GetComponent<controller>();
    }

    void FixedUpdate()
    {
        // Switch camera location when TAB key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (locationIndicator > 4 || locationIndicator < 2)
                locationIndicator = 2;
            else
                locationIndicator++;
        }

        // Smoothly move and rotate camera
        transform.position = camLocations[(int)locationIndicator].position * (1 - smoothTime) +
                             transform.position * smoothTime;

        transform.LookAt(camLocations[1].transform);

        // Adjust smoothness based on vehicle speed
        smoothTime = (controllerRef.KPH >= 150)
            ? Mathf.Abs((controllerRef.KPH) / 150) - 0.85f
            : 0.45f;
    }
}
