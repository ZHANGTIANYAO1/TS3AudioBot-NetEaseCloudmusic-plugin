using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.ResourceFactories;

public class PlayControl
{
    private PlayManager playManager;
    private InvokerData invoker;
    private Ts3Client ts3Client;
    private List<MusicInfo> songList = new List<MusicInfo>();
    private NLog.Logger Log;
    private string neteaseApi;
    private string neteaseApiUNM;
    private Mode mode;
    private int currentPlay = 0;
    private string cookies = "";
    private MusicInfo currentPlayMusicInfo;
    private PlayListMeta playListMeta;

    public PlayControl(PlayManager playManager, Ts3Client ts3Client, NLog.Logger log, string neteaseApi, string neteaseApiUNM)
    {
        Log = log;
        this.neteaseApi = neteaseApi;
        this.playManager = playManager;
        this.ts3Client = ts3Client;
        this.neteaseApiUNM = neteaseApiUNM;
    }

    public MusicInfo GetCurrentPlayMusicInfo()
    {
        return currentPlayMusicInfo;
    }

    public string GetCookies()
    {
        return cookies;
    }

    public void SetCookies(string cookies)
    {
        this.cookies = cookies;
    }

    public InvokerData Getinvoker()
    {
        return this.invoker;
    }

    public void SetInvoker(InvokerData invoker)
    {
        this.invoker = invoker;
    }

    public Mode GetMode()
    {
        return this.mode;
    }

    public void SetMode(Mode mode)
    {
        this.mode = mode;
    }

    public void SetPlayList(PlayListMeta meta, List<MusicInfo> list)
    {
        playListMeta = meta;
        songList = new List<MusicInfo>(list);
        currentPlay = 0;
        if (mode == Mode.RandomPlay || mode == Mode.RandomLoopPlay)
        {
            Utils.ShuffleArrayList(songList);
        }
    }

    public List<MusicInfo> GetPlayList()
    {
        return songList;
    }

    public void AddMusic(MusicInfo musicInfo, bool insert = true)
    {
        songList.RemoveAll(m => m.Id == musicInfo.Id);
        if (insert)
            songList.Insert(0, musicInfo);
        else
            songList.Add(musicInfo);
    }

    public async Task PlayNextMusic()
    {
        if (songList.Count == 0)
        {
            return;
        }
        var musicInfo = GetNextMusic();
        await PlayMusic(musicInfo);
    }

    public async Task PlayMusic(MusicInfo musicInfo)
    {
        try
        {
            var invoker = Getinvoker();

            currentPlayMusicInfo = musicInfo;

            await musicInfo.InitMusicInfo(neteaseApi, cookies);
            string musicUrl = await musicInfo.getMusicUrl(neteaseApi, neteaseApiUNM, cookies);
            Log.Info($"Music name: {musicInfo.Name}, picUrl: {musicInfo.Image}, url: {musicUrl}");

            if (musicUrl.StartsWith("error"))
            {
                await ts3Client.SendChannelMessage($"音乐链接获取失败 [{musicInfo.Name}] {musicUrl}");
                await PlayNextMusic();
                return;
            }

            await playManager.Play(invoker, new MediaPlayResource(musicUrl, musicInfo.GetMusicInfo(), await musicInfo.GetImage(), false));

            await ts3Client.SendChannelMessage($"► 正在播放：{musicInfo.GetFullNameBBCode()}");

            string desc;
            if (musicInfo.InPlayList)
            {
                desc = $"[{currentPlay}/{songList.Count}] {musicInfo.GetFullName()}";
            }
            else
            {
                desc = musicInfo.GetFullName();
            }
            await ts3Client.ChangeDescription(desc);
            await MainCommands.CommandBotAvatarSet(ts3Client, musicInfo.Image);
        }
        catch (Exception e)
        {
            Log.Error(e, "PlayMusic error");
            await ts3Client.SendChannelMessage($"播放音乐失败 [{musicInfo.Name}]");
            await PlayNextMusic();
        }
    }

    public List<MusicInfo> GetNextPlayList(int limit = 3)
    {
        var list = new List<MusicInfo>();
        limit = Math.Min(limit, songList.Count);
        for (int i = 0; i < limit; i++)
        {
            list.Add(songList[i]);
        }
        return list;
    }

    private MusicInfo GetNextMusic()
    {
        MusicInfo result = songList[0];
        songList.RemoveAt(0);
        if (mode == Mode.SeqLoopPlay || mode == Mode.RandomLoopPlay) // 循环的重新加入列表
        {
            songList.Add(result);
            currentPlay += 1;
        }
        else
        {
            currentPlay = 1; // 不是循环播放就固定当前播放第一首
        }

        if (mode == Mode.RandomLoopPlay) // 如果播放计次达到播放列表最大就重新排序
        {
            if (currentPlay >= songList.Count)
            {
                Utils.ShuffleArrayList(songList);
                currentPlay = 1; // 重排了就从头开始
            }
        }

        return result;
    }

    public async Task<string> GetPlayListString()
    {
        var musicList = GetNextPlayList();
        var musicInfo = GetCurrentPlayMusicInfo();
        var descBuilder = new StringBuilder();
        descBuilder.AppendLine($"\n当前正在播放：{musicInfo.GetFullNameBBCode()}");
        var modeStr = mode switch
        {
            Mode.SeqPlay => "顺序播放",
            Mode.SeqLoopPlay => "当顺序循环",
            Mode.RandomPlay => "随机播放",
            Mode.RandomLoopPlay => "随机循环",
            _ => $"未知模式{mode}",
        };
        descBuilder.AppendLine($"当前播放模式：{modeStr}");
        descBuilder.Append("播放列表 ");
        if (playListMeta != null)
        {
            descBuilder.Append($"[URL=https://music.163.com/#/playlist?id={playListMeta.Id}]{playListMeta.Name}[/URL] ");
        }
        descBuilder.AppendLine($"[{currentPlay}/{songList.Count}]");


        for (var i = 0; i < musicList.Count; i++)
        {
            var music = musicList[i];
            await music.InitMusicInfo(neteaseApi, cookies);
            descBuilder.AppendLine($"{i + 1}: {music.GetFullNameBBCode()}");
        }

        return descBuilder.ToString();
    }
}
