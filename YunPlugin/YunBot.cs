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
using TSLib;
using TSLib.Full;
using TSLib.Full.Book;
using TSLib.Messages;

namespace YunPlugin
{
    public class YunPlgun : IBotPlugin /* or ICorePlugin */
    {
        private static YunPlgun Instance;
        private static IniFile MyIni;
        private static NLog.Logger Log = NLog.LogManager.GetLogger($"TS3AudioBot.Plugins.{typeof(YunPlgun).Namespace}");
        private static string neteaseApi;

        public static NLog.Logger GetLogger()
        {
            return Log;
        }

        private PlayManager playManager;
        private Ts3Client ts3Client;
        private Connection serverView;
        private PlayControl playControl;
        private SemaphoreSlim slimlock = new SemaphoreSlim(1, 1);

        TsFullClient TS3FullClient { get; set; }
        public Player PlayerConnection { get; set; }

        private static ulong ownChannelID;
        private static List<ulong> ownChannelClients = new List<ulong>();

        public YunPlgun(PlayManager playManager, Ts3Client ts3Client, Connection serverView)
        {
            Instance = this;
            this.playManager = playManager;
            this.ts3Client = ts3Client;
            this.serverView = serverView;
        }

        public void Initialize()
        {
            playControl = new PlayControl(playManager, ts3Client, Log);
            loadConfig(playControl);

            playManager.AfterResourceStarted += PlayManager_AfterResourceStarted;
            playManager.PlaybackStopped += PlayManager_PlaybackStopped;

            TS3FullClient.OnEachClientLeftView += OnEachClientLeftView;
            TS3FullClient.OnEachClientEnterView += OnEachClientEnterView;
            TS3FullClient.OnEachClientMoved += OnEachClientMoved;

            _ = updateOwnChannel();

            ts3Client.SendChannelMessage("网易云音乐插件加载成功！");
        }

        private void loadConfig(PlayControl playControl)
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

            playControl.SetMode(playMode);
            playControl.SetCookies(cookies);
            playControl.SetNeteaseApi(neteaseApi);

            Log.Info("Yun Plugin loaded");
            Log.Info($"Play mode: {playMode}");
            Log.Info($"Cookie: {cookies}");
            Log.Info($"Api address: {neteaseApi}");
        }

        private async Task updateOwnChannel(ulong channelID = 0)
        {
            if (channelID < 1) channelID = (await TS3FullClient.WhoAmI()).Value.ChannelId.Value;
            ownChannelID = channelID;
            ownChannelClients.Clear();
            R<ClientList[], CommandError> r = await TS3FullClient.ClientList(ClientListOptions.uid);
            if (!r)
            {
                throw new Exception($"Clientlist failed ({r.Error.ErrorFormat()})");
            }
            foreach (var client in r.Value.ToList())
            {
                if (client.ChannelId.Value == channelID)
                {
                    if (client.ClientId == TS3FullClient.ClientId) continue;
                    ownChannelClients.Add(client.ClientId.Value);
                }
            }
        }

        private void checkOwnChannel()
        {
            if (ownChannelClients.Count < 1)
            {
                PlayerConnection.Paused = true;
            }
            else
            {
                PlayerConnection.Paused = false;
            }
            Log.Info("ownChannelClients: {}", ownChannelClients.Count);
        }

        private async void OnEachClientMoved(object sender, ClientMoved e)
        {
            if (e.ClientId == TS3FullClient.ClientId)
            {
                await updateOwnChannel(e.TargetChannelId.Value);
                return;
            }
            var hasClient = ownChannelClients.Contains(e.ClientId.Value);
            if (e.TargetChannelId.Value == ownChannelID)
            {
                if (!hasClient) ownChannelClients.Add(e.ClientId.Value);
                checkOwnChannel();
            }
            else if (hasClient)
            {
                ownChannelClients.Remove(e.ClientId.Value);
                checkOwnChannel();
            }
        }

