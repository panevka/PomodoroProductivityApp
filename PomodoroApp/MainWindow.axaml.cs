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
        var SettingsPanel = this.FindControl<SettingsControl>("SettingsControl");
        SettingsPanel.ParentControl = this;
    }

    private void SettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsControl.IsVisible = !SettingsControl.IsVisible;
        SettingsButton.IsEnabled = !SettingsControl.IsVisible;
        SettingsButton.Opacity = SettingsControl.IsVisible ? 0.3 : 1;
    }

   
}