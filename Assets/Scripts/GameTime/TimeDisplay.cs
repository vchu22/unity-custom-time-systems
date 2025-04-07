using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeOfDayText;  // Reference to a UI Text element that displays the time of a day
    public TMPro.TextMeshProUGUI dayText; // Reference to a UI Text element that displays the current day
    public TMPro.TextMeshProUGUI dayOfWeekText; // Reference to a UI Text element that displays the current day of week
    public string dayFormatString = "Day {0}";

    public bool displayAM_PM = false;  // The format to display the time in (True if want to display time as 00:00 AM)
    public bool abbreviateDayOfWeek = false;

    public Button pauseTimeButton;
    private TimeManager timeManager;
    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
    }
    private void OnEnable()
    {
        timeManager.OnMinuteChanged += UpdateTimeDisplay;
        timeManager.OnDayChanged += UpdateDayDisplay;
    }

    private void OnDisable()
    {
        timeManager.OnMinuteChanged -= UpdateTimeDisplay;
        timeManager.OnDayChanged -= UpdateDayDisplay;
    }

    // Update the UI text whenever the time changes
    private void UpdateTimeDisplay()
    {
        // Format and display the time in a readable format, e.g., "Day 1 - 12:00 PM"
        string period = "AM";
        int currentHour = timeManager.getCurrentHour();
        int currentMinute = timeManager.getCurrentMinute();

        if (currentHour >= 12)
        {
            currentHour = currentHour > 12 ? currentHour - 12 : 12;
            period = "PM";
        }
        timeOfDayText.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute) + (displayAM_PM ? " " + period : "");
    }

    // Update the UI text whenever the day changes
    private void UpdateDayDisplay()
    {
        dayText.text = string.Format(dayFormatString, timeManager.getCurrentDay());
        dayOfWeekText.text = abbreviateDayOfWeek ? timeManager.getDayOfWeekString().Substring(0, 3) : timeManager.getDayOfWeekString();
    }

    public void ChangePauseButtonIcon()
    {
        pauseTimeButton.GetComponentInChildren<TextMeshProUGUI>().text = timeManager.getPauseStatus() ? "Pause" : "Start";
        timeManager.TogglePause();
    }
    public void LoadSavedGameTime()
    {
        GameTime gameTime = new GameTime(2, 1, 32);
        timeManager.LoadTime(gameTime);
    }
}
