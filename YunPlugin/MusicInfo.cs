using System;
using System.Text.Json;
using NeteaseApiData;
using System.Collections.Generic;

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

    public MusicInfo(string id)
    {
        this.id = id;
    }

    public void initMusicInfo(string api)
    {
        if (name != "" && img != "")
        {
            return;
        }
        try
        {
            string musicdetailurl = api + "/song/detail?ids=" + id;
            string musicdetailjson = Utils.HttpGet(musicdetailurl);
            MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
            img = musicDetail.songs[0].al.picUrl;
            name = musicDetail.songs[0].name;

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
                    name += " - ";
                    name += string.Join("/", artists);
                }
            }
        }
        catch (Exception e)
        {
            name = "歌名获取失败!\n" + e.Message;
        }
    }

    public string getMusicUrl(string api, string cookie = "")//获得歌曲URL
    {
        string api_url = api + "/song/url?id=" + id;
        if (cookie != "")
        {
            api_url += "&cookie=" + cookie;
        }
        string musicurljson = Utils.HttpGet(api_url);
        musicURL musicurl = JsonSerializer.Deserialize<musicURL>(musicurljson);
        string mp3 = musicurl.data[0].url;
        return mp3;
    }
}