using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.CommandSystem;
using TS3AudioBot.Plugins;
using TSLib.Full;
using YunBot.Services;

namespace YunBot;

public enum PlayMode
{
    Sequential = 0,
    SequentialLoop = 1,
    Random = 2,
    RandomLoop = 3
}

public class YunPlugin : IBotPlugin
{
    private static readonly string ConfigPath = Path.Combine("plugins", "YunSettings.json");

    private ConfigManager _configManager = null!;
    private NeteaseApiClient _api = null!;
    private readonly List<long> _songQueue = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly Random _random = new();
    private int _currentIndex;
    private bool _eventSubscribed;

    // Saved references for playback continuation
    private PlayManager? _playManager;
    private InvokerData? _invoker;
    private Ts3Client? _ts3Client;

    public void Initialize()
    {
        _configManager = new ConfigManager(ConfigPath);
        _api = new NeteaseApiClient(
            _configManager.Config.ApiAddress,
            _configManager.Config.Cookie);
        Console.WriteLine("[YunBot] Plugin loaded");
        Console.WriteLine($"[YunBot] PlayMode={_configManager.Config.PlayMode}, API={_configManager.Config.ApiAddress}");
    }

    public void Dispose()
    {
        _songQueue.Clear();
        _api?.Dispose();
        _lock.Dispose();
    }

    private void SaveContext(PlayManager pm, InvokerData inv, Ts3Client ts3)
    {
        _playManager = pm;
        _invoker = inv;
        _ts3Client = ts3;
    }

    private void EnsureEventSubscribed(Player player)
    {
        if (_eventSubscribed) return;
        player.OnSongEnd += OnSongEnd;
        _eventSubscribed = true;
    }

    private PlayMode CurrentMode => (PlayMode)_configManager.Config.PlayMode;

    // ========== Commands ==========

    [Command("yun play")]
    public async Task<string> CommandPlay(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);

        var search = await _api.SearchSongAsync(arguments);
        var song = search.Result?.Songs?.FirstOrDefault();
        if (song == null) return "未找到歌曲";

        var url = await _api.GetMusicUrlAsync(song.Id);
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接，可能需要登录VIP账户";

        var detail = await _api.GetSongDetailAsync(song.Id);
        if (detail?.Album?.PicUrl != null)
            await MainCommands.CommandBotAvatarSet(ts3Client, detail.Album.PicUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, song.Name);

