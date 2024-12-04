namespace PomodoroApp;

public class AppSettings
{
    public int WorkSessionDuration { get; set; }
    public int ShortBreakDuration { get; set; }
    public int LongBreakDuration { get; set; }

    public AppSettings()
    {
        WorkSessionDuration = 25;
        ShortBreakDuration = 5;
        LongBreakDuration = 30;
    }
    
    public AppSettings(int workSessionDuration, int shortBreakDuration, int longBreakDuration)
    {
        WorkSessionDuration = workSessionDuration;
        ShortBreakDuration = shortBreakDuration;
        LongBreakDuration = longBreakDuration;
    }
}