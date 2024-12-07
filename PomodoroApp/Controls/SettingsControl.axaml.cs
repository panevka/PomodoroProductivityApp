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
    public delegate void SettingsSavedEventHandler ();
    public delegate void SettingsClosedEventHandler ();
    public event SettingsClosedEventHandler SettingsClosed;
    public event SettingsSavedEventHandler? SettingsSaved;
    
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
        
        SaveButton.Click += OnSaveButtonClick;
        CloseButton.Click += OnCloseButtonClick;

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

    private void OnSaveButtonClick(object? sender, RoutedEventArgs e)
    {
        SettingsManager.SaveSettings(new AppSettings(WorkSessionDuration, ShortBreakDuration, LongBreakDuration));
        SettingsSaved?.Invoke();
    }
    private void OnCloseButtonClick(object? sender, RoutedEventArgs e)
    {
        SettingsClosed?.Invoke();
    }
    
}