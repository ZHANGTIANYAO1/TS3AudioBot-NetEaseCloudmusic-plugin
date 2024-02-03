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
        static YunPlgun Instance;
        static IniFile MyIni;
        private static NLog.Logger Log = NLog.LogManager.GetLogger($"TS3AudioBot.Plugins.{typeof(YunPlgun).Namespace}");
        public static bool isEventnotadded = true;
        SemaphoreSlim slimlock = new SemaphoreSlim(1, 1);
        public static string neteaseApi;

        private PlayManager playManager;
        private Ts3Client ts3Client;
        private Connection serverView;
        private PlayControl playControl;

        public YunPlgun(PlayManager playManager, Ts3Client ts3Client, Connection serverView) {
            Instance = this;
            this.playManager = playManager;
            this.ts3Client = ts3Client;
            this.serverView = serverView;
        }

        public void Initialize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                MyIni = new IniFile("plugins/YunSettings.ini");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                MyIni = new IniFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/data/plugins/YunSettings.ini");

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
            Log.Info("Yun Plugin loaded");
            Log.Info($"Play mode: {playMode}");
            Log.Info($"Cookie: {cookies}");
            Log.Info($"Api address: {neteaseApi}");

            playControl = new PlayControl(playManager, ts3Client, Log, neteaseApi);
            playControl.SetMode(playMode);
            playControl.SetCookies(cookies);

            ts3Client.SendChannelMessage("Yun bot loaded success!");
        }

        public async Task AudioService_Playstoped(object sender, EventArgs e) //当上一首音乐播放完触发
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

                switch (playMode)
                {
                    case Mode.SeqPlay:
                        return Task.FromResult("当前播放模式为顺序播放");

                    case Mode.SeqLoopPlay:
                        return Task.FromResult("当前播放模式为顺序循环");

                    case Mode.RandomPlay:
                        return Task.FromResult("当前播放模式为随机播放");

                    case Mode.RandomLoopPlay:
                        return Task.FromResult("当前播放模式为随机循环");

                    default:
                        return Task.FromResult("请输入正确的播放模式");
                }
            }
            else
            {
                return Task.FromResult("请输入正确的播放模式");
            }
        }

        [Command("yun gedanid")]
        public async Task<string> playgedan(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
        {
            if (isEventnotadded)
            {
                player.OnSongEnd += AudioService_Playstoped;
                Log.Debug("event added");
                isEventnotadded = false;
            }
            playControl.SetInvoker(invoker);
            string strid = id.ToString();
            string url = neteaseApi + "/playlist/detail?id=" + strid;
            string json = Utils.HttpGet(url);
            GedanDetail gedanDetail = JsonSerializer.Deserialize<GedanDetail>(json);
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
        public async Task<string> CommandGedan(string name, PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
        {
            string urlSearch = neteaseApi + "/search?keywords=" + name + "&limit=1&type=1000";
            string json = Utils.HttpGet(urlSearch);
            SearchGedan searchgedan = JsonSerializer.Deserialize<SearchGedan>(json);
            long gedanid = searchgedan.result.playlists[0].id;
            if (isEventnotadded)
            {
                player.OnSongEnd += AudioService_Playstoped;
                Console.WriteLine("event added");
                isEventnotadded = false;
            }
            playControl.SetInvoker(invoker);
            string strid = gedanid.ToString();
            string url = neteaseApi + "/playlist/detail?id=" + strid;
            string jsons = Utils.HttpGet(url);
            GedanDetail gedanDetail = JsonSerializer.Deserialize<GedanDetail>(jsons);
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
        public async Task<string> CommandYunPlay(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);
            string urlSearch = neteaseApi + "/search?keywords=" + arguments + "&limit=1";
            string Searchjson = Utils.HttpGet(urlSearch);
            yunSearchSong yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            playControl.AddMusic(new MusicInfo(firstmusicid.ToString()));
            await playControl.PlayNextMusic();
            return "";
        }

        [Command("yun playid")]
        public async Task<string> CommandYunPlayId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);
            playControl.AddMusic(new MusicInfo(arguments.ToString()));
            await playControl.PlayNextMusic();
            return "";
        }

        [Command("yun add")]
        public Task<string> CommandYunAdd(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);
            string urlSearch = neteaseApi + "/search?keywords=" + arguments + "&limit=1";
            string Searchjson = Utils.HttpGet(urlSearch);
            yunSearchSong yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            playControl.AddMusic(new MusicInfo(firstmusicid.ToString()));
            return Task.FromResult("已添加到下一首播放");
        }

        [Command("yun addid")]
        public Task<string> CommandYunAddId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
        {
            playControl.SetInvoker(invoker);
            playControl.AddMusic(new MusicInfo(arguments.ToString()));
            return Task.FromResult("已添加到下一首播放");
        }

        [Command("yun next")]
        public async Task<string> CommandYunNext(PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
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
            string url1 = neteaseApi + "/login/qr/key" + "?timestamp=" + GetTimeStamp();
            string json1 = Utils.HttpGet(url1);
            Log.Debug(json1);
            LoginKey loginKey = JsonSerializer.Deserialize<LoginKey>(json1);
            string key = loginKey.data.unikey;
            string url2 = neteaseApi + "/login/qr/create?key=" + key + "&qrimg=true&timestamp=" + GetTimeStamp();
            string json2 = Utils.HttpGet(url2);
            LoginImg loginImg = JsonSerializer.Deserialize<LoginImg>(json2);
            string base64String = loginImg.data.qrimg;
            await ts3Client.SendChannelMessage("正在生成二维码");
            await ts3Client.SendChannelMessage(loginImg.data.qrimg);
            Log.Debug(base64String);
            string[] img = base64String.Split(",");
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
                string url3 = neteaseApi + "/login/qr/check?key=" + key + "&timestamp=" + GetTimeStamp();
                string json3 = Utils.HttpGet(url3);
                Log.Debug(json3);
                Status1 status1 = JsonSerializer.Deserialize<Status1>(json3);
                code = status1.code;
                cookies = status1.cookie;
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
            changeCookies(cookies);
            
            return result;
        }

        [Command("yun list")]
        public Task<string> PlayList()
        {
            return Task.FromResult(playControl.GetPlayListString());
        }

        //以下全是功能性函数
        public static void changeCookies(string cookies) //更改cookie
        {
            Instance.playControl.SetCookies(cookies);
            MyIni.Write("cookies1", cookies);
        }

        public static string GetTimeStamp() //获得时间戳
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static async Task genList(long id, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            string gedanid = id.ToString();
            string url = neteaseApi + "/playlist/track/all?id=" + gedanid;
            string gedanjson = Utils.HttpGet(url);
            GeDan Gedans = JsonSerializer.Deserialize<GeDan>(gedanjson);
            long numOfSongs = Gedans.songs.Count();
            if (numOfSongs > 100)
            {
                await ts3Client.SendChannelMessage("警告歌单过大，可能需要一定的时间生成");
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


        public void Dispose()
        {
            Instance = null;
            MyIni = null;
            playControl = null;
        }
    }
}