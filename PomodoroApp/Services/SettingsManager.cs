using System;
using System.IO;
using System.Text.Json;
using Path = System.IO.Path;

namespace PomodoroApp;

public static class SettingsManager
{
    private static readonly string _settingsFilePath = 
         Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PomodoroApp", "settings.json");
    private static AppSettings _currentSettings;
    
    public static void SaveSettings(AppSettings settings)
    {
        _currentSettings = settings;
        var directory = Path.GetDirectoryName(_settingsFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsFilePath, json);
    }

    public static AppSettings LoadSettings()
    {
        if (_currentSettings != null) return _currentSettings;
        if (File.Exists(_settingsFilePath))
        {
            var json = File.ReadAllText(_settingsFilePath);
            _currentSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        else
        {
            _currentSettings = new AppSettings();
        }

        return _currentSettings;
    }

}