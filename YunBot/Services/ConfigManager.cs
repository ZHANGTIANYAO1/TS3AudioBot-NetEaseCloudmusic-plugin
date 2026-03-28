using System.Text.Json;
using System.Text.Json.Serialization;

namespace YunBot.Services;

public class PluginConfig
{
    [JsonPropertyName("playMode")]
    public int PlayMode { get; set; }

    [JsonPropertyName("apiAddress")]
    public string ApiAddress { get; set; } = "https://127.0.0.1:3000";

    [JsonPropertyName("cookie")]
    public string Cookie { get; set; } = "";
}

public class ConfigManager
{
    private readonly string _configPath;
    private PluginConfig _config;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public ConfigManager(string configPath)
    {
        _configPath = configPath;
        _config = Load();
    }

    public PluginConfig Config => _config;

    private PluginConfig Load()
    {
        if (!File.Exists(_configPath))
        {
            // Migrate from old INI format if it exists
            var iniPath = Path.Combine(Path.GetDirectoryName(_configPath)!, "YunSettings.ini");
            if (File.Exists(iniPath))
                return MigrateFromIni(iniPath);

            var config = new PluginConfig();
            Save(config);
            return config;
        }

        var json = File.ReadAllText(_configPath);
        return JsonSerializer.Deserialize<PluginConfig>(json) ?? new PluginConfig();
    }

    private PluginConfig MigrateFromIni(string iniPath)
    {
        var config = new PluginConfig();
        foreach (var line in File.ReadAllLines(iniPath))
        {
            var parts = line.Split('=', 2);
            if (parts.Length != 2) continue;
            var key = parts[0].Trim();
            var value = parts[1].Trim();
            switch (key)
            {
                case "playMode" when int.TryParse(value, out var mode):
                    config.PlayMode = mode;
                    break;
                case "WangYiYunAPI_Address" when !string.IsNullOrEmpty(value):
                    config.ApiAddress = value;
                    break;
                case "cookies1" when !string.IsNullOrEmpty(value):
                    config.Cookie = value;
                    break;
            }
        }

        Save(config);
        Console.WriteLine($"[YunBot] Migrated config from {iniPath} to {_configPath}");
        return config;
    }

    public void Save(PluginConfig? config = null)
    {
        if (config != null)
            _config = config;

        var dir = Path.GetDirectoryName(_configPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(_config, JsonOptions);
        File.WriteAllText(_configPath, json);
    }

    public void UpdatePlayMode(int mode)
    {
        _config.PlayMode = mode;
        Save();
    }

    public void UpdateCookie(string cookie)
    {
        _config.Cookie = cookie;
        Save();
    }

    public void UpdateApiAddress(string address)
    {
        _config.ApiAddress = address;
        Save();
    }
}
