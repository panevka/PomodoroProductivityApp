using System;

namespace PomodoroApp;

public class SessionManager
{
    private SessionType _currentSession;
    private TimerManager _timer;
    private int _cycleCounter;

    public SessionManager(TimerManager timer)
    {
        _timer = timer;
        SwitchSessionType(SessionType.Work);
        _timer.TimerFinished += PomodoroSessionSwitch;
    }
        
    public event EventHandler SessionSwitched;

    public void PomodoroSessionSwitch(object? obj, EventArgs e)
    {
        if (_cycleCounter == 4)
        {
            SwitchSessionType(SessionType.LongBreak);
            _cycleCounter = 0;
        }
        else
        {
            switch (_currentSession)
            {
                case SessionType.Work:
                    SwitchSessionType(SessionType.ShortBreak);
                    break;
                case SessionType.ShortBreak:
                    SwitchSessionType(SessionType.Work);
                    _cycleCounter++;
                    break;
                case SessionType.LongBreak:
                    SwitchSessionType(SessionType.Work);
                    break;
            }
        }
        _timer.StartTimer();
    }
    
    private void SwitchSessionType(SessionType newSessionType)
    {
        switch (newSessionType)
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
        _currentSession = newSessionType;
        SessionSwitched?.Invoke(this, EventArgs.Empty);
    }

    public SessionType GetCurrentSessionType()
    {
        return _currentSession;
    }
}