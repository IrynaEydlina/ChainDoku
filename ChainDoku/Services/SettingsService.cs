using ChainDoku.Enums;
using Microsoft.Extensions.Logging;
using Models.Enums;

namespace ChainDoku.Services;

internal class SettingsService
{
    private readonly ILogger<SettingsService> _logger;
    private const string DiffKey = "DifficultyKey";
    private const string ThemeKey = "ThemeKey";
    private Difficulty _difficulty;
    private ThemeEnum _theme;

    public SettingsService(ILogger<SettingsService> logger)
    {
        _logger = logger;
        _difficulty = (Difficulty)Preferences.Get(DiffKey, 0);
        _theme = (ThemeEnum)Preferences.Get(ThemeKey, 0);
    }

    public async void Init()
    {
        await Task.Delay(TimeSpan.FromSeconds(15));
        try
        {
            DeviceDisplay.KeepScreenOn = true;
            _logger.LogInformation("Settings Initialized");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "DeviceDisplay not available");
        }
    }

    public Difficulty Difficulty
    {
        get => _difficulty;

        set
        {
            _difficulty = value;
            Preferences.Set(DiffKey, (int)_difficulty);
        }
    }

    public ThemeEnum Theme
    {
        get => _theme;
        set
        {
            _theme = value;
            Preferences.Set(ThemeKey, (int)_theme);
        }
    }
}