using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] public float levelTime = 60f; // Set the initial time for the level
    [SerializeField] private float currentTime;

    [SerializeField] public Text timerText;

    void Start()
    {
        currentTime = levelTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            // Reset the level when the time runs out
            ResetLevel();
        }
    }

    void UpdateTimerUI()
    {
        // Display the timer in a minutes:seconds format
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ResetLevel()
    {
        // You can replace this with your own logic for resetting the level
        // For example, reloading the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
