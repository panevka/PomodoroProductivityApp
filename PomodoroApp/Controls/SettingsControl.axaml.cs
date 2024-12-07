using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PomodoroApp;

public partial class SettingsControl : UserControl
{
    private int _workSessionDuration = SettingsManager.LoadSettings().WorkSessionDuration;
    private int _shortBreakDuration = SettingsManager.LoadSettings().ShortBreakDuration;
    private int _longBreakDuration = SettingsManager.LoadSettings().LongBreakDuration;
    public MainWindow ParentControl { get; set; }
    public int WorkSessionDuration
    {
        get => _workSessionDuration;
        private set => _workSessionDuration = value;
    }

    public int ShortBreakDuration
    {
        get => _shortBreakDuration;
        private set => _shortBreakDuration = value;
    }
    
    public int LongBreakDuration
    {
        get => _longBreakDuration;
        private set => _longBreakDuration = value;
    }
    
    public SettingsControl()
    {
        InitializeComponent();
        WorkSessionSlider.Value = WorkSessionDuration;
        ShortBreakSlider.Value = ShortBreakDuration;
        LongBreakSlider.Value = LongBreakDuration;
        
        SaveButton.Click += SaveSettingsButton_Click;
        CloseButton.Click += CloseSettingsButton_Click;

        WorkSessionSlider.ValueChanged += (sender, args) =>
        {
            WorkSessionDuration = (int)args.NewValue;
        };
        
        ShortBreakSlider.ValueChanged += (sender, args) =>
        {
            ShortBreakDuration = (int)args.NewValue;
        };

        LongBreakSlider.ValueChanged += (sender, args) =>
        {
            LongBreakDuration = (int)args.NewValue;
        };
    }

    private void SaveSettingsButton_Click(object? sender, RoutedEventArgs e)
    {
        SettingsManager.SaveSettings(new AppSettings(WorkSessionDuration, ShortBreakDuration, LongBreakDuration));
    }
    private void CloseSettingsButton_Click(object? sender, RoutedEventArgs e)
    {
        IsVisible = false;
        ParentControl.SettingsButton.IsEnabled = !this.IsVisible;
        ParentControl.SettingsButton.Opacity = 1;
    }
    
}