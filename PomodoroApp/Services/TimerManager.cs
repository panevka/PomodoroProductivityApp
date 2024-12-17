using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace PomodoroApp;

public class TimerManager
{
    private readonly TextBlock _timerTextBlock;
    private int _currentTime;
    private bool _isPaused = true;
    private readonly object _lock = new();
    private CancellationTokenSource _cancellationTokenSource;

    public TimerManager(TextBlock timerTextBlock, int timerBaseTime = 0)
    {
        _currentTime = timerBaseTime;
        _timerTextBlock = timerTextBlock;
        UpdateTimerDisplay();
    }

    public event EventHandler TimerFinished;
    
    public bool IsTimerPaused()
    {
            return _isPaused; 
    }

    public bool IsRunning => !_isPaused;

    public async void StartTimer()
    {
        if (!_isPaused) return;
        try
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await TimerCountdownTask(_cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
            _cancellationTokenSource = null;
            Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            _isPaused = true;
        }
    }



    public void StopTimer()
    {
            _cancellationTokenSource?.Cancel();
    }

    public void ChangeTimerInitialTime(int newInitialTime)
    {
            if (!_isPaused) StopTimer();
            _currentTime = newInitialTime;
            UpdateTimerDisplay();
    }

    private async Task TimerCountdownTask(CancellationToken cancellationToken)
    {
        _isPaused = false;
        while (_currentTime > 0 && !cancellationToken.IsCancellationRequested)
        {
            _currentTime--;
            UpdateTimerDisplay();
            await Task.Delay(1000, cancellationToken);
        }
        _isPaused = true;
        if (_currentTime == 0 )
        {
            TimerFinished?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            throw new InvalidProgramException("Timer cancelled");
        }
    }

    private void UpdateTimerDisplay()
    {
        int seconds = _currentTime % 60;
        int hours = Convert.ToInt32(_currentTime / 60 / 60);
        int minutes = (_currentTime - _currentTime % 60) / 60 - hours * 60;
        _timerTextBlock.Text = $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
}