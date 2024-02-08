using NeteaseApiData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.CommandSystem;
using TS3AudioBot.Plugins;
using TSLib.Full;
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

        public YunPlgun(PlayManager playManager, Ts3Client ts3Client, Connection serverView)
        {
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
                playMode = (Mode)int.Parse(MyIni.Read("playMode", "0"));
            }
            catch (Exception e)
            {
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

            playManager.AfterResourceStarted += PlayManager_AfterResourceStarted;
            playManager.PlaybackStopped += PlayManager_PlaybackStopped;

            ts3Client.SendChannelMessage("网易云音乐插件加载成功！");
        }

        private Task PlayManager_AfterResourceStarted(object sender, PlayInfoEventArgs value)
        {
            playControl.SetInvoker(value.Invoker);
            return Task.CompletedTask;
        }

        public async Task PlayManager_PlaybackStopped(object sender, EventArgs e) //当上一首音乐播放完触发
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
        public async Task<string> playgedan(long id, Ts3Client ts3Client)
        {
            try
            {
                string strid = id.ToString();

                var gedanDetail = await GetPlayListDetail(strid);
                string gedanname = gedanDetail.playlist.name;
                string imgurl = gedanDetail.playlist.coverImgUrl;
                await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
                await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
                await ts3Client.SendChannelMessage("开始添加歌单");

                List<MusicInfo> songList = new List<MusicInfo>();
                await genList(id, playControl.GetCookies(), songList, ts3Client);
                await ts3Client.SendChannelMessage("歌单添加完毕：" + gedanname + " [" + songList.Count.ToString() + "]");
                playControl.SetPlayList(new PlayListMeta(strid, gedanname, imgurl), songList);
                await playControl.PlayNextMusic();
            } catch (Exception e)
            {
                Log.Error(e, "playgedan error");
                return "播放歌单失败";
            }

            return "开始播放歌单";
        }

        [Command("yun gedan")]
        public async Task<string> CommandGedan(string name, Ts3Client ts3Client)
        {
            string urlSearch = $"{neteaseApi}/search?keywords={name}&limit=1&type=1000";
            SearchGedan searchgedan = await Utils.HttpGetAsync<SearchGedan>(urlSearch);
            long gedanid = searchgedan.result.playlists[0].id;
            string strid = gedanid.ToString();
            GedanDetail gedanDetail = await GetPlayListDetail(strid);
            string gedanname = gedanDetail.playlist.name;
            string imgurl = gedanDetail.playlist.coverImgUrl;
            await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
            await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
            await ts3Client.SendChannelMessage("开始添加歌单");
            List<MusicInfo> songList = new List<MusicInfo>();
            await genList(gedanid, playControl.GetCookies(), songList, ts3Client);
            await ts3Client.SendChannelMessage("歌单添加完毕：" + gedanname + " [" + songList.Count.ToString() + "]");
            playControl.SetPlayList(new PlayListMeta(strid, gedanname, imgurl), songList);
            await playControl.PlayNextMusic();

            return "开始播放歌单";
        }

        [Command("yun play")]
        public async Task<string> CommandYunPlay(string arguments)
        {
            string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
            yunSearchSong yunSearchSong = await Utils.HttpGetAsync<yunSearchSong>(urlSearch);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            var music = new MusicInfo(firstmusicid.ToString(), false);
            playControl.AddMusic(music, false);
            await playControl.PlayMusic(music);
            return null;
        }

        [Command("yun playid")]
        public async Task<string> CommandYunPlayId(long arguments)
        {
            var music = new MusicInfo(arguments.ToString(), false);
            playControl.AddMusic(music, false);
            await playControl.PlayMusic(music);
            return null;
        }

        [Command("yun add")]
        public async Task<string> CommandYunAdd(string arguments)
        {
            string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
            yunSearchSong yunSearchSong = await Utils.HttpGetAsync<yunSearchSong>(urlSearch);
            long firstmusicid = yunSearchSong.result.songs[0].id;
            playControl.AddMusic(new MusicInfo(firstmusicid.ToString()));
            return "已添加到下一首播放";
        }

        [Command("yun addid")]
        public Task<string> CommandYunAddId(long arguments)
        {
            playControl.AddMusic(new MusicInfo(arguments.ToString()));
            return Task.FromResult("已添加到下一首播放");
        }

        [Command("yun next")]
        public async Task<string> CommandYunNext(PlayManager playManager)
        {
            var playList = playControl.GetPlayList();
            if (playList.Count == 0)
            {
                return "播放列表为空";
            }
            if (playManager.IsPlaying)
            {
                await playManager.Stop();
            }
            return null;
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

        [Command("yun status")]
        public async Task<string> CommandNcmStatusAsync()
        {
            string result = $"\n网易云API: {neteaseApi}\nUNM-API: {neteaseApiUNM}\n当前用户: ";

            if (string.IsNullOrEmpty(playControl.GetCookies()))
            {
                result += $"未登入";
                return result;
            }

            var status = await GetLoginStatusAasync(neteaseApi, playControl.GetCookies());
            if (status.data.code == 200 && status.data.account.status == 0)
            {
                result += $"[URL=https://music.163.com/#/user/home?id={status.data.profile.userId}]{status.data.profile.nickname}[/URL]\n";
            }
            else
            {
                result += $"未登入";
            }

            return result;
        }

        // 以下全是功能性函数
        public static async Task<string> GetLoginKey()
        {
            LoginKey loginKey = await Utils.HttpGetAsync<LoginKey>($"{neteaseApi}/login/qr/key?timestamp={Utils.GetTimeStamp()}");
            return loginKey.data.unikey;
        }

        public static async Task<string> GetLoginQRImage(string key)
        {
            LoginImg loginImg = await Utils.HttpGetAsync<LoginImg>($"{neteaseApi}/login/qr/create?key={key}&qrimg=true&timestamp={Utils.GetTimeStamp()}");
            return loginImg.data.qrimg;
        }

        public static async Task<Status1> CheckLoginStatus(string key)
        {
            return await Utils.HttpGetAsync<Status1>($"{neteaseApi}/login/qr/check?key={key}&timestamp={Utils.GetTimeStamp()}");
        }

        public async Task<GedanDetail> GetPlayListDetail(string id)
        {
            return await Utils.HttpGetAsync<GedanDetail>($"{neteaseApi}/playlist/detail?id={id}");
        }

        public static void ChangeCookies(string cookies) //更改cookie
        {
            Instance.playControl.SetCookies(cookies);
            MyIni.Write("cookies1", cookies);
        }

        public static async Task<RespStatus> GetLoginStatusAasync(string server, string cookie)
        {
            return await Utils.HttpGetAsync<RespStatus>($"{server}/login/status?timestamp={Utils.GetTimeStamp()}", cookie);
        }

        public async Task genList(long id, string cookie, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            string gedanid = id.ToString();
            GedanDetail gedanDetail = await GetPlayListDetail(gedanid);
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount != 0)
            {
                await genListTrack(id, cookie, gedanDetail, SongList, ts3Client);
                return;
            }
            GeDan Gedans = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={gedanid}", cookie);
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

        public async Task genListTrack(long id, string cookie, GedanDetail gedanDetail, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            string gedanid = id.ToString();
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{trackCount}]");
            }
            for (int i = 0; i < trackCount; i += 50)
            {
                GeDan geDan = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={gedanid}&limit=50&offset={i}", cookie);

                for (int j = 0; j < geDan.songs.Length; j++)
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