        await MainCommands.CommandPlay(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"正在播放：{song.Name}");
        return $"正在播放：{song.Name}";
    }

    [Command("yun playid")]
    public async Task<string> CommandPlayId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);

        var url = await _api.GetMusicUrlAsync(id);
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接";

        var detail = await _api.GetSongDetailAsync(id);
        var name = detail?.Name ?? id.ToString();
        if (detail?.Album?.PicUrl != null)
            await MainCommands.CommandBotAvatarSet(ts3Client, detail.Album.PicUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, name);

        await MainCommands.CommandPlay(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"正在播放：{name}");
        return $"正在播放：{name}";
    }

    [Command("yun add")]
    public async Task<string> CommandAdd(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);

        var search = await _api.SearchSongAsync(arguments);
        var song = search.Result?.Songs?.FirstOrDefault();
        if (song == null) return "未找到歌曲";

        var url = await _api.GetMusicUrlAsync(song.Id);
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接";

        await MainCommands.CommandAdd(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"已添加到播放列表：{song.Name}");
        return $"已添加到播放列表：{song.Name}";
    }

    [Command("yun addid")]
    public async Task<string> CommandAddId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);

        var url = await _api.GetMusicUrlAsync(id);
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接";

        var detail = await _api.GetSongDetailAsync(id);
        var name = detail?.Name ?? id.ToString();

        await MainCommands.CommandAdd(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"已添加到播放列表：{name}");
        return $"已添加到播放列表：{name}";
    }

    [Command("yun gedanid")]
    public async Task<string> CommandPlaylistById(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        return await LoadAndPlayPlaylist(id, playManager, invoker, ts3Client);
    }

    [Command("yun gedan")]
    public async Task<string> CommandPlaylistByName(string name, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        var search = await _api.SearchPlaylistAsync(name);
        var playlist = search.Result?.Playlists?.FirstOrDefault();
        if (playlist == null) return "未找到歌单";

        return await LoadAndPlayPlaylist(playlist.Id, playManager, invoker, ts3Client);
    }

    [Command("yun next")]
    public async Task<string> CommandNext(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);

        if (!playManager.IsPlaying || _songQueue.Count == 0)
            return "播放列表为空，无法播放下一首";

        await playManager.Stop();
        return "切换下一首";
    }

    [Command("yun mode")]
    public string CommandMode(int mode)
    {
        if (mode < 0 || mode > 3) return "请输入正确的播放模式 (0-3)";

        _configManager.UpdatePlayMode(mode);
        return (PlayMode)mode switch
        {
            PlayMode.Sequential => "当前播放模式：顺序播放",
            PlayMode.SequentialLoop => "当前播放模式：顺序循环",
            PlayMode.Random => "当前播放模式：随机播放",
            PlayMode.RandomLoop => "当前播放模式：随机循环",
            _ => "未知模式"
        };
    }

    [Command("yun list")]
    public string CommandList()
    {
        if (_songQueue.Count == 0) return "播放列表为空";
        return $"播放列表中共有 {_songQueue.Count} 首歌曲，当前模式：{CurrentMode}";
    }

    [Command("yun clear")]
    public string CommandClear()
    {
        _songQueue.Clear();
        return "播放列表已清空";
    }

    [Command("yun login")]
    public async Task<string> CommandLogin(Ts3Client ts3Client, TsFullClient tsClient)
    {
        var key = await _api.GetQrKeyAsync();
        var qrImgBase64 = await _api.GetQrImageAsync(key);

        await ts3Client.SendChannelMessage("正在生成登录二维码...");

        // Upload QR code as bot avatar
        var parts = qrImgBase64.Split(',');
        var imgData = Convert.FromBase64String(parts.Length > 1 ? parts[1] : parts[0]);
        using var stream = new MemoryStream(imgData);
        await tsClient.UploadAvatar(stream);
        await ts3Client.ChangeDescription("请用网易云APP扫描机器人头像二维码登录");

        // Poll login status
        for (var i = 0; i < 120; i++)
        {
            await Task.Delay(1000);
            var status = await _api.CheckQrStatusAsync(key);

            if (status.Code == 803)
            {
                var cookie = status.Cookie ?? "";
                _api.Cookie = cookie;
                _configManager.UpdateCookie(cookie);
                await tsClient.DeleteAvatar();
                await ts3Client.SendChannelMessage("登录成功！");
                return "登录成功";
            }

            if (status.Code == 800)
            {
                await tsClient.DeleteAvatar();
                return "二维码已过期，请重试";
            }
        }

        await tsClient.DeleteAvatar();
        return "登录超时，请重试";
    }

    [Command("yun api")]
    public string CommandSetApi(string address)
    {
        _configManager.UpdateApiAddress(address);
        _api.BaseUrl = address;
        return $"API地址已更新为：{address}";
    }

    // ========== Internal Methods ==========

    private async Task<string> LoadAndPlayPlaylist(long playlistId, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        // Get playlist detail (name, cover)
        var detail = await _api.GetPlaylistDetailAsync(playlistId);
        var playlist = detail.Playlist;
        if (playlist == null) return "无法获取歌单信息";

        if (playlist.CoverImgUrl != null)
            await MainCommands.CommandBotAvatarSet(ts3Client, playlist.CoverImgUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, playlist.Name);

        // Load all track IDs
        _songQueue.Clear();
        var tracks = await _api.GetPlaylistTracksAsync(playlistId);
        if (tracks.Songs == null || tracks.Songs.Count == 0)
            return "歌单为空";

        foreach (var song in tracks.Songs)
        {
            if (song.Id > 0)
                _songQueue.Add(song.Id);
        }

        await ts3Client.SendChannelMessage($"已加载歌单「{playlist.Name}」共 {_songQueue.Count} 首");

        // Play first song
        return await PlayFromQueue(playManager, invoker, ts3Client);
    }

    private async Task<string> PlayFromQueue(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        if (_songQueue.Count == 0) return "播放列表为空";

        var isRandom = CurrentMode is PlayMode.Random or PlayMode.RandomLoop;
        _currentIndex = isRandom ? _random.Next(_songQueue.Count) : 0;

        var songId = _songQueue[_currentIndex];
        _songQueue.RemoveAt(_currentIndex);

        // For loop modes, re-add to end
        if (CurrentMode is PlayMode.SequentialLoop or PlayMode.RandomLoop)
            _songQueue.Add(songId);

        var url = await _api.GetMusicUrlAsync(songId);
        if (string.IsNullOrEmpty(url))
        {
            // Skip unplayable songs
            if (_songQueue.Count > 0)
                return await PlayFromQueue(playManager, invoker, ts3Client);
            return "无法获取歌曲链接";
        }

        var detail = await _api.GetSongDetailAsync(songId);
        var name = detail?.Name ?? songId.ToString();

        await MainCommands.CommandPlay(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"正在播放：{name}");
        return "开始播放歌单";
    }

    private async Task OnSongEnd(object? sender, EventArgs e)
    {
        await _lock.WaitAsync();
        try
        {
            if (_songQueue.Count == 0 || _playManager == null || _invoker == null || _ts3Client == null)
                return;

            await PlayFromQueue(_playManager, _invoker, _ts3Client);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Error playing next song: {ex.Message}");
        }
        finally
        {
            _lock.Release();
        }
    }
}
