using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Safest method: Move app to background
        try
        {
            using (var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = unity.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                activity.Call<bool>("moveTaskToBack", true);
            }
        }
        catch
        {
            Application.Quit();
        }
#else
        Application.Quit();
#endif
    }
}
