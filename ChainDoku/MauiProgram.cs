﻿using ChainDoku.Data;
using ChainDoku.Services;
using Microsoft.Extensions.Logging;

namespace ChainDoku
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<StateService>();
            builder.Services.AddSingleton<SudokuGridGenerator>();
            builder.Services.AddSingleton<SettingsService>();
            
            var app =  builder.Build();

            // HACK: Set KeepScreenOn after some time app initialized
            app.Services.GetService<SettingsService>().Init();

            return app;
        }
    }
}