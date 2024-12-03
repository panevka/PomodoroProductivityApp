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
    }

    private void SettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsControl.IsVisible = !SettingsControl.IsVisible;
    }

   
}