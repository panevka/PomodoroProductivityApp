using Avalonia.Controls;

namespace PomodoroApp;

public partial class Timer : UserControl
{
    private TimerManager _timer;
    public Timer()
    {
        InitializeComponent();
        _timer = new TimerManager(Time, SettingsManager.LoadSettings().WorkSessionDuration * 60);
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
}