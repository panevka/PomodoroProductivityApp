using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PomodoroApp;

public partial class Timer : UserControl
{ 
    public Timer()
    {
        InitializeComponent();
        StartTimerCountDown(Time, SettingsManager.LoadSettings().WorkSessionDuration*60);

    }

    private async void StartTimerCountDown(TextBlock timeComponent, int time)
    {
        await UpdateTimer(timeComponent, time);
    }
    
    private static async Task UpdateTimer(TextBlock timeComponent, int baseTime)
    {
        int seconds, hours, minutes;
        
        while (baseTime != 0)
        {
            baseTime--;
            seconds = baseTime % 60;
            hours = Convert.ToInt32(baseTime / 60 / 60);
            minutes = (baseTime - baseTime % 60) / 60 - hours * 60;
            timeComponent.Text = $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
            await Task.Delay(1000);
        }
    }
    
    
    
}