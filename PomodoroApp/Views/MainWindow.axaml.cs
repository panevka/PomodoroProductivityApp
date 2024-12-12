using System;
using System.Diagnostics;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PomodoroApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SettingsButton.Click += SettingsButton_OnClick;
        SettingsControl.SettingsSaved += TimerControl.ReloadTimerSettings;
        SettingsControl.SettingsClosed += OnSettingsClosed;
        TimerControl.Session.SessionSwitched += OnSessionSwitched;
    }

    private void OnSessionSwitched(object? sender, EventArgs e)
    {
        SessionType currentSessionType = TimerControl.Session.GetCurrentSessionType();
        
        switch (currentSessionType)
        {
            case SessionType.Work:
                Header.Text = "Work Session Running";
                break;
            case SessionType.ShortBreak:
                Header.Text = "Short-Break Session Running";
                break;
            case SessionType.LongBreak:
                Header.Text = "Long-Break Session Running";
                break;
        }
    }

    private void SettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsControl.IsVisible = true;
        SettingsButton.IsEnabled = false;
        SettingsButton.Opacity = 0.3;
    }
   
    private void OnSettingsClosed()
    {
        SettingsControl.IsVisible = false;
        SettingsButton.IsEnabled = true;
        SettingsButton.Opacity = 1;
    }
}