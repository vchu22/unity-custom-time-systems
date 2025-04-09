using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    protected bool timePaused = false;  // Whether the clock is paused
    public float tickSeconds = 1f;   // The number of seconds for each InvokeRepeating
    public int minuteIncrements = 1; // Speed measured by the number of in-game minutes a real world second is equal to (higher means faster)
    
    // Displayable time variables
    [SerializeField]
    private GameDateTime currentTime = new GameDateTime(); // Current in-game time
    public GameCalendar calendar;
    int dayOfWeekIndex = 0;

    // Events or functions to call when certain time milestones are reached
    public delegate void MinuteChanged();
    public event MinuteChanged OnMinuteChanged;

    public delegate void DayChanged();
    public event DayChanged OnDayChanged;

    protected void Start()
    {
        // Invoke time progress every second
        InvokeRepeating(nameof(ProgressTime), 0, tickSeconds);
    }
    public void TogglePause()
    {
        timePaused = !timePaused;
        if (timePaused)
        {
            CancelInvoke(nameof(ProgressTime));
        }
        else
        {
            InvokeRepeating(nameof(ProgressTime), 0, tickSeconds);
        }
    }

    // Load game time from save file
    public void LoadTime(GameDateTime time)
    {
        currentTime.minute = time.minute;
        currentTime.hour = time.hour;

        OnMinuteChanged();
    }

    // Coroutine to progress time
    protected void ProgressTime()
    {
        currentTime.minute += minuteIncrements;  // Increase time based on the speed multiplier

        // Check if an hour has passed
        if (currentTime.minute >= 60) // 60 minutes = 1 hour
        {
            int hourIncrements = currentTime.minute / 60;
            currentTime.minute = currentTime.minute % 60;

            currentTime.hour += hourIncrements;
            if (currentTime.hour >= 24) // Reset to 0 if hour reaches 24 (next day)
            {
                int dayIncrements = currentTime.hour / 24;
                currentTime.hour = currentTime.hour % 24;
                currentTime.day += dayIncrements;

                // Update day of week
                dayOfWeekIndex = ((int)(dayOfWeekIndex + dayIncrements) % calendar.daysOfWeek.Length);

                // Trigger the DayChanged event
                OnDayChanged();
            }
        }
        // Trigger the MinuteChanged event
        OnMinuteChanged();
    }

    // Getters and setters
    public bool getPauseStatus() { return timePaused; }
    public int getCurrentMinute() { return currentTime.minute; }
    public int getCurrentHour() { return currentTime.hour; }
    public string getDayOfWeek() { return calendar.daysOfWeek[dayOfWeekIndex]; }
}