[System.Serializable]
public class GameTime
{
    public int minute; // in-game minutes
    public int hour; // in-game hour
    public int day; // in-game day

    public GameTime() {
        this.minute = 0;
        this.hour = 0;
        this.day = 0;
    }
    public GameTime(int day, int hour, int minute) {
        this.minute = minute;
        this.hour = hour;
        this.day = day;
    }
}
