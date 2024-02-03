using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.CommandSystem;
using TS3AudioBot.Playlists;
using TS3AudioBot.Plugins;
using TS3AudioBot.ResourceFactories;
using TSLib.Full;
using NeteaseApiData;
using TSLib.Full.Book;

namespace YunPlugin
{
    public class YunPlgun : IBotPlugin /* or ICorePlugin */
    {
        private static YunPlgun Instance;
        private static IniFile MyIni;
        private static NLog.Logger Log = NLog.LogManager.GetLogger($"TS3AudioBot.Plugins.{typeof(YunPlgun).Namespace}");
        private static string neteaseApi;
        private static string neteaseApiUNM;

        public static NLog.Logger GetLogger()
        {
            return Log;
        }

        private PlayManager playManager;
        private Ts3Client ts3Client;
        private Connection serverView;
        private PlayControl playControl;
        private SemaphoreSlim slimlock = new SemaphoreSlim(1, 1);

        public YunPlgun(PlayManager playManager, Ts3Client ts3Client, Connection serverView) {
            Instance = this;
            this.playManager = playManager;
            this.ts3Client = ts3Client;
            this.serverView = serverView;
        }

        public void Initialize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string dockerEnvFilePath = "/.dockerenv";

                if (System.IO.File.Exists(dockerEnvFilePath))
                {
                    Log.Info("运行在Docker环境.");
                }
                else
                {
                    Log.Info("运行在Linux环境.");
                }

                string location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                MyIni = new IniFile(System.IO.File.Exists(dockerEnvFilePath) ? location + "/data/plugins/YunSettings.ini" : location + "/plugins/YunSettings.ini");
            }
            else
            {
                MyIni = new IniFile("plugins/YunSettings.ini");
            }
            

            var cookies = MyIni.Read("cookies1");
            Mode playMode;
            try
            {
                playMode = (Mode) int.Parse(MyIni.Read("playMode", "0"));
            }
            catch (Exception e) {
                Log.Warn($"Get play mode error!{e}");
                playMode = Mode.SeqPlay;
            }

            neteaseApi = MyIni.Read("WangYiYunAPI_Address", "https://127.0.0.1:3000");
            neteaseApiUNM = MyIni.Read("UNM_Address", "https://127.0.0.1:3001");

            Log.Info("Yun Plugin loaded");
            Log.Info($"Play mode: {playMode}");
            Log.Info($"Cookie: {cookies}");
            Log.Info($"Api address: {neteaseApi}");

            playControl = new PlayControl(playManager, ts3Client, Log, neteaseApi, neteaseApiUNM);
            playControl.SetMode(playMode);
            playControl.SetCookies(cookies);

            playManager.PlaybackStopped += AudioService_PlaybackStopped;

