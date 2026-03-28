using System.Text.Json.Serialization;

namespace YunBot.Models;

// ===== Search Song =====

public class SearchSongResponse
{
    [JsonPropertyName("result")]
    public SearchResult? Result { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class SearchResult
{
    [JsonPropertyName("songs")]
    public List<SearchSongItem>? Songs { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("songCount")]
    public int SongCount { get; set; }
}

public class SearchSongItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("artists")]
    public List<ArtistInfo>? Artists { get; set; }

    [JsonPropertyName("album")]
    public AlbumInfo? Album { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }
}

// ===== Shared Models =====

public class ArtistInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}

public class AlbumInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("picUrl")]
    public string? PicUrl { get; set; }
}

// ===== Music URL =====

public class MusicUrlResponse
{
    [JsonPropertyName("data")]
    public List<MusicUrlData>? Data { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class MusicUrlData
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("br")]
    public long Br { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

// ===== Song Detail =====

public class SongDetailResponse
{
    [JsonPropertyName("songs")]
    public List<SongDetailItem>? Songs { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class SongDetailItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("ar")]
    public List<ArtistInfo>? Artists { get; set; }

    [JsonPropertyName("al")]
    public AlbumInfo? Album { get; set; }

    [JsonPropertyName("dt")]
    public int Duration { get; set; }
}

// ===== Playlist Search =====

public class SearchPlaylistResponse
{
    [JsonPropertyName("result")]
    public PlaylistSearchResult? Result { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class PlaylistSearchResult
{
    [JsonPropertyName("playlists")]
    public List<PlaylistItem>? Playlists { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("playlistCount")]
    public int PlaylistCount { get; set; }
}

public class PlaylistItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("coverImgUrl")]
    public string? CoverImgUrl { get; set; }

    [JsonPropertyName("trackCount")]
    public int TrackCount { get; set; }

    [JsonPropertyName("playCount")]
    public long PlayCount { get; set; }
}

// ===== Playlist Detail =====

public class PlaylistDetailResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("playlist")]
    public PlaylistDetail? Playlist { get; set; }
}

public class PlaylistDetail
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("coverImgUrl")]
    public string? CoverImgUrl { get; set; }

    [JsonPropertyName("trackCount")]
    public int TrackCount { get; set; }

    [JsonPropertyName("trackIds")]
    public List<TrackIdItem>? TrackIds { get; set; }
}

public class TrackIdItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}

// ===== Playlist Tracks =====

public class PlaylistTracksResponse
{
    [JsonPropertyName("songs")]
    public List<SongDetailItem>? Songs { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

// ===== QR Login =====

public class QrKeyResponse
{
    [JsonPropertyName("data")]
    public QrKeyData? Data { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class QrKeyData
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("unikey")]
    public string Unikey { get; set; } = "";
}

public class QrImageResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("data")]
    public QrImageData? Data { get; set; }
}

public class QrImageData
{
    [JsonPropertyName("qrurl")]
    public string? QrUrl { get; set; }

    [JsonPropertyName("qrimg")]
    public string? QrImg { get; set; }
}

public class QrCheckResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("cookie")]
    public string? Cookie { get; set; }
}

// ===== UnblockNeteaseMusic =====

public class UnblockMatchResponse
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("br")]
    public int Br { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}
