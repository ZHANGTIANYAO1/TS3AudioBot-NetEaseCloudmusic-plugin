using System.Text.Json;
using YunBot.Models;

namespace YunBot.Services;

public class NeteaseApiClient : IDisposable
{
    private readonly HttpClient _http;
    private string _baseUrl;
    private string _cookie;

    public NeteaseApiClient(string baseUrl, string cookie = "")
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _cookie = cookie;
        _http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
    }

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

    public async Task<string?> GetMusicUrlAsync(long songId)
    {
        var cookiePart = !string.IsNullOrEmpty(_cookie) ? $"&cookie={Uri.EscapeDataString(_cookie)}" : "";
        var resp = await GetAsync<MusicUrlResponse>($"/song/url?id={songId}{cookiePart}");
        return resp.Data?.FirstOrDefault()?.Url;
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
