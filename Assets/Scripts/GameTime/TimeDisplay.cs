using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeText;  // Reference to a UI Text element to display the time

    private void OnEnable()
    {
        //TimeManager.OnDayChanged += UpdateTimeDisplay;
        //TimeManager.OnHourChanged += UpdateTimeDisplay;
    }

    private void OnDisable()
    {
        //TimeManager.OnDayChanged -= UpdateTimeDisplay;
        //TimeManager.OnHourChanged -= UpdateTimeDisplay;
    }

    // Update the UI text whenever the time changes
    private void UpdateTimeDisplay(int time)
    {
        // Format and display the time in a readable format, e.g., "Day 1 - 12:00 PM"
        //int displayHour = time;
        //string period = "AM";

        //if (displayHour >= 12)
        //{
        //    displayHour = displayHour > 12 ? displayHour - 12 : 12;
        //    period = "PM";
        //}

        //timeText.text = "Day " + TimeManager.day + " - " + displayHour + ":00 " + period;
    }
}
