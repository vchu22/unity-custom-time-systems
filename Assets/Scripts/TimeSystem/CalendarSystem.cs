using System.Collections.Generic;

public enum CalendarSystem { Georgian, Seasons, Custom, None }
public enum DayOfWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

public class GameCalendar
{
    // Key-value pairs representing a list of months in a calendar year and their corresponding number of months
    Dictionary<string, int> monthDays;
    public int daysIn1Year = 365;

    public GameCalendar() : this(CalendarSystem.Georgian) { }
    public GameCalendar(CalendarSystem calendarSystem, int months = 4, int daysIn1Month = 30)
    {
        switch (calendarSystem)
        {
            case CalendarSystem.Georgian:
                monthDays = new Dictionary<string, int>()
                {
                    { "January", 31 }, { "February", 28 }, { "March", 31 },
                    { "April", 30 }, { "May", 31 }, { "June", 30 },
                    { "July", 31 }, { "August", 31 }, { "September", 30 },
                    { "October", 31 }, { "November", 30 }, { "December", 31 },
                };
                break;
            case CalendarSystem.Seasons:
                monthDays = new Dictionary<string, int>()
                {
                    { "Spring", daysIn1Month }, { "Summer", daysIn1Month }, { "March", daysIn1Month }, { "April", daysIn1Month }
                };
                daysIn1Year = daysIn1Month * 4;
                break;
            case CalendarSystem.Custom:
                for (int i = 0; i < 4; i++)
                {
                    monthDays.Add(i.ToString(), daysIn1Month);
                }
                daysIn1Year = daysIn1Month * months;
                break;
            default:
                monthDays = null;
                break;
        }
    }
    public int getMonthIndex(int days)
    {
        int daysCheckpoint = 0;
        int month = 0;
        foreach (var pair in monthDays)
        {
            daysCheckpoint += pair.Value;
            if (days > daysCheckpoint)
            {
                month++;
                break;
            }
        }
        return month;
    }
    public string getMonthString(int days)
    {
        int daysCheckpoint = 0;
        string month = "";
        foreach (var pair in monthDays)
        {
            daysCheckpoint += pair.Value;
            if (days <= daysCheckpoint)
            {
                month = pair.Key;
                break;
            }
        }
        return month;
    }
}