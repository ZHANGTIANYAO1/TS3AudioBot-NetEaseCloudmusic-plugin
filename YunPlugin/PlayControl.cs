using System.Collections.Generic;
using System.Linq;
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
    private int randomOffset = 0;
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

    public void AddMusic(MusicInfo musicInfo)
    {
        songList.RemoveAll(m => m.Id == musicInfo.Id);
        songList.Insert(0, musicInfo);
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
        var invoker = Getinvoker();

        currentPlayMusicInfo = musicInfo;

        await musicInfo.InitMusicInfo(neteaseApi, cookies);
        string musicUrl = await musicInfo.getMusicUrl(neteaseApi, neteaseApiUNM, cookies);
        Log.Info($"Music name: {musicInfo.Name}, picUrl: {musicInfo.Image}, url: {musicUrl}");

        if (musicUrl == "error")
        {
            await ts3Client.SendChannelMessage($"音乐链接获取失败 [{musicInfo.Name}]");
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

    public List<MusicInfo> GetNextPlayList(int limit = 3)
    {
        var list = new List<MusicInfo>();
        if (songList.Count <= limit)
        {
            limit = songList.Count;
        }
        for (int i = 0; i < limit; i++)
        {
            list.Add(songList[i]);
        }
        return list;
    }

    private MusicInfo GetNextMusic()
    {
        MusicInfo result;
        switch (mode)
        {
            case Mode.SeqPlay:
                result = songList[0];
                songList.RemoveAt(0);
                break;
            case Mode.SeqLoopPlay:
                result = songList[0];
                songList.RemoveAt(0);
                songList.Add(result);
                break;

            case Mode.RandomPlay: // 随机播放
            case Mode.RandomLoopPlay:
                if (randomOffset < 0)
                {
                    randomOffset = songList.Count - 1;
                    Utils.ShuffleArrayList(songList);
                    currentPlay = 0;
                }
                randomOffset -= 1;

                result = songList[0];
                songList.RemoveAt(0);
                if (mode == Mode.RandomLoopPlay)
                {
                    songList.Add(result);
                }
                break;

            default:
                Log.Error($"Mode is not found! {mode}");
                result = songList[0];
                songList.RemoveAt(0);
                break;
        }
        currentPlay += 1;
        return result;
    }

    public async Task<string> GetPlayListString()
    {
        var musicList = GetNextPlayList();
        var musicInfo = GetCurrentPlayMusicInfo();
        var descBuilder = new StringBuilder();
        descBuilder.AppendLine($"\n当前正在播放：{musicInfo.GetFullNameBBCode()}");
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
