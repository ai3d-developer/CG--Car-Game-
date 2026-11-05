using UnityEngine;

public class AndroidBackQuit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("AndroidBackQuit: Back detected. Moving to background.");
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                using (var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var activity = unity.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // Show a short toast for debug (optional)
                    try
                    {
                        using (var toastClass = new AndroidJavaClass("android.widget.Toast"))
                        {
                            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                            {
                                var context = activity.Call<AndroidJavaObject>("getApplicationContext");
                                var jstr = new AndroidJavaObject("java.lang.String", "Exiting app...");
                                var toast = toastClass.CallStatic<AndroidJavaObject>("makeText", context, jstr, 0);
                                toast.Call("show");
                            }));
                        }
                    }
                    catch (System.Exception) { /* ignore toast errors */ }

                    // Move to background (safe)
                    activity.Call<bool>("moveTaskToBack", true);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("AndroidBackQuit native exit failed: " + e);
                Application.Quit();
            }
#else
            Application.Quit();
#endif
        }
    }
}
