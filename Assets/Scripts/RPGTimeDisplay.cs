using TMPro;

public class RPGTimeDisplay : TimeUI
{
    public bool abbreviateDayOfWeek = false;
    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = GetComponent<RPGTimeManager>();
    }
    private void OnEnable()
    {
        timeManager.OnMinuteChanged += UpdateTimeDisplay;
    }

    private void OnDisable()
    {
        timeManager.OnMinuteChanged -= UpdateTimeDisplay;
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
        //dayText.text = string.Format(dayFormatString, timeManager.getCurrentDay());
        //dayOfWeekText.text = abbreviateDayOfWeek ? timeManager.getDayOfWeekString().Substring(0, 3) : timeManager.getDayOfWeekString();
    }
}
