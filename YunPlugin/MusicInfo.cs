using NeteaseApiData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TS3AudioBot.ResourceFactories;

public enum Mode
{
    SeqPlay = 0,
    SeqLoopPlay = 1,
    RandomPlay = 2,
    RandomLoopPlay = 3,
}

public class PlayListMeta
{
    public string Id;
    public string Name;
    public string Image;

    public PlayListMeta(string id, string name, string image)
    {
        Id = id;
        Name = name;
        Image = image;
    }
}

public class MusicInfo
{
    public string Id = "";
    public string Name = "";
    public string Image = "";
    public string DetailUrl = "";
    public bool InPlayList;
    private Dictionary<string, int?> Author = new Dictionary<string, int?>();

    public MusicInfo(string id, bool inPlayList = true)
    {
        this.Id = id;
        InPlayList = inPlayList;
    }

    public string GetAuthor()
    {
        return string.Join(" / ", Author.Keys);
    }

    public string GetFullName()
    {
        var author = GetAuthor();
        author = !string.IsNullOrEmpty(author) ? $" - {author}" : "";
        return Name + author;
    }

    public string GetFullNameBBCode()
    {
        var author = GetAuthorBBCode();
        author = !string.IsNullOrEmpty(author) ? $" - {author}" : "";
        return $"[URL={DetailUrl}]{Name}[/URL]{author}";
    }

    public string GetAuthorBBCode()
    {
        return string.Join(" / ", Author.Select(entry =>
        {
            string key = entry.Key;
            int? id = entry.Value;
            string authorName = id == null ? key : $"[URL=https://music.163.com/#/artist?id={id}]{key}[/URL]";
            return authorName;
        }));
    }

    public AudioResource GetMusicInfo()
    {
        var ar = new AudioResource(DetailUrl, GetFullName(), "media")
                    .Add("PlayUri", Image);
        return ar;
    }

    public async Task<byte[]> GetImage()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Image);
        request.Method = "GET";

        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (Stream stream = response.GetResponseStream())
        using (MemoryStream memoryStream = new MemoryStream())
        {
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public async Task InitMusicInfo(string api, string cookie)
    {
        if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Image))
        {
            return;
        }
        try
        {
            string musicdetailurl = $"{api}/song/detail?ids={Id}&t={Utils.GetTimeStamp()}";
            JsonSongDetail musicDetail = await Utils.HttpGetAsync<JsonSongDetail>(musicdetailurl, cookie);
            Image = musicDetail.songs[0].al.picUrl;
            Name = musicDetail.songs[0].name;
            DetailUrl = $"https://music.163.com/#/song?id={Id}";

            Author.Clear();

            var artists = musicDetail.songs[0].ar;
            if (artists != null)
            {
                foreach (var artist in artists)
                {
                    if (!string.IsNullOrEmpty(artist.name))
                    {
                        Author.Add(artist.name, artist.id);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Name = "歌名获取失败!\n" + e.Message;
        }
    }

    // 获得歌曲URL
    public async Task<string> getMusicUrl(string api, string cookie = "")
    {
        string api_url = $"{api}/song/url?id={Id}&t={Utils.GetTimeStamp()}";

        try
        {
            MusicURL musicurl = await Utils.HttpGetAsync<MusicURL>(api_url, cookie);
            return musicurl.data[0].url;
        }
        catch (Exception e)
        {
            YunPlugin.YunPlgun.GetLogger().Error(e, $"Get music url error: {api_url}");
            return $"error: {e.Message}";
        }
    }
}