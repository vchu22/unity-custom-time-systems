using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool timePaused = false;
    public float tickSeconds = 1f;   // The time interval to call InvokeRepeating
    public int minuteIncrements = 1; // Speed measured by how many in-game minutes should pass in 1 real world second (higher == faster)

    // Displayable time variables
    [SerializeField]
    private GameTime currentTime = new GameTime(); // Current in-game time

    // Events or functions to call when certain time milestones are reached
    public delegate void MinuteChanged();
    public event MinuteChanged OnMinuteChanged;

    private void Start()
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
    public void LoadTime(GameTime time)
    {
        currentTime.minute = time.minute;
        currentTime.hour = time.hour;

        OnMinuteChanged();
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
        }
        // Trigger the MinuteChanged event
        OnMinuteChanged();
    }

    // Getters and setters
    public bool getPauseStatus() { return timePaused; }
    public int getCurrentMinute() { return currentTime.minute; }
    public int getCurrentHour() { return currentTime.hour; }
}