        private void OnEachClientEnterView(object sender, ClientEnterView e)
        {
            if (e.ClientId == TS3FullClient.ClientId) return;
            if (e.TargetChannelId.Value == ownChannelID) ownChannelClients.Add(e.ClientId.Value);
            checkOwnChannel();
        }
        private void OnEachClientLeftView(object sender, ClientLeftView e)
        {
            if (e.ClientId == TS3FullClient.ClientId) return;
            if (e.SourceChannelId.Value == ownChannelID) ownChannelClients.Remove(e.ClientId.Value);
            checkOwnChannel();
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

        [Command("yun gedan")]
        public async Task<string> CommandGedan(string name, Ts3Client ts3Client)
        {
            try
            {
                string listId = Utils.ExtractIdFromAddress(name);
                if (!Utils.IsNumber(listId))
                {
                    string urlSearch = $"{neteaseApi}/search?keywords={name}&limit=1&type=1000";
                    SearchGedan searchgedan = await Utils.HttpGetAsync<SearchGedan>(urlSearch);
                    if (searchgedan.result.playlists.Length == 0)
                    {
                        return "未找到歌单";
                    }
                    listId = searchgedan.result.playlists[0].id.ToString();
                }

                var gedanDetail = await GetPlayListDetail(listId, playControl.GetCookies());
                string gedanname = gedanDetail.playlist.name;
                string imgurl = gedanDetail.playlist.coverImgUrl;
                await ts3Client.ChangeDescription(gedanname);
                await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
                await ts3Client.SendChannelMessage("开始添加歌单");

                List<MusicInfo> songList = new List<MusicInfo>();
                await genList(listId, playControl.GetCookies(), songList, ts3Client);
                await ts3Client.SendChannelMessage("歌单添加完毕：" + gedanname + " [" + songList.Count.ToString() + "]");
                playControl.SetPlayList(new PlayListMeta(listId, gedanname, imgurl), songList);
                await playControl.PlayNextMusic();
            }
            catch (Exception e)
            {
                Log.Error(e, "playgedan error");
                return "播放歌单失败";
            }

            return "开始播放歌单";
        }

        [Command("yun play")]
        public async Task<string> CommandYunPlay(string arguments)
        {
            string songid = Utils.ExtractIdFromAddress(arguments);
            if (!Utils.IsNumber(songid))
            {
                string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
                yunSearchSong yunSearchSong = await Utils.HttpGetAsync<yunSearchSong>(urlSearch);
                if (yunSearchSong.result.songs.Length == 0)
                {
                    return "未找到歌曲";
                }
                songid = yunSearchSong.result.songs[0].id.ToString();
            }
            var music = new MusicInfo(songid, false);
            playControl.AddMusic(music, false);
            await playControl.PlayMusic(music);
            return null;
        }

        [Command("yun add")]
        public async Task<string> CommandYunAdd(string arguments)
        {
            string songid = Utils.ExtractIdFromAddress(arguments);
            if (!Utils.IsNumber(songid))
            {
                string urlSearch = $"{neteaseApi}/search?keywords={arguments}&limit=1";
                yunSearchSong yunSearchSong = await Utils.HttpGetAsync<yunSearchSong>(urlSearch);
                if (yunSearchSong.result.songs.Length == 0)
                {
                    return "未找到歌曲";
                }
                songid = yunSearchSong.result.songs[0].id.ToString();
            }
            playControl.AddMusic(new MusicInfo(songid));
            return "已添加到下一首播放";
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
            string result = $"\n网易云API: {neteaseApi}\n当前用户: ";

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

        public async Task<GedanDetail> GetPlayListDetail(string id, string cookie)
        {
            return await Utils.HttpGetAsync<GedanDetail>($"{neteaseApi}/playlist/detail?id={id}&timestamp={Utils.GetTimeStamp()}", cookie);
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

        public async Task genList(string id, string cookie, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            GedanDetail gedanDetail = await GetPlayListDetail(id, cookie);
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount != 0)
            {
                await genListTrack(id, cookie, gedanDetail, SongList, ts3Client);
                return;
            }
            GeDan Gedans = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={id}", cookie);
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

        public async Task genListTrack(string id, string cookie, GedanDetail gedanDetail, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{trackCount}]");
            }
            for (int i = 0; i < trackCount; i += 50)
            {
                GeDan geDan = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={id}&limit=50&offset={i}", cookie);

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
            playManager.AfterResourceStarted -= PlayManager_AfterResourceStarted;
            playManager.PlaybackStopped -= PlayManager_PlaybackStopped;
            TS3FullClient.OnEachClientLeftView -= OnEachClientLeftView;
            TS3FullClient.OnEachClientEnterView -= OnEachClientEnterView;
            TS3FullClient.OnEachClientMoved -= OnEachClientMoved;
        }
    }
}