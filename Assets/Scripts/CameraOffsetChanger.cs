using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraFollowAnimation : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Vector3 topViewOffset = new Vector3(0, 20, 0);   // top-down view
    public Vector3 backViewOffset = new Vector3(0, 5, -10); // back view
    public float transitionTime = 3f;                       // seconds

    void Start()
    {
        if (virtualCamera != null)
        {
            StartCoroutine(AnimateCamera());
        }
        else
        {
            Debug.LogError("Virtual Camera not assigned!");
        }
    }

    IEnumerator AnimateCamera()
    {
        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer == null)
        {
            Debug.LogError("No CinemachineTransposer found!");
            yield break;
        }

        transposer.m_FollowOffset = topViewOffset; // start from top view
        Vector3 startOffset = topViewOffset;
        Vector3 endOffset = backViewOffset;
        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionTime);
            transposer.m_FollowOffset = Vector3.Lerp(startOffset, endOffset, t);
            yield return null;
        }

        transposer.m_FollowOffset = endOffset; // final position
    }
}
