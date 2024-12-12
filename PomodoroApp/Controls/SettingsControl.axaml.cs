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
        InitializeDefaultValues();
        SaveButton.Click += OnSaveButtonClick;
        CloseButton.Click += OnCloseButtonClick;

        WorkSessionSlider.ValueChanged += (sender, args) =>
        {
            WorkSessionDuration = (int)args.NewValue;
            WorkSessionTextBox.Text = ConvertValueToHourRepresentative(WorkSessionDuration);
        };
        
        ShortBreakSlider.ValueChanged += (sender, args) =>
        {
            ShortBreakDuration = (int)args.NewValue;
            ShortBreakTextBox.Text = ConvertValueToHourRepresentative(ShortBreakDuration);
        };

        LongBreakSlider.ValueChanged += (sender, args) =>
        {
            LongBreakDuration = (int)args.NewValue;
            LongBreakTextBox.Text = ConvertValueToHourRepresentative(LongBreakDuration);
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

    private string ConvertValueToHourRepresentative(int sliderValue)
    {
        int hours = Convert.ToInt32(sliderValue / 60);
        int minutes = sliderValue % 60;
        
        if (hours == 0)
        {
            return $"{minutes + " m"}";
        }
        if (minutes == 0)
        {
            return $"{hours + " h"}";
        }
        return $"{hours  + "h " }{minutes + " m"}";
    }

    private void InitializeDefaultValues()
    {
        WorkSessionSlider.Value = WorkSessionDuration;
        ShortBreakSlider.Value = ShortBreakDuration;
        LongBreakSlider.Value = LongBreakDuration;
        
        WorkSessionTextBox.Text = ConvertValueToHourRepresentative(WorkSessionDuration);
        ShortBreakTextBox.Text = ConvertValueToHourRepresentative(ShortBreakDuration);
        LongBreakTextBox.Text = ConvertValueToHourRepresentative(LongBreakDuration);
    }
    
}