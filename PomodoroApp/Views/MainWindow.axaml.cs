using System.Diagnostics;
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