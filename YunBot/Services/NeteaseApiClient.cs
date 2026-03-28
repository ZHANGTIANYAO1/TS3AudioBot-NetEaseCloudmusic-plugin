using System.Text.Json;
using YunBot.Models;

namespace YunBot.Services;

public class NeteaseApiClient : IDisposable
{
    private const int SearchTypeAlbum = 10;
    private const int SearchTypePlaylist = 1000;

    private readonly HttpClient _http;
    private readonly PluginConfig _config;

    public NeteaseApiClient(PluginConfig config)
    {
        _config = config;
        _http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
    }

    private static string Timestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

    private string CookieParam =>
        !string.IsNullOrEmpty(_config.Cookie) ? $"&cookie={Uri.EscapeDataString(_config.Cookie)}" : "";

    private async Task<T> GetAsync<T>(string path)
    {
        var url = $"{_config.ApiAddress.TrimEnd('/')}{path}";
        var json = await _http.GetStringAsync(url);
        return JsonSerializer.Deserialize<T>(json)
               ?? throw new InvalidOperationException($"Failed to deserialize response from {path}");
    }

    public async Task<byte[]> DownloadBytesAsync(string url) => await _http.GetByteArrayAsync(url);

    public async Task<SearchSongResponse> SearchSongAsync(string keywords, int limit = 1)
    {
        return await GetAsync<SearchSongResponse>(
            $"/search?keywords={Uri.EscapeDataString(keywords)}&limit={limit}");
    }

    public async Task<SearchPlaylistResponse> SearchPlaylistAsync(string keywords, int limit = 1)
    {
        return await GetAsync<SearchPlaylistResponse>(
            $"/search?keywords={Uri.EscapeDataString(keywords)}&limit={limit}&type={SearchTypePlaylist}");
    }

    public async Task<SearchAlbumResponse> SearchAlbumAsync(string keywords, int limit = 1)
    {
        return await GetAsync<SearchAlbumResponse>(
            $"/search?keywords={Uri.EscapeDataString(keywords)}&limit={limit}&type={SearchTypeAlbum}");
    }

    /// <summary>Requires login.</summary>
    public async Task<PersonalFmResponse> GetPersonalFmAsync()
    {
        return await GetAsync<PersonalFmResponse>($"/personal_fm?timestamp={Timestamp}{CookieParam}");
    }

    public async Task<AlbumResponse> GetAlbumAsync(long albumId)
    {
        return await GetAsync<AlbumResponse>($"/album?id={albumId}");
    }

    /// <summary>
    /// Gets the music URL. Tries official API first, then UnblockNeteaseMusic if enabled.
    /// </summary>
    public async Task<string?> GetMusicUrlAsync(long songId)
    {
        var url = await GetOfficialMusicUrlAsync(songId);
        if (!string.IsNullOrEmpty(url))
            return url;

        if (_config.UnblockerEnabled)
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
            var resp = await GetAsync<MusicUrlResponse>(
                $"/song/url/v1?id={songId}&level=exhigh&timestamp={Timestamp}{CookieParam}");
            return resp.Data?.FirstOrDefault()?.Url;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] Official API error for song {songId}: {ex.Message}");
            return null;
        }
    }

    private async Task<string?> GetUnblockedMusicUrlAsync(long songId)
    {
        var unblockerBase = _config.UnblockerAddress.TrimEnd('/');

        try
        {
            var json = await _http.GetStringAsync($"{unblockerBase}/song/url?id={songId}");
            var resp = JsonSerializer.Deserialize<MusicUrlResponse>(json);
            var url = resp?.Data?.FirstOrDefault()?.Url;
            if (!string.IsNullOrEmpty(url))
                return url;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[YunBot] UnblockNeteaseMusic error for song {songId}: {ex.Message}");
        }

        try
        {
            var json = await _http.GetStringAsync($"{unblockerBase}/match?id={songId}");
            var resp = JsonSerializer.Deserialize<UnblockMatchResponse>(json);
            if (!string.IsNullOrEmpty(resp?.Url))
                return resp.Url;
        }
        catch { }

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
