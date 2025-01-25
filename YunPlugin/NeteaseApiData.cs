using System.Collections.Generic;

#pragma warning disable CS8632
namespace NeteaseApiData
{
    public class ArtistsItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
    }

    public class Artist
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
    }

    public class SongsItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public ArtistsItem[] artists { get; set; }
    }

    public class Result
    {
        public SongsItem[] songs { get; set; }
    }

    public class yunSearchSong
    {
        public Result result { get; set; }
        public long code { get; set; }
    }

    public class DataItem
    {
        public long id { get; set; }
        public string url { get; set; }
    }

    public class MusicURL
    {
        public DataItem[] data { get; set; }
        public long code { get; set; }
    }

    public class ArItem
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class Al
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
    }

    public class SongsItems
    {
        public string name { get; set; }
        public long id { get; set; }
        public ArItem[] ar { get; set; }
    }

    public class Album
    {
        public string company { get; set; }
        public string picUrl { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ZhuanJi
    {
        public bool resourceState { get; set; }
        public SongsItems[] songs { get; set; }
        public long code { get; set; }
        public Album album { get; set; }

    }

    public class GeDan
    {
        public SongsItems[] songs { get; set; }
        public long code { get; set; }
    }

    public class FM
    {
        public FMData[] data { get; set; }
        public long code { get; set; }
    }

    public class FMData
    {
        public string name { get; set; }
        public long id { get; set; }
    }

    public class ArtistsItems
    {
        public string name { get; set; }
        public long id { get; set; }
        public string picUrl { get; set; }
    }

    public class Artists
    {
        public string name { get; set; }
        public long id { get; set; }
        public string picUrl { get; set; }
    }

    public class ArtistsItemss
    {
        public string name { get; set; }
        public long id { get; set; }
        public string picUrl { get; set; }
    }

    public class Track
    {
        public string name { get; set; }
        public long id { get; set; }
        public ArtistsItemss[] artists { get; set; }
    }

    public class PlaylistsItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public string coverImgUrl { get; set; }
        public Track track { get; set; }
    }

    public class Results
    {
        public PlaylistsItem[] playlists { get; set; }
    }

    public class AlbumsItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
    }

    public class ResultsZhuanJi
    {
        public AlbumsItem[] albums { get; set; }
    }

    public class SearchGedan
    {
        public Results result { get; set; }
        public long code { get; set; }
    }

    public class SearchZhuanJi
    {
        public ResultsZhuanJi result { get; set; }
        public long code { get; set; }
    }

    public class Data
    {
        public int code { get; set; }
        public string unikey { get; set; }
    }

    public class LoginKey
    {
        public Data data { get; set; }
        public int code { get; set; }
    }

    public class Datas
    {
        public string qrurl { get; set; }
        public string qrimg { get; set; }
    }

    public class LoginImg
    {
        public int code { get; set; }
        public Datas data { get; set; }
    }

    public class Status1
    {
        public long code { get; set; }
        public string message { get; set; }
        public bool data { get; set; }
        public string cookie { get; set; }
    }

    public class Playlist
    {
        public long id { get; set; }
        public string name { get; set; }
        public string coverImgUrl { get; set; }
        public int trackCount { get; set; }

    }

    public class GedanDetail
    {
        public long code { get; set; }
        public Playlist playlist { get; set; }
    }

    public class ZhuanJiDetail
    {
        public long code { get; set; }
        public Album album { get; set; }
    }

    public class MusicCheck
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class SongDetail
    {
        public string id { get; set; }
        public string program_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string picUrl { get; set; }
    }
    public class JsonSongDetailSongAlbum
    {
        public string? name { get; set; }
        public string? picUrl { get; set; }
    }
    public class JsonSongDetailSongAuthor
    {
        public int? id { get; set; }
        public string? name { get; set; }
    }
    public class JsonSongDetailSong
    {
        public long id { get; set; }
        public string? name { get; set; }
        public object? noCopyrightRcmd { get; set; }
        public JsonSongDetailSongAlbum? al { get; set; }
        public JsonSongDetailSongAuthor[]? ar { get; set; }
    }
    public class JsonSongDetail
    {
        public int code { get; set; }
        public JsonSongDetailSong[]? songs { get; set; }
    }

    public class RespStatus
    {
        public class Account
        {
            public bool anonimousUser { get; set; }
            public int ban { get; set; }
            public int baoyueVersion { get; set; }
            public long createTime { get; set; }
            public int donateVersion { get; set; }
            public long id { get; set; }
            public bool paidFee { get; set; }
            public int status { get; set; }
            public int tokenVersion { get; set; }
            public int type { get; set; }
            public string userName { get; set; }
            public int vipType { get; set; }
            public int whitelistAuthority { get; set; }
        }

        public class Profile
        {
            public int accountStatus { get; set; }
            public int accountType { get; set; }
            public string avatarUrl { get; set; }
            public string nickname { get; set; }
            public int userId { get; set; }
            public int userType { get; set; }

            public int vipType { get; set; }
            public long viptypeVersion { get; set; }
        }
        public class Data
        {
            public int code { get; set; }
            public Account? account { get; set; }
            public Profile? profile { get; set; }
        }
        public Data data { get; set; }
    }
}
