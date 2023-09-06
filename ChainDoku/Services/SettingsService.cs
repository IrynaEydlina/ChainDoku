using Microsoft.Extensions.Logging;

namespace ChainDoku.Services;

internal class SettingsService
{
    private readonly ILogger<SettingsService> _logger;

    public SettingsService(ILogger<SettingsService> logger) => this._logger = logger;

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
}