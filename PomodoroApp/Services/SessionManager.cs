using System;

namespace PomodoroApp;

public class SessionManager
{
    private SessionType _currentSession;
    private TimerManager _timer;
    public void SwitchSessionType(TimerManager timerManager, SessionType newSessionType)
    {
        switch (_currentSession)
        {
            case SessionType.Work:
                timerManager.ChangeTimerInitialTime(SettingsManager.LoadSettings().WorkSessionDuration);
                break;
            case SessionType.ShortBreak:
                timerManager.ChangeTimerInitialTime(SettingsManager.LoadSettings().ShortBreakDuration);
                break;
            case SessionType.LongBreak:
                timerManager.ChangeTimerInitialTime(SettingsManager.LoadSettings().LongBreakDuration);
                break;
        }

        _currentSession = newSessionType;
    }

    public SessionType GetCurrentSession()
    {
        return _currentSession;
    }
}