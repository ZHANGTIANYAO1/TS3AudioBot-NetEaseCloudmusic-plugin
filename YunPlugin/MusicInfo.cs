using System;
using System.Text.Json;
using NeteaseApiData;
using System.Collections.Generic;
using System.Threading.Tasks;
using TS3AudioBot.ResourceFactories;
using System.Text;
using System.Net;
using System.IO;

public enum Mode
{
    SeqPlay = 0,
    SeqLoopPlay = 1,
    RandomPlay = 2,
    RandomLoopPlay = 3,
}

public class MusicInfo
{
    public string id = "";
    public string name = "";
    public string img = "";
    public string author = "";
    public string detailUrl = "";

    public MusicInfo(string id)
    {
        this.id = id;
    }

    public AudioResource GetMusicInfo()
    {
        var ar = new AudioResource(detailUrl, name, "media")
                    .Add("PlayUri", img);
        return ar;
    }

    public async Task<byte[]> GetImage()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(img);
        request.Method = "GET";

        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (Stream stream = response.GetResponseStream())
        using (MemoryStream memoryStream = new MemoryStream())
        {
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public async Task InitMusicInfo(string api)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(img))
        {
            return;
        }
        try
        {
            string musicdetailurl = api + "/song/detail?ids=" + id;
            string musicdetailjson = await Utils.HttpGetAsync(musicdetailurl);
            MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
            img = musicDetail.songs[0].al.picUrl;
            name = musicDetail.songs[0].name;
            detailUrl = $"https://music.163.com/#/song?id={id}";

            if (musicDetail.songs[0].ar != null) {
                List<string> artists = new List<string>();
                for (int i = 0; i < musicDetail.songs[0].ar.Count; i++) {
                    if (!string.IsNullOrEmpty(musicDetail.songs[0].ar[i].name))
                    {
                        artists.Add(musicDetail.songs[0].ar[i].name);
                    }
                }
                if (artists.Count > 0)
                {
                    author = string.Join("/", artists);
                }
            }
        }
        catch (Exception e)
        {
            name = "歌名获取失败!\n" + e.Message;
        }
    }

    private async Task<MusicCheck> CheckMusic(string api, string id)
    {
        string musicCheckUrl = $"{api}/check/music?id={id}";
        string searchMusicCheckJson = await Utils.HttpGetAsync(musicCheckUrl);
        return JsonSerializer.Deserialize<MusicCheck>(searchMusicCheckJson);
    }

    // 获得歌曲URL
    public async Task<string> getMusicUrl(string api, string apiUNM, string cookie = "")
    {
        string api_url = $"{api}/song/url?id={id}";
        MusicCheck check = await CheckMusic(api, id);
        if (!check.success)
        {
            YunPlugin.YunPlgun.GetLogger().Warn($"Get music error: {check.message}");
            api_url += $"&proxy={apiUNM}";
        }
        else if (cookie != "")
        {
            api_url += $"&cookie={cookie}";
        }
        string musicurljson = await  Utils.HttpGetAsync(api_url);
        musicURL musicurl = JsonSerializer.Deserialize<musicURL>(musicurljson);
        string mp3 = musicurl.data[0].url;
        if (!check.success)
        {
            mp3 = mp3
                .Replace("http://music.163.com", apiUNM)
                .Replace("https://music.163.com", apiUNM);
        }
        return mp3;
    }
}