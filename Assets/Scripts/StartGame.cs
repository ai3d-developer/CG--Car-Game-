using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startPanel;   // Reference to your Start UI panel

    void Start()
    {
        // Pause the entire game at the beginning
        Time.timeScale = 0f;
    }

    public void OnStartButtonPressed()
    {
        // Hide the Start Panel
        startPanel.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
    }
}
