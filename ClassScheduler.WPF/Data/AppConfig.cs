using ClassScheduler.WPF.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace ClassScheduler.WPF.Data;

public class AppConfig
{
    public TopmostEffectsSettings TopmostEffectsSettings { get; set; } = new();

    public WallPaperSettings WallPaperSettings { get; set; } = new();

    public AppBarConfig AppBarConfig { get; set; } = new();
}

public class TopmostEffectsSettings
{
    public bool? IsDateTimeVisible { get; set; } = true;
}

public class WallPaperSettings
{
    public string? WallPapersPath { get; set; }

    public int CurrentWallPaperIndex { get; set; } = 0;

    public bool? WallPapersEnabled { get; set; } = false;

    public int PreparationLeadTime { get; set; } = 60;

    public WallPaperStyle? WallPaperStyle { get; set; } = Utils.WallPaperStyle.Stretched;
}

public class AppBarConfig
{
    public bool EnabledAll { get; set; } = true;
    public bool EnabledShowTime { get; set; } = true;
    public bool EnabledShowDate { get; set; } = true;
    public bool EnabledShowWeekDay { get; set; } = true;
    public bool EnabledShowTimeLeft { get; set; } = true;
    public bool EnabledShowWeather { get; set; } = true;
    public string WeatherCityLocID { get; set; } = "101010100";  // 北京
    public bool ShowWeatherRegularly { get; set; } = false;
    public String WeatherRegularlyBeginTime { get; set; } = "06:00";
    public String WeatherRegularlyEndTime { get; set; } = "23:00";
    public bool EnabledShowSentence { get; set; } = true;
}

public static class AppConfigExtensions
{
    public static void Save(
        this AppConfig config,
        string path = "./Data/AppConfig.json",
        Action<JsonSerializerOptions>? optionsProcessor = null)
    {
        var options = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
        };
        optionsProcessor?.Invoke(options);

        var json = JsonSerializer.Serialize(config, options);
        File.WriteAllText(path, json);
    }

    public static AppConfig? LoadAsAppConfig(
        this string path,
        Action<JsonSerializerOptions>? optionsProcessor = null)
    {
        if (!Path.Exists(path)) return null;

        var options = new JsonSerializerOptions();
        optionsProcessor?.Invoke(options);

        var json = File.ReadAllText(path);
        var appConfig = JsonSerializer.Deserialize<AppConfig>(json, options);

        return appConfig;
    }
}
