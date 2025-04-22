[System.Serializable]
public class GameDateTime
{
    public int minute; // in-game minutes
    public int hour; // in-game hour
    public int day = 1;  // in-game day
    public int month; // in-game month
    public int year; // in-game year

    public GameDateTime() : this(0, 0) { }

    public GameDateTime(int hour, int minute)
    {
        this.minute = minute;
        this.hour = hour;
    }

    public GameDateTime(int day, int hour, int minute)
    {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
    }

    public GameDateTime(int month, int day, int hour, int minute)
    {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
        this.month = month;
    }

    public GameDateTime(int year, int month, int day, int hour, int minute)
    {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
        this.month = month;
        this.year = year;
    }
}
