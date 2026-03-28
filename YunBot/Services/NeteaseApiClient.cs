using System.Text.Json;
using YunBot.Models;

namespace YunBot.Services;

public class NeteaseApiClient : IDisposable
{
    private readonly HttpClient _http;
    private string _baseUrl;
    private string _cookie;
    private string _unblockerUrl;
    private bool _unblockerEnabled;

    public NeteaseApiClient(string baseUrl, string cookie = "", bool unblockerEnabled = false, string unblockerUrl = "")
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _cookie = cookie;
        _unblockerEnabled = unblockerEnabled;
        _unblockerUrl = unblockerUrl.TrimEnd('/');
        _http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
    }

    public HttpClient Http => _http;

    public string BaseUrl
    {
        get => _baseUrl;
        set => _baseUrl = value.TrimEnd('/');
    }

    public string Cookie
    {
        get => _cookie;
        set => _cookie = value;
    }

    public bool UnblockerEnabled
    {
        get => _unblockerEnabled;
        set => _unblockerEnabled = value;
    }

    public string UnblockerUrl
    {
        get => _unblockerUrl;
        set => _unblockerUrl = value.TrimEnd('/');
    }

    private static string Timestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

    private async Task<T> GetAsync<T>(string path)
    {
        var url = $"{_baseUrl}{path}";
        var json = await _http.GetStringAsync(url);
        return JsonSerializer.Deserialize<T>(json)
               ?? throw new InvalidOperationException($"Failed to deserialize response from {path}");
    }

    public async Task<SearchSongResponse> SearchSongAsync(string keywords, int limit = 1)
    {
        return await GetAsync<SearchSongResponse>(
            $"/search?keywords={Uri.EscapeDataString(keywords)}&limit={limit}");
    }

    public async Task<SearchPlaylistResponse> SearchPlaylistAsync(string keywords, int limit = 1)
    {
        return await GetAsync<SearchPlaylistResponse>(
            $"/search?keywords={Uri.EscapeDataString(keywords)}&limit={limit}&type=1000");
    }

    /// <summary>
    /// Gets the music URL. First tries the official API, then falls back to UnblockNeteaseMusic if enabled.
    /// </summary>
    public async Task<string?> GetMusicUrlAsync(long songId)
    {
        // Try official API first
        var url = await GetOfficialMusicUrlAsync(songId);
        if (!string.IsNullOrEmpty(url))
            return url;

        // Fallback to UnblockNeteaseMusic
        if (_unblockerEnabled)
        {
            var unblockedUrl = await GetUnblockedMusicUrlAsync(songId);
            if (!string.IsNullOrEmpty(unblockedUrl))
            {
                Console.WriteLine($"[YunBot] Song {songId} resolved via UnblockNeteaseMusic");
                return unblockedUrl;
            }
        }

        return null;
    }

    private async Task<string?> GetOfficialMusicUrlAsync(long songId)
    {
        try
        {
            var cookiePart = !string.IsNullOrEmpty(_cookie) ? $"&cookie={Uri.EscapeDataString(_cookie)}" : "";
            var resp = await GetAsync<MusicUrlResponse>($"/song/url?id={songId}{cookiePart}");
            var data = resp.Data?.FirstOrDefault();
            // code 200 means success, url being null means no permission
            return data?.Url;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Official API error for song {songId}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Tries to resolve music URL through UnblockNeteaseMusic proxy.
    /// UnblockNeteaseMusic typically proxies the NeteaseCloudMusicApi and provides
    /// alternative sources for blocked/copyrighted songs.
    /// Supports both the standard NeteaseCloudMusicApi format and the UNM server format.
    /// </summary>
    private async Task<string?> GetUnblockedMusicUrlAsync(long songId)
    {
        try
        {
            // Try standard NeteaseCloudMusicApi-compatible endpoint
            var url = $"{_unblockerUrl}/song/url?id={songId}";
            var json = await _http.GetStringAsync(url);
            var resp = JsonSerializer.Deserialize<MusicUrlResponse>(json);
            var data = resp?.Data?.FirstOrDefault();
            if (!string.IsNullOrEmpty(data?.Url))
                return data.Url;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] UnblockNeteaseMusic error for song {songId}: {ex.Message}");
        }

        try
        {
            // Try UNM server /match endpoint (UnblockNeteaseMusic-Server format)
            var url = $"{_unblockerUrl}/match?id={songId}";
            var json = await _http.GetStringAsync(url);
            var resp = JsonSerializer.Deserialize<UnblockMatchResponse>(json);
            if (!string.IsNullOrEmpty(resp?.Url))
                return resp.Url;
        }
        catch
        {
            // Silently ignore - this format may not be supported
        }

        return null;
    }

    public async Task<SongDetailItem?> GetSongDetailAsync(long songId)
    {
        var resp = await GetAsync<SongDetailResponse>($"/song/detail?ids={songId}");
        return resp.Songs?.FirstOrDefault();
    }

    public async Task<PlaylistDetailResponse> GetPlaylistDetailAsync(long playlistId)
    {
        return await GetAsync<PlaylistDetailResponse>($"/playlist/detail?id={playlistId}");
    }

    public async Task<List<long>> GetPlaylistTrackIdsAsync(long playlistId)
    {
        var detail = await GetPlaylistDetailAsync(playlistId);
        return detail.Playlist?.TrackIds?.Select(t => t.Id).ToList() ?? new List<long>();
    }

    public async Task<PlaylistTracksResponse> GetPlaylistTracksAsync(long playlistId)
    {
        return await GetAsync<PlaylistTracksResponse>($"/playlist/track/all?id={playlistId}");
    }

    public async Task<string> GetQrKeyAsync()
    {
        var resp = await GetAsync<QrKeyResponse>($"/login/qr/key?timestamp={Timestamp}");
        return resp.Data?.Unikey ?? throw new InvalidOperationException("Failed to get QR key");
    }

    public async Task<string> GetQrImageAsync(string key)
    {
        var resp = await GetAsync<QrImageResponse>(
            $"/login/qr/create?key={Uri.EscapeDataString(key)}&qrimg=true&timestamp={Timestamp}");
        return resp.Data?.QrImg ?? throw new InvalidOperationException("Failed to get QR image");
    }

    public async Task<QrCheckResponse> CheckQrStatusAsync(string key)
    {
        return await GetAsync<QrCheckResponse>(
            $"/login/qr/check?key={Uri.EscapeDataString(key)}&timestamp={Timestamp}");
    }

    public void Dispose()
    {
        _http.Dispose();
    }
}
