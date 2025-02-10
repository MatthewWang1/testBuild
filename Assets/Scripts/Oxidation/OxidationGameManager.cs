using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OxidationGameManager : MonoBehaviour
{
    public Text timerText; // Reference to the TimerText UI
    [SerializeField] private float timerDuration; // Duration of the timer
    private float currentTime;
    private bool isTimerRunning = true;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            // Debug.Log("running");
            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                DisableInput();
            }

            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        // Update the UI Text to show the remaining time
        timerText.text = $"{currentTime:F1}"; // Format to 1 decimal place
    }

    void DisableInput()
    {
        // Disable all player input (example: set a static flag or disable components)
        Clickable.isInputEnabled = false;
    }

    public void Reset()
    {
        isTimerRunning = true;
        currentTime = timerDuration;
        UpdateTimerText();
    }

}
