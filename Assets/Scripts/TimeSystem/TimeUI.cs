using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeOfDayText;  // Reference to a UI Text element that displays the time of a day
    public TextMeshProUGUI dayText;  // Reference to a UI Text element that displays numbers of days passed or the day in a month
    public bool abbreviateDayOfWeek;
    public TextMeshProUGUI dayOfWeekText;  // Reference to a UI Text element that displays the day in the week
    public TextMeshProUGUI monthText;
    public Button pauseTimeButton;

    public bool displayAM_PM = false;  // The format to display the time in (True if want to display time as 00:00 AM)

    protected TimeManager timeManager;
    

    protected void Awake()
    {
        timeManager = GetComponent<TimeManager>();
    }
    protected void OnEnable()
    {
        timeManager.OnMinuteChanged += UpdateTimeDisplay;
        timeManager.OnDayChanged += UpdateDayDisplay;
        timeManager.OnMonthChanged += UpdateMonthDisplay;
    }

    protected void OnDisable()
    {
        timeManager.OnMinuteChanged -= UpdateTimeDisplay;
        timeManager.OnDayChanged -= UpdateDayDisplay;
        timeManager.OnMonthChanged -= UpdateMonthDisplay;
    }

    // Update the UI text whenever the time changes
    protected void UpdateTimeDisplay()
    {
        // Format and display the time in a readable format, e.g., "12:00 PM"
        string period = "AM";
        int currentHour = timeManager.getCurrentHour();
        int currentMinute = timeManager.getCurrentMinute();

        if (displayAM_PM && currentHour >= 12)
        {
            currentHour = currentHour > 12 ? currentHour - 12 : 12;
            period = "PM";
        }
        timeOfDayText.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute) + (displayAM_PM ? " " + period : "");
    }
    private void UpdateDayDisplay()
    {
        dayText.text = timeManager.getCurrentDay().ToString();
        dayOfWeekText.text = abbreviateDayOfWeek ? timeManager.getDayOfWeek().Substring(0, 3) : timeManager.getDayOfWeek();
    }
    private void UpdateMonthDisplay()
    {
        monthText.text = timeManager.getCurrentMonthString();
    }
    public void ChangePauseButtonIcon()
    {
        pauseTimeButton.GetComponentInChildren<TextMeshProUGUI>().text = timeManager.getPauseStatus() ? "Pause" : "Start";
        timeManager.TogglePause();
    }
    public void LoadSavedGameTime()
    {
        GameDateTime gameTime = new GameDateTime(2, 1, 32);
        timeManager.LoadTime(gameTime);
    }
}
