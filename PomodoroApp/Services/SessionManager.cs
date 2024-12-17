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
        _timer.TimerFinished += (sender, args) =>
        {
            SwitchToNextSession();
            _timer.StartTimer();
        };
    }

    public delegate void SessionSwitchedEventHandler();
    public event SessionSwitchedEventHandler SessionSwitched;

    public void SwitchToNextSession()
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
    }
    
    public void SwitchToPreviousSession()
    {
        switch (_currentSession)
        {
            case SessionType.Work:
                if (_cycleCounter == 0)
                {
                    SwitchSessionType(SessionType.LongBreak);
                }
                else
                {
                    SwitchSessionType(SessionType.ShortBreak);
                    _cycleCounter--;
                }
                break;
            case SessionType.ShortBreak:
                SwitchSessionType(SessionType.Work);
                break;
            case SessionType.LongBreak:
                SwitchSessionType(SessionType.Work);
                _cycleCounter = 4;
                break;
        }
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
        SessionSwitched?.Invoke();
    }

    public SessionType GetCurrentSessionType()
    {
        return _currentSession;
    }
}