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
    private const int MaxSkipRetries = 5; // Prevent infinite recursion when skipping failed songs

    private ConfigManager _configManager = null!;
    private NeteaseApiClient _api = null!;
    private readonly List<long> _songQueue = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly Random _random = new();
    private int _currentIndex;
    private bool _eventSubscribed;
    private long _currentSongId;
    private string _currentSongName = "";
    private int _skipCount;

    // Saved references for playback continuation
    private PlayManager? _playManager;
    private InvokerData? _invoker;
    private Ts3Client? _ts3Client;

    public void Initialize()
    {
        _configManager = new ConfigManager(ConfigPath);
        _api = new NeteaseApiClient(
            _configManager.Config.ApiAddress,
            _configManager.Config.Cookie,
            _configManager.Config.UnblockerEnabled,
            _configManager.Config.UnblockerAddress);
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

    /// <summary>
    /// Sets bot avatar from a URL with automatic image compression to fit TS3 limits.
    /// </summary>
    private async Task SetBotAvatarSafe(Ts3Client ts3Client, TsFullClient? tsFullClient, string imageUrl)
    {
        try
        {
            using var processed = await ImageProcessor.DownloadAndProcessAvatarAsync(_api.Http, imageUrl);
            if (tsFullClient != null)
            {
                await tsFullClient.UploadAvatar(processed);
            }
            else
            {
                // Fallback to MainCommands which has its own resize logic
                await MainCommands.CommandBotAvatarSet(ts3Client, imageUrl);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Avatar upload failed, trying fallback: {ex.Message}");
            try
            {
                await MainCommands.CommandBotAvatarSet(ts3Client, imageUrl);
            }
            catch (Exception ex2)
            {
                Console.WriteLine($"[YunBot] Avatar fallback also failed: {ex2.Message}");
            }
        }
    }

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
            await SetBotAvatarSafe(ts3Client, null, detail.Album.PicUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, song.Name);

        _currentSongId = song.Id;
        _currentSongName = song.Name;

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
            await SetBotAvatarSafe(ts3Client, null, detail.Album.PicUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, name);

        _currentSongId = id;
        _currentSongName = name;

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
        if (playManager.IsPlaying)
            await playManager.Stop();
        _currentSongId = 0;
        _currentSongName = "";
        return "已停止播放并清空队列";
    }

    [Command("yun fm")]
    public async Task<string> CommandFm(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        try
        {
            var fm = await _api.GetPersonalFmAsync();
            if (fm.Data == null || fm.Data.Count == 0)
                return "无法获取私人FM，请确认已登录";

            _songQueue.Clear();
            // Add all FM songs to queue
            foreach (var song in fm.Data)
                _songQueue.Add(song.Id);

            // Play first one
            _skipCount = 0;
            var firstSong = fm.Data[0];
            var url = await _api.GetMusicUrlAsync(firstSong.Id);
            if (string.IsNullOrEmpty(url))
                return "无法获取FM歌曲链接";

            _currentSongId = firstSong.Id;
            _currentSongName = firstSong.Name;
            _songQueue.RemoveAt(0);

            if (firstSong.Album?.PicUrl != null)
                await SetBotAvatarSafe(ts3Client, null, firstSong.Album.PicUrl);
            await MainCommands.CommandBotDescriptionSet(ts3Client, $"私人FM: {firstSong.Name}");

            await MainCommands.CommandPlay(playManager, invoker, url);
            await ts3Client.SendChannelMessage($"私人FM：{firstSong.Name}");
            return $"私人FM：{firstSong.Name}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] FM error: {ex.Message}");
            return "私人FM获取失败，请确认已登录账户";
        }
    }

    [Command("yun album")]
    public async Task<string> CommandAlbum(string name, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        try
        {
            var search = await _api.SearchAlbumAsync(name);
            var album = search.Result?.Albums?.FirstOrDefault();
            if (album == null) return "未找到专辑";

            return await LoadAndPlayAlbum(album.Id, playManager, invoker, ts3Client);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Album search error: {ex.Message}");
            return "搜索专辑失败";
        }
    }

    [Command("yun albumid")]
    public async Task<string> CommandAlbumId(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        EnsureEventSubscribed(player);
        SaveContext(playManager, invoker, ts3Client);

        return await LoadAndPlayAlbum(id, playManager, invoker, ts3Client);
    }

    [Command("yun status")]
    public string CommandStatus(PlayManager playManager)
    {
        var status = new
        {
            isPlaying = playManager.IsPlaying,
            currentSongId = _currentSongId,
            currentSongName = _currentSongName,
            queueCount = _songQueue.Count,
            playMode = _configManager.Config.PlayMode,
            apiAddress = _configManager.Config.ApiAddress,
            unblockerEnabled = _configManager.Config.UnblockerEnabled,
            unblockerAddress = _configManager.Config.UnblockerAddress
        };
        return JsonSerializer.Serialize(status);
    }

    [Command("yun login")]
    public async Task<string> CommandLogin(Ts3Client ts3Client, TsFullClient tsClient)
    {
        var key = await _api.GetQrKeyAsync();
        var qrImgBase64 = await _api.GetQrImageAsync(key);

        await ts3Client.SendChannelMessage("正在生成登录二维码...");

        // Process and upload QR code as bot avatar (with compression)
        using var processed = ImageProcessor.ProcessBase64Avatar(qrImgBase64);
        await tsClient.UploadAvatar(processed);
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

    [Command("yun unblocker")]
    public string CommandUnblocker(string toggle, string? address = null)
    {
        var enabled = toggle.ToLowerInvariant() is "on" or "true" or "1" or "enable";
        _configManager.UpdateUnblocker(enabled, address);
        _api.UnblockerEnabled = enabled;
        if (address != null)
            _api.UnblockerUrl = address;

        if (enabled)
            return $"UnblockNeteaseMusic 已启用，地址：{_api.UnblockerUrl}";
        return "UnblockNeteaseMusic 已禁用";
    }

    // ========== Internal Methods ==========

    private async Task<string> LoadAndPlayAlbum(long albumId, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        var albumResp = await _api.GetAlbumAsync(albumId);
        if (albumResp.Songs == null || albumResp.Songs.Count == 0)
            return "专辑为空或无法获取";

        var albumInfo = albumResp.Album;
        if (albumInfo?.PicUrl != null)
            await SetBotAvatarSafe(ts3Client, null, albumInfo.PicUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, albumInfo?.Name ?? "专辑");

        _songQueue.Clear();
        foreach (var song in albumResp.Songs)
        {
            if (song.Id > 0)
                _songQueue.Add(song.Id);
        }

        await ts3Client.SendChannelMessage($"已加载专辑「{albumInfo?.Name}」共 {_songQueue.Count} 首");

        _skipCount = 0;
        return await PlayFromQueue(playManager, invoker, ts3Client);
    }

    private async Task<string> LoadAndPlayPlaylist(long playlistId, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        var detail = await _api.GetPlaylistDetailAsync(playlistId);
        var playlist = detail.Playlist;
        if (playlist == null) return "无法获取歌单信息";

        if (playlist.CoverImgUrl != null)
            await SetBotAvatarSafe(ts3Client, null, playlist.CoverImgUrl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, playlist.Name);

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

        _skipCount = 0;
        return await PlayFromQueue(playManager, invoker, ts3Client);
    }

    private async Task<string> PlayFromQueue(PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        if (_songQueue.Count == 0) return "播放列表为空";

        var isRandom = CurrentMode is PlayMode.Random or PlayMode.RandomLoop;
        _currentIndex = isRandom ? _random.Next(_songQueue.Count) : 0;

        var songId = _songQueue[_currentIndex];
        _songQueue.RemoveAt(_currentIndex);

        // Try to get the music URL BEFORE re-adding to queue for loop modes
        var url = await _api.GetMusicUrlAsync(songId);
        if (string.IsNullOrEmpty(url))
        {
            // Song failed - do NOT re-add to queue even in loop mode
            Console.WriteLine($"[YunBot] Skipping song {songId}: no URL available");
            _skipCount++;

            if (_skipCount >= MaxSkipRetries || _songQueue.Count == 0)
            {
                _skipCount = 0;
                return "连续多首歌曲无法播放，已停止";
            }

            return await PlayFromQueue(playManager, invoker, ts3Client);
        }

        // URL is valid - now re-add for loop modes
        if (CurrentMode is PlayMode.SequentialLoop or PlayMode.RandomLoop)
            _songQueue.Add(songId);

        _skipCount = 0;
        _currentSongId = songId;

        var detail = await _api.GetSongDetailAsync(songId);
        _currentSongName = detail?.Name ?? songId.ToString();

        await MainCommands.CommandPlay(playManager, invoker, url);
        await ts3Client.SendChannelMessage($"正在播放：{_currentSongName}");
        return "开始播放歌单";
    }

    private async Task OnSongEnd(object? sender, EventArgs e)
    {
        await _lock.WaitAsync();
        try
        {
            if (_songQueue.Count == 0 || _playManager == null || _invoker == null || _ts3Client == null)
                return;

            _skipCount = 0;
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
