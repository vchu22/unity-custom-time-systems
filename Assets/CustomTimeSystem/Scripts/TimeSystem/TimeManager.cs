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
    public delegate void MonthChanged();
    public event MonthChanged OnMonthChanged;
    public delegate void YearChanged();
    public event YearChanged OnYearChanged;

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
        IncrementMinute();
    }
    // Increment time units
    private void IncrementMinute()
    {
        currentTime.minute += minuteIncrements;  // Increase time based on the speed multiplier

        // Check if an hour has passed
        if (currentTime.minute >= 60)
        {
            IncrementHour();
        }
        OnMinuteChanged();
    }
    private void IncrementHour()
    {
        int hourIncrements = currentTime.minute / calendar.maxMinutesInHour;
        currentTime.minute = currentTime.minute % calendar.maxMinutesInHour;
        currentTime.hour += hourIncrements;

        if (currentTime.hour >= calendar.maxHoursInDay)
        {
            IncrementDay();
        }
    }
    private void IncrementDay()
    {
        int dayIncrements = currentTime.hour / calendar.maxHoursInDay;
        currentTime.hour = currentTime.hour % calendar.maxHoursInDay;
        currentTime.day += dayIncrements;

        // Update day of week
        dayOfWeekIndex = ((int)(dayOfWeekIndex + dayIncrements) % calendar.daysOfWeek.Length);

        if (currentTime.day > calendar.monthsDays[currentTime.month].numberOfDays)
        {
            IncrementMonth();
        }
        OnDayChanged();
    }
    private void IncrementMonth()
    {
        int monthIncrements = 0;
        while (currentTime.day > calendar.monthsDays[currentTime.month].numberOfDays)
        {
            int diff = currentTime.day - calendar.monthsDays[currentTime.month].numberOfDays;
            if (diff > 0)
                currentTime.day = diff;
            else currentTime.day = 1;
            if (currentTime.month < calendar.monthsDays.Length - 1)
            {
                monthIncrements++;
                currentTime.month += 1;
            }
            else
            {
                IncrementYear(monthIncrements / calendar.monthsDays.Length + 1);
                currentTime.month = 0;
                break;
            }
        }
        OnMonthChanged();
    }
    private void IncrementYear(int yearIncrements)
    {
        currentTime.year += yearIncrements;
        OnYearChanged();
    }
    // Getters and setters
    public bool getPauseStatus() { return timePaused; }
    public int getCurrentMinute() { return currentTime.minute; }
    public int getCurrentHour() { return currentTime.hour; }
    public int getCurrentDay() { return currentTime.day; }
    public int getDayOfWeekIndex() { return dayOfWeekIndex; }
    public string getDayOfWeekString(bool abbreviate = false)
    {
        return abbreviate ? calendar.daysOfWeek[dayOfWeekIndex].Substring(0, 3) : calendar.daysOfWeek[dayOfWeekIndex];
    }
    public int getCurrentMonthIndex() { return currentTime.month; }
    public string getCurrentMonthString(bool abbreviate = false)
    {
        if (calendar.monthsDays.Length > 0)
        {
            return abbreviate ? calendar.monthsDays[currentTime.month].name.Substring(0, 3) : calendar.monthsDays[currentTime.month].name;
        }
        return null;
    }

    public int getCurrentYear() { return currentTime.year; }
}