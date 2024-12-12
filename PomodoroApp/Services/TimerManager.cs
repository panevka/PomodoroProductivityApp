using System;
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
  
    public TimerManager(TextBlock timerTextBlock, int timerBaseTime=0)
    {
        _currentTime = timerBaseTime;
        _timerTextBlock = timerTextBlock;
        UpdateTimerDisplay();
    }
      
    public event EventHandler TimerFinished;
    
    public bool IsPaused => _isPaused;
    public bool IsRunning => !_isPaused;

    public async void StartTimer()
    {
        lock (_lock)
        {
            if (!_isPaused) return; 
            _isPaused = false;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        try
        {
            await TimerCountdownTask(_cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            lock (_lock)
            {
                _isPaused = true;
                _cancellationTokenSource = null;
                _currentTime = 0;
            }
        }
    }

    public void StopTimer()
    {
        lock (_lock)
        {
            if (_isPaused) return;
            _isPaused = true;
            _cancellationTokenSource?.Cancel();
        }
    }

    public void ChangeTimerInitialTime(int newInitialTime)
    {
        StopTimer();
        Interlocked.Exchange(ref _currentTime, newInitialTime);
        UpdateTimerDisplay();
    }

    private async Task TimerCountdownTask(CancellationToken cancellationToken)
    {
        while (_currentTime >= 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(1000, cancellationToken);
            Interlocked.Decrement(ref _currentTime);
            UpdateTimerDisplay();
        }
        lock (_lock)
        {
            _isPaused = true;
        }
        TimerFinished?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateTimerDisplay()
    {
        int seconds = _currentTime % 60;
        int hours = Convert.ToInt32(_currentTime / 60 / 60);
        int minutes = (_currentTime - _currentTime % 60) / 60 - hours * 60;
        _timerTextBlock.Text = $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
}