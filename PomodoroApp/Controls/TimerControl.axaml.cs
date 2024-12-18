using Avalonia.Controls;

namespace PomodoroApp;

public partial class TimerControl : UserControl
{
    private TimerManager _timer;
    private SessionManager _session;

    public SessionManager Session
    {
        get => _session;
    }
    public TimerControl()
    {
        InitializeComponent();
        _timer = new TimerManager(Time);
        _session = new SessionManager(_timer);
        RefreshButton.Click += (sender, args) =>
        {
            RefreshButton.Classes.Clear();
            RefreshButton.Classes.Add("rotating");
            ReloadTimerSettings();
        };
        
        TimerButton.Click += (sender, args) =>
        {
            if (!_timer.IsTimerPaused())
            {
                TimerButton.Classes.Remove("PauseButton");
                TimerButton.Classes.Add("StartButton");
                _timer.StopTimer();
            }
            else
            {
                TimerButton.Classes.Remove("StartButton");
                TimerButton.Classes.Add("PauseButton");
                _timer.StartTimer(); 
            }
        };

    }
    public void ReloadTimerSettings()
    {
        switch (_session.GetCurrentSessionType())
        {
            case SessionType.Work:
                _timer.ChangeTimerInitialTime(SettingsManager.LoadSettings().WorkSessionDuration*60);
                break;
            case SessionType.ShortBreak:
                _timer.ChangeTimerInitialTime(SettingsManager.LoadSettings().ShortBreakDuration*60);
                break;
            case SessionType.LongBreak:
                _timer.ChangeTimerInitialTime(SettingsManager.LoadSettings().LongBreakDuration*60);
                break;
        }
        TimerButton.Classes.Remove("PauseButton");
        TimerButton.Classes.Add("StartButton");
    }
}