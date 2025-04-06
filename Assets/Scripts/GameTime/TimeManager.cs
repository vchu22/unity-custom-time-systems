using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private bool timePaused = false;
    public float tickSeconds = 1f;   // The time interval to call InvokeRepeating
    public int minuteIncrements = 1; // Speed measured by how many in-game minutes should pass in 1 real world second (higher == faster)

    // Displayable time variables
    public GameTime currentTime = new GameTime(); // Current in-game time

    public enum DayOfWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }
    public DayOfWeek dayOfWeek = DayOfWeek.Sunday;

    // Events or functions to call when certain time milestones are reached
    public delegate void MinuteChanged(int newHour);
    public static event MinuteChanged OnMinuteChanged;

    public delegate void HourChanged(int newHour);
    public static event HourChanged OnHourChanged;

    public delegate void DayChanged(int newDay);
    public static event DayChanged OnDayChanged;

    private void Start()
    {
        // Invoke time progress every second
        InvokeRepeating(nameof(ProgressTime), 0, tickSeconds);
    }
    public void TooglePause(Button btn)
    {
        timePaused = !timePaused;
        btn.GetComponentInChildren<TextMeshProUGUI>().text = timePaused? "Start": "Pause";
        if (timePaused)
        {
            CancelInvoke(nameof(ProgressTime));
        }
        else
        {
            InvokeRepeating(nameof(ProgressTime), 0, tickSeconds);
        }
    }

    // Coroutine to progress time
    private void ProgressTime()
    {
        currentTime.minute += minuteIncrements;  // Increase time based on the speed multiplier

        // Check if an hour has passed
        if (currentTime.minute >= 60) // 60 minutes = 1 hour
        {
            int hourIncrements = currentTime.minute / 60;
            currentTime.minute = currentTime.minute % 60;
            // Trigger the MinuteChanged event
            if (OnMinuteChanged != null)
                OnMinuteChanged(currentTime.minute);

            currentTime.hour += hourIncrements;
            if (currentTime.hour >= 24) // Reset to 0 if hour reaches 24 (next day)
            {
                int dayIncrements = currentTime.hour / 24;
                currentTime.hour = currentTime.hour % 24;
                currentTime.day += dayIncrements;

                // Update day of week
                dayOfWeek = (DayOfWeek)((int)(dayOfWeek + dayIncrements) % 7);

                // Trigger the DayChanged event
                if (OnDayChanged != null)
                    OnDayChanged(currentTime.day);
            }

            // Trigger the HourChanged event
            if (OnHourChanged != null)
                OnHourChanged(currentTime.hour);
        }

    }
}