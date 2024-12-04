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
    private int _workSessionDuration;
    private int _shortBreakDuration;
    private int _longBreakDuration;

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
        
        SaveButton.Click += SaveSettingsButton_Click;

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
    
    
}