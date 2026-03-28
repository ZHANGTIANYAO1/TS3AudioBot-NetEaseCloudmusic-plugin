using System.Text.Json;
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
    private const int MaxSkipRetries = 5;
    private const int QrLoginSuccess = 803;
    private const int QrCodeExpired = 800;

    private ConfigManager _configManager = null!;
    private NeteaseApiClient _api = null!;
    private readonly List<long> _songQueue = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly Random _random = new();
    private bool _eventSubscribed;
    private long _currentSongId;
    private string _currentSongName = "";
    private int _skipCount;

    private PlayManager? _playManager;
    private InvokerData? _invoker;
    private Ts3Client? _ts3Client;

    public void Initialize()
    {
        _configManager = new ConfigManager(ConfigPath);
        _api = new NeteaseApiClient(_configManager.Config);
        Console.WriteLine("[YunBot] Plugin loaded");
        Console.WriteLine($"[YunBot] PlayMode={_configManager.Config.PlayMode}, API={_configManager.Config.ApiAddress}");
        if (_configManager.Config.UnblockerEnabled)
            Console.WriteLine($"[YunBot] UnblockNeteaseMusic enabled: {_configManager.Config.UnblockerAddress}");
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

    private async Task SetAvatarSafe(Ts3Client ts3Client, string imageUrl)
    {
        try
        {
            var bytes = await _api.DownloadBytesAsync(imageUrl);
            using var processed = ImageProcessor.ProcessAvatar(bytes);
            await MainCommands.CommandBotAvatarSet(ts3Client, imageUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Avatar upload failed: {ex.Message}");
        }
    }

    /// <summary>Resolves a song by ID: gets URL, sets avatar/description, plays it.</summary>
    private async Task<string> PlaySongByIdAsync(long id, PlayManager pm, InvokerData inv, Ts3Client ts3)
    {
        var (urlTask, detailTask) = (_api.GetMusicUrlAsync(id), _api.GetSongDetailAsync(id));
        var url = await urlTask;
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接，可能需要登录VIP账户";

        var detail = await detailTask;
        var name = detail?.Name ?? id.ToString();

        _currentSongId = id;
        _currentSongName = name;

        var avatarTask = detail?.Album?.PicUrl != null
            ? SetAvatarSafe(ts3, detail.Album.PicUrl)
            : Task.CompletedTask;
        var descTask = MainCommands.CommandBotDescriptionSet(ts3, name);
        await Task.WhenAll(avatarTask, descTask);

        await MainCommands.CommandPlay(pm, inv, url);
        await ts3.SendChannelMessage($"正在播放：{name}");
        return $"正在播放：{name}";
    }

    /// <summary>Resolves a song by ID and adds to playlist.</summary>
    private async Task<string> AddSongByIdAsync(long id, PlayManager pm, InvokerData inv, Ts3Client ts3)
    {
        var (urlTask, detailTask) = (_api.GetMusicUrlAsync(id), _api.GetSongDetailAsync(id));
        var url = await urlTask;
        if (string.IsNullOrEmpty(url)) return "无法获取歌曲链接";

        var detail = await detailTask;
        var name = detail?.Name ?? id.ToString();

        await MainCommands.CommandAdd(pm, inv, url);
        await ts3.SendChannelMessage($"已添加到播放列表：{name}");
        return $"已添加到播放列表：{name}";
    }

    [Command("yun play")]
    public async Task<string> CommandPlay(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);
        var search = await _api.SearchSongAsync(arguments);
        var song = search.Result?.Songs?.FirstOrDefault();
        if (song == null) return "未找到歌曲";
        return await PlaySongByIdAsync(song.Id, playManager, invoker, ts3Client);
    }

    [Command("yun playid")]
    public async Task<string> CommandPlayId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);
        return await PlaySongByIdAsync(id, playManager, invoker, ts3Client);
    }

    [Command("yun add")]
    public async Task<string> CommandAdd(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);
        var search = await _api.SearchSongAsync(arguments);
        var song = search.Result?.Songs?.FirstOrDefault();
        if (song == null) return "未找到歌曲";
        return await AddSongByIdAsync(song.Id, playManager, invoker, ts3Client);
    }

    [Command("yun addid")]
    public async Task<string> CommandAddId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);
        return await AddSongByIdAsync(id, playManager, invoker, ts3Client);
    }

    [Command("yun gedanid")]
    public async Task<string> CommandPlaylistById(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        var (detailTask, tracksTask) = (_api.GetPlaylistDetailAsync(id), _api.GetPlaylistTracksAsync(id));
        var detail = await detailTask;
        var playlist = detail.Playlist;
        if (playlist == null) return "无法获取歌单信息";

        var tracks = await tracksTask;
        var songIds = tracks.Songs?.Where(s => s.Id > 0).Select(s => s.Id).ToList() ?? new();
        if (songIds.Count == 0) return "歌单为空";

        return await LoadAndPlayCollection(songIds, playlist.Name, playlist.CoverImgUrl, playManager, invoker, ts3Client);
    }

    [Command("yun gedan")]
    public async Task<string> CommandPlaylistByName(string name, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        var search = await _api.SearchPlaylistAsync(name);
        var playlist = search.Result?.Playlists?.FirstOrDefault();
        if (playlist == null) return "未找到歌单";

        return await CommandPlaylistById(playlist.Id, playManager, invoker, ts3Client, player);
    }

    [Command("yun album")]
    public async Task<string> CommandAlbum(string name, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        var search = await _api.SearchAlbumAsync(name);
        var album = search.Result?.Albums?.FirstOrDefault();
        if (album == null) return "未找到专辑";

        return await CommandAlbumId(album.Id, playManager, invoker, ts3Client, player);
    }

    [Command("yun albumid")]
    public async Task<string> CommandAlbumId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        var albumResp = await _api.GetAlbumAsync(id);
        if (albumResp.Songs == null || albumResp.Songs.Count == 0) return "专辑为空或无法获取";

        var songIds = albumResp.Songs.Where(s => s.Id > 0).Select(s => s.Id).ToList();
        var info = albumResp.Album;
        return await LoadAndPlayCollection(songIds, info?.Name ?? "专辑", info?.PicUrl, playManager, invoker, ts3Client);
    }

    [Command("yun fm")]
    public async Task<string> CommandFm(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        try
        {
            var fm = await _api.GetPersonalFmAsync();
            if (fm.Data == null || fm.Data.Count == 0) return "无法获取私人FM，请确认已登录";

            var songIds = fm.Data.Select(s => s.Id).ToList();
            var first = fm.Data[0];
            return await LoadAndPlayCollection(songIds, $"私人FM: {first.Name}", first.Album?.PicUrl, playManager, invoker, ts3Client);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] FM error: {ex.Message}");
            return "私人FM获取失败，请确认已登录账户";
        }
    }

    [Command("yun next")]
    public async Task<string> CommandNext(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        SaveContext(playManager, invoker, ts3Client);
        if (!playManager.IsPlaying || _songQueue.Count == 0) return "播放列表为空，无法播放下一首";
        await playManager.Stop();
        return "切换下一首";
    }

    [Command("yun pause")]
    public string CommandPause(Player player)
    {
        player.Paused = !player.Paused;
        return player.Paused ? "已暂停" : "已继续播放";
    }

    [Command("yun stop")]
    public async Task<string> CommandStop(PlayManager playManager)
    {
        _songQueue.Clear();
        if (playManager.IsPlaying) await playManager.Stop();
        _currentSongId = 0;
        _currentSongName = "";
        return "已停止播放并清空队列";
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
    public string CommandClear() { _songQueue.Clear(); return "播放列表已清空"; }

    [Command("yun status")]
    public string CommandStatus(PlayManager playManager) => JsonSerializer.Serialize(new
    {
        isPlaying = playManager.IsPlaying,
        currentSongId = _currentSongId,
        currentSongName = _currentSongName,
        queueCount = _songQueue.Count,
        playMode = _configManager.Config.PlayMode,
        apiAddress = _configManager.Config.ApiAddress,
        unblockerEnabled = _configManager.Config.UnblockerEnabled,
        unblockerAddress = _configManager.Config.UnblockerAddress
    });

    [Command("yun login")]
    public async Task<string> CommandLogin(Ts3Client ts3Client, TsFullClient tsClient)
    {
        var key = await _api.GetQrKeyAsync();
        var qrImgBase64 = await _api.GetQrImageAsync(key);

        await ts3Client.SendChannelMessage("正在生成登录二维码...");
        using var processed = ImageProcessor.ProcessBase64Avatar(qrImgBase64);
        await tsClient.UploadAvatar(processed);
        await ts3Client.ChangeDescription("请用网易云APP扫描机器人头像二维码登录");

        for (var i = 0; i < 120; i++)
        {
            await Task.Delay(1000);
            var status = await _api.CheckQrStatusAsync(key);

            if (status.Code == QrLoginSuccess)
            {
                _configManager.UpdateCookie(status.Cookie ?? "");
                await tsClient.DeleteAvatar();
                await ts3Client.SendChannelMessage("登录成功！");
                return "登录成功";
            }

            if (status.Code == QrCodeExpired)
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
        return $"API地址已更新为：{address}";
    }

    [Command("yun unblocker")]
    public string CommandUnblocker(string toggle, string? address = null)
    {
        var enabled = toggle.ToLowerInvariant() is "on" or "true" or "1" or "enable";
        _configManager.UpdateUnblocker(enabled, address);
        return enabled ? $"UnblockNeteaseMusic 已启用，地址：{_configManager.Config.UnblockerAddress}" : "UnblockNeteaseMusic 已禁用";
    }

    private async Task<string> LoadAndPlayCollection(List<long> songIds, string name, string? coverUrl,
        PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        _songQueue.Clear();
        _songQueue.AddRange(songIds);

        var avatarTask = coverUrl != null ? SetAvatarSafe(ts3Client, coverUrl) : Task.CompletedTask;
        var descTask = MainCommands.CommandBotDescriptionSet(ts3Client, name);
        await Task.WhenAll(avatarTask, descTask);

        await ts3Client.SendChannelMessage($"已加载「{name}」共 {_songQueue.Count} 首");
        _skipCount = 0;
        return await PlayFromQueue();
    }

    private async Task<string> PlayFromQueue()
    {
        var pm = _playManager!;
        var inv = _invoker!;
        var ts3 = _ts3Client!;

        while (_songQueue.Count > 0 && _skipCount < MaxSkipRetries)
        {
            var isRandom = CurrentMode is PlayMode.Random or PlayMode.RandomLoop;
            var idx = isRandom ? _random.Next(_songQueue.Count) : 0;
            var songId = _songQueue[idx];

            // Swap with last and remove (O(1) for random, avoids O(n) shift)
            _songQueue[idx] = _songQueue[^1];
            _songQueue.RemoveAt(_songQueue.Count - 1);

            var (urlTask, detailTask) = (_api.GetMusicUrlAsync(songId), _api.GetSongDetailAsync(songId));
            var url = await urlTask;

            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine($"[YunBot] Skipping song {songId}: no URL available");
                _skipCount++;
                continue;
            }

            // URL valid — re-add for loop modes
            if (CurrentMode is PlayMode.SequentialLoop or PlayMode.RandomLoop)
                _songQueue.Add(songId);

            _skipCount = 0;
            _currentSongId = songId;

            var detail = await detailTask;
            _currentSongName = detail?.Name ?? songId.ToString();

            await MainCommands.CommandPlay(pm, inv, url);
            await ts3.SendChannelMessage($"正在播放：{_currentSongName}");
            return "开始播放";
        }

        _skipCount = 0;
        return _songQueue.Count == 0 ? "播放列表为空" : "连续多首歌曲无法播放，已停止";
    }

    private async Task OnSongEnd(object? sender, EventArgs e)
    {
        await _lock.WaitAsync();
        try
        {
            if (_songQueue.Count == 0 || _playManager == null || _invoker == null || _ts3Client == null)
                return;
            _skipCount = 0;
            await PlayFromQueue();
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
