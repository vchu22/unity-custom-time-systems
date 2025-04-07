[System.Serializable]
public class GameDateTime : GameTime
{
    public int month; // in-game month
    public int year; // in-game year

    public GameCalendar calendar;

    public GameDateTime() : this(0,0,0) { }
    public GameDateTime(int day, int hour, int minute)
    {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
        this.year = 0;
    }

    public GameDateTime(int day, int hour, int minute, int year)
    {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
        this.year = year;
        calendar = new GameCalendar();
    }

    public string getMonth()
    {
        if (calendar == null)
        {
            return null;
        }
        return calendar.getMonthString(day);
    }
}