            ts3Client.SendChannelMessage("Yun bot loaded success!");
        }

        public async Task AudioService_PlaybackStopped(object sender, EventArgs e) //当上一首音乐播放完触发
        {
            await slimlock.WaitAsync();
            try
            {
                Log.Debug("上一首歌结束");
                await playControl.PlayNextMusic();
            }
            finally
            {
                slimlock.Release();
            }
        }

        [Command("yun mode")]
        public Task<string> PlayMode(int mode)
        {
            if (Enum.IsDefined(typeof(Mode), mode))
            {
                Mode playMode = (Mode)mode;
                playControl.SetMode(playMode);
                MyIni.Write("playMode", mode.ToString());

                return Task.FromResult(playMode switch
                {
                    Mode.SeqPlay => "当前播放模式为顺序播放",
                    Mode.SeqLoopPlay => "当前播放模式为顺序循环",
                    Mode.RandomPlay => "当前播放模式为随机播放",
                    Mode.RandomLoopPlay => "当前播放模式为随机循环",
                    _ => "请输入正确的播放模式",
                });
            }
            else
            {
                return Task.FromResult("请输入正确的播放模式");
            }
        }

        [Command("yun gedanid")]
        public async Task<string> playgedan(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);
            string strid = id.ToString();

            var gedanDetail = await GetPlayListDetail(strid);
            string gedanname = gedanDetail.playlist.name;
            string imgurl = gedanDetail.playlist.coverImgUrl;
            await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
            await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
            await ts3Client.SendChannelMessage("开始添加歌单");

            List<MusicInfo> songList = new List<MusicInfo>();
            await genList(id, songList, ts3Client);
            await ts3Client.SendChannelMessage("歌单添加完毕：" + gedanname + " [" + songList.Count.ToString() + "]");
            playControl.SetPlayList(songList);
            await playControl.PlayNextMusic();

            return "开始播放歌单";
        }

        [Command("yun gedan")]
        public async Task<string> CommandGedan(string name, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);

            string urlSearch = $"{neteaseApi}/search?keywords={name}&limit=1&type=1000";
            string json = await Utils.HttpGetAsync(urlSearch);
            SearchGedan searchgedan = JsonSerializer.Deserialize<SearchGedan>(json);
            long gedanid = searchgedan.result.playlists[0].id;
            string strid = gedanid.ToString();
            GedanDetail gedanDetail = await GetPlayListDetail(strid);
            string gedanname = gedanDetail.playlist.name;
            string imgurl = gedanDetail.playlist.coverImgUrl;
            await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
            await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
            await ts3Client.SendChannelMessage("开始添加歌单");
            List<MusicInfo> songList = new List<MusicInfo>();
            await genList(gedanid, songList, ts3Client);
            await ts3Client.SendChannelMessage("歌单添加完毕：" + gedanname + " [" + songList.Count.ToString() + "]");
            playControl.SetPlayList(songList);
            await playControl.PlayNextMusic();

            return "开始播放歌单";
        }

        [Command("yun play")]
        public async Task<string> CommandYunPlay(string arguments, InvokerData invoker)
        {
            playControl.SetInvoker(invoker);
            string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
            string Searchjson = await Utils.HttpGetAsync(urlSearch);
            yunSearchSong yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            playControl.AddMusic(new MusicInfo(firstmusicid.ToString()));
            await playControl.PlayNextMusic();
            return "";
        }

        [Command("yun playid")]
        public async Task<string> CommandYunPlayId(long arguments, InvokerData invoker)
        {
            playControl.SetInvoker(invoker);
            playControl.AddMusic(new MusicInfo(arguments.ToString()));
            await playControl.PlayNextMusic();
            return "";
        }

        [Command("yun add")]
        public async Task<string> CommandYunAdd(string arguments, InvokerData invoker)
        {
            playControl.SetInvoker(invoker);
            string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
            string Searchjson = await Utils.HttpGetAsync(urlSearch);
            yunSearchSong yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            playControl.AddMusic(new MusicInfo(firstmusicid.ToString()));
            return "已添加到下一首播放";
        }

        [Command("yun addid")]
        public Task<string> CommandYunAddId(long arguments, InvokerData invoker)
        {
            playControl.SetInvoker(invoker);
            playControl.AddMusic(new MusicInfo(arguments.ToString()));
            return Task.FromResult("已添加到下一首播放");
        }

        [Command("yun next")]
        public async Task<string> CommandYunNext(PlayManager playManager, InvokerData invoker)
        {
            playControl.SetInvoker(invoker);
            var playList = playControl.GetPlayList();
            if (playList.Count == 0)
            {
                return "播放列表为空";
            }
            if (playManager.IsPlaying)
            {
                await playManager.Stop();
            }
            await playControl.PlayNextMusic();
            return "";
        }

        [Command("yun login")]
        public static async Task<string> CommanloginAsync(Ts3Client ts3Client, TsFullClient tsClient)
        {
            string key = await GetLoginKey();
            string qrimg = await GetLoginQRImage(key);
            
            await ts3Client.SendChannelMessage("正在生成二维码");
            await ts3Client.SendChannelMessage(qrimg);
            Log.Debug(qrimg);
            string[] img = qrimg.Split(",");
            byte[] bytes = Convert.FromBase64String(img[1]);
            Stream stream = new MemoryStream(bytes);
            await tsClient.UploadAvatar(stream);
            await ts3Client.ChangeDescription("请用网易云APP扫描二维码登陆");

            int i = 0;
            long code;
            string result;
            string cookies;
            while (true)
            {
                Status1 status = await CheckLoginStatus(key);
                code = status.code;
                cookies = status.cookie;
                i = i + 1;
                Thread.Sleep(1000);
                if (i == 120)
                {
                    result = "登陆失败或者超时";
                    await ts3Client.SendChannelMessage("登陆失败或者超时");
                    break;
                }
                if (code == 803)
                {
                    result = "登陆成功";
                    await ts3Client.SendChannelMessage("登陆成功");
                    break;
                }
            }
            await tsClient.DeleteAvatar();
            ChangeCookies(cookies);
            
            return result;
        }

        [Command("yun list")]
        public async Task<string> PlayList()
        {
            var playList = playControl.GetPlayList();
            if (playList.Count == 0)
            {
                return "播放列表为空";
            }
            return await playControl.GetPlayListString();
        }

        // 以下全是功能性函数
        public static async Task<string> GetLoginKey()
        {
            string url1 = $"{neteaseApi}/login/qr/key?timestamp={Utils.GetTimeStamp()}";
            string json1 = await Utils.HttpGetAsync(url1);
            Log.Debug(json1);
            LoginKey loginKey = JsonSerializer.Deserialize<LoginKey>(json1);
            return loginKey.data.unikey;
        }

        public static async Task<string> GetLoginQRImage(string key)
        {
            string url2 = $"{neteaseApi}/login/qr/create?key={key}&qrimg=true&timestamp={Utils.GetTimeStamp()}";
            string json2 = await Utils.HttpGetAsync(url2);
            LoginImg loginImg = JsonSerializer.Deserialize<LoginImg>(json2);
            return loginImg.data.qrimg;
        }

        public static async Task<Status1> CheckLoginStatus(string key)
        {
            string url3 = $"{neteaseApi}/login/qr/check?key={key}&timestamp={Utils.GetTimeStamp()}";
            string json3 = await Utils.HttpGetAsync(url3);
            Log.Debug(json3);
            return JsonSerializer.Deserialize<Status1>(json3);
        }

        public async Task<GedanDetail> GetPlayListDetail(string id)
        {
            string url = $"{neteaseApi}/playlist/detail?id={id}";
            string json = await Utils.HttpGetAsync(url);
            return JsonSerializer.Deserialize<GedanDetail>(json);
        }

        public static void ChangeCookies(string cookies) //更改cookie
        {
            Instance.playControl.SetCookies(cookies);
            MyIni.Write("cookies1", cookies);
        }

        public async Task genList(long id, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            string gedanid = id.ToString();
            GedanDetail gedanDetail = await GetPlayListDetail(gedanid);
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount != 0)
            {
                await genListTrack(id, gedanDetail, SongList, ts3Client);
                return;
            }
            string url = neteaseApi + "/playlist/track/all?id=" + gedanid;
            string gedanjson = await Utils.HttpGetAsync(url);
            GeDan Gedans = JsonSerializer.Deserialize<GeDan>(gedanjson);
            long numOfSongs = Gedans.songs.Count();
            if (numOfSongs > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{numOfSongs}]");
            }
            for (int i = 0; i < numOfSongs; i++)
            {
                long musicid = Gedans.songs[i].id;
                if (musicid > 0)
                {
                    SongList.Add(new MusicInfo(musicid.ToString()));
                }
            }
        }

        public async Task genListTrack(long id, GedanDetail gedanDetail, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            string gedanid = id.ToString();
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{trackCount}]");
            }
            for (int i = 0; i < trackCount; i += 50)
            {
                string searchJson = await Utils.HttpGetAsync($"{neteaseApi}/playlist/track/all?id={gedanid}&limit=50&offset={i}");
                GeDan geDan = JsonSerializer.Deserialize<GeDan>(searchJson);

                for (int j = 0; j < geDan.songs.Count; j++)
                {
                    SongList.Add(new MusicInfo(geDan.songs[j].id.ToString()));
                }
            }
        }

        public void Dispose()
        {
            Instance = null;
            MyIni = null;
            playControl = null;
        }
    }
}