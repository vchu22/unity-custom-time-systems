public enum CalendarSystem { Georgian, Seasons, Custom, None }

[System.Serializable]
public class GameCalendar
{
    // A list representing the months in a calendar year and their corresponding number of days
    public Month[] monthsDays;
    public string[] daysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    public int daysIn1Year = 365;

    public GameCalendar() : this(CalendarSystem.Georgian) { }
    public GameCalendar(CalendarSystem calendarSystem, int daysInMonth = 30, int monthsInYear = 3)
    {
        switch (calendarSystem)
        {
            case CalendarSystem.Georgian:
                monthsDays = new Month[12] {
                    new Month("January", 31), new Month("February", 28), new Month("March", 31),
                    new Month("April", 30), new Month("May", 31), new Month("June", 30),
                    new Month("July", 31), new Month("August", 31), new Month("September", 30),
                    new Month("October", 31), new Month("November", 30), new Month("December", 31),
                };
                break;
            case CalendarSystem.Seasons:
                monthsDays = new Month[4] {
                    new Month("Spring", daysInMonth), new Month("Summer", daysInMonth),
                    new Month("Fall", daysInMonth), new Month("Winter", daysInMonth)
                };
                break;
            case CalendarSystem.Custom:
                monthsDays = new Month[monthsInYear];
                for (int i = 0; i < monthsInYear; i++)
                {
                    monthsDays[i] = new Month("", daysInMonth);
                }
                daysIn1Year = daysInMonth * monthsInYear;
                break;
            default:
                monthsDays = null;
                break;
        }
    }
}

[System.Serializable]
public class Month
{
    public string name;
    public int numberOfDays;

    public Month(string name, int numberOfDays)
    {
        this.name = name;
        this.numberOfDays = numberOfDays;
    }
}