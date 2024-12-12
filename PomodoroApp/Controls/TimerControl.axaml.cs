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
        
        TimerButton.Click += (sender, args) =>
        {
            if (_timer.IsRunning)
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
        _timer.ChangeTimerInitialTime(SettingsManager.LoadSettings().WorkSessionDuration*60);
        TimerButton.Classes.Remove("PauseButton");
        TimerButton.Classes.Add("StartButton");
    }
}