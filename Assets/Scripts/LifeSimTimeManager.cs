using UnityEngine;

public class LifeSimTimeManager : TimeManager
{
    private bool timePaused = false;

    // Displayable time variables
    private GameTime currentTime = new GameTime(); // Current in-game time

    private DayOfWeek dayOfWeek = DayOfWeek.Sunday;

    // Events or functions to call when certain time milestones are reached
    public delegate void DayChanged();
    public event DayChanged OnDayChanged;

    private void Start()
    {
        // Invoke time progress every second
        InvokeRepeating(nameof(ProgressTime), 0, tickSeconds);
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

            currentTime.hour += hourIncrements;
            if (currentTime.hour >= 24) // Reset to 0 if hour reaches 24 (next day)
            {
                int dayIncrements = currentTime.hour / 24;
                currentTime.hour = currentTime.hour % 24;
                currentTime.day += dayIncrements;

                // Update day of week
                dayOfWeek = (DayOfWeek)((int)(dayOfWeek + dayIncrements) % 7);

                // Trigger the DayChanged event
                OnDayChanged();
            }
        }
        // Trigger the MinuteChanged event
        //OnMinuteChanged();
    }

    // Getters and setters
    public int getCurrentDay() { return currentTime.day; }
    public DayOfWeek getDayOfWeek() { return dayOfWeek; }
    public string getDayOfWeekString() { return dayOfWeek.ToString(); }
}