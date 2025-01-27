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
        public static Config config;
        private static NLog.Logger Log = NLog.LogManager.GetLogger($"TS3AudioBot.Plugins.{typeof(YunPlgun).Namespace}");
        private static string neteaseApi;
        private static Timer timer;

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

            if (config.autoPause) {
                TS3FullClient.OnEachClientLeftView += OnEachClientLeftView;
                TS3FullClient.OnEachClientEnterView += OnEachClientEnterView;
                TS3FullClient.OnEachClientMoved += OnEachClientMoved;
            }

            _ = updateOwnChannel();

            ts3Client.SendChannelMessage("网易云音乐插件加载成功！");

            // Add the following code to automatically add FM songs if isPrivateFM is true
            if (playControl.GetPrivateFM())
            {
                // Start a new task to handle asynchronous operations
                Task.Run(async () =>
                {
                    // Check if user is logged in
                    if (!await IsUserLoggedIn())
                    {
                        await ts3Client.SendChannelMessage("私人FM需要登录网易云账号");
                        return;
                    }

                    // Clear current playlist
                    playControl.Clear();

                    // Add next FM song
                    await AddNextFMSong();

                    // Start playing
                    await playControl.PlayNextMusic();
                });
            }
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
                config = Config.GetConfig(System.IO.File.Exists(dockerEnvFilePath) ? location + "/data/plugins/YunSettings.yml" : location + "/plugins/YunSettings.yml");
            }
            else
            {
                config = Config.GetConfig("plugins/YunSettings.yml");
            }


            var header = config.Header;
            Mode playMode;
            try
            {
                playMode = (Mode)config.playMode;
            }
            catch (Exception e)
            {
                Log.Warn($"Get play mode error!{e}");
                playMode = Mode.SeqPlay;
            }

            bool isPrivateFMMode;
            try
            {
                isPrivateFMMode = (bool)config.isPrivateFMMode;
            }
            catch (Exception e)
            {
                Log.Warn($"Get PrivateFM mode error!{e}");
                isPrivateFMMode = false;
            }

            neteaseApi = config.neteaseApi;

            playControl.SetMode(playMode);
            playControl.SetHeader(header);
            playControl.SetNeteaseApi(neteaseApi);
            playControl.SetPrivateFM(isPrivateFMMode);

            if (timer != null)
            {
                timer.Dispose();
            }

            if (config.cookieUpdateIntervalMin <= 0)
            {
                timer = new Timer(async (e) =>
                {
                    if (!config.isQrlogin && header.ContainsKey("Cookie") && !string.IsNullOrEmpty(header["Cookie"]))
                    {
                        try
                        {
                            string url = $"{neteaseApi}/login/refresh?t={Utils.GetTimeStamp()}";
                            Status1 status = await Utils.HttpGetAsync<Status1>(url, header);
                            if (status.code == 200)
                            {
                                var newCookie = Utils.MergeCookie(header["Cookie"], status.cookie);
                                ChangeCookies(newCookie, false);
                                Log.Info("Cookie update success");
                            }
                            else
                            {
                                Log.Warn("Cookie update failed");
                            }
                        } catch (Exception ex)
                        {
                            Log.Error(ex, "Cookie update error");
                        }
                    }
                }, null, TimeSpan.Zero.Milliseconds, TimeSpan.FromMinutes(config.cookieUpdateIntervalMin).Milliseconds);
            }

            Log.Info("Yun Plugin loaded");
            Log.Info($"Play mode: {playMode}");
            for (int i = 0; i < header.Count; i++)
            {
                Log.Info($"Header: {header.Keys.ElementAt(i)}: {header.Values.ElementAt(i)}");
            }
            Log.Info($"Api address: {neteaseApi}");
            if (config.cookieUpdateIntervalMin <= 0)
            {
                Log.Info("Cookie update disabled");
            }
            else
            {
                Log.Info($"Cookie update interval: {config.cookieUpdateIntervalMin} min");
            }
        }

        private async Task updateOwnChannel(ulong channelID = 0)
        {
            if (channelID < 1) channelID = (await TS3FullClient.WhoAmI()).Value.ChannelId.Value;
            ownChannelID = channelID;
            ownChannelClients.Clear();
            R<ClientList[], CommandError> r = await TS3FullClient.ClientList();
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
            if (!config.autoPause)
            {
                return;
            }
            if (ownChannelClients.Count < 1)
            {
                PlayerConnection.Paused = true;
            }
            else
            {
                PlayerConnection.Paused = false;
            }
            Log.Debug("ownChannelClients: {}", ownChannelClients.Count);
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

        public async Task PlayManager_PlaybackStopped(object sender, EventArgs e)
        {
            await slimlock.WaitAsync();
            try
            {
                Log.Debug("上一首歌结束");

                // Check if current mode is PrivateFM
                if ((playControl.GetPrivateFM()) && (playControl.GetPlayList().Count == 0))
                {
                    // Clear current playlist
                    playControl.Clear();

                    // Get next FM song
                    await AddNextFMSong();

                    // Play the new song
                    await playControl.PlayNextMusic();
                }
                else if (playControl.GetPlayList().Count == 0)
                {
                    await ts3Client.ChangeDescription("当前无正在播放歌曲");
                    return;
                }
                else
                {
                    // Existing logic for other play modes
                    await playControl.PlayNextMusic();
                }
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
                config.playMode = playMode;
                config.Save();

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

        [Command("yun zhuanji")]
        public async Task<string> CommandZhuanJi(string data, Ts3Client ts3Client)
        {
            try
            {
                string[] sp = data.Split(' ');

                for (int i = 0; i < sp.Length; i++)
                {
                    if (sp[i] == "")
                    {
                        sp = sp.Take(i).Concat(sp.Skip(i + 1)).ToArray();
                    }
                }

                if (sp.Length == 0)
                {
                    return "请输入专辑ID或者专辑名";
                }
                else if (sp.Length > 2)
                {
                    return "参数过多";
                }

                string id = sp[0];
                string maxLenght = sp.Length > 1 ? sp[1] : "100";
                if (maxLenght == "max")
                {
                    maxLenght = "-1";
                }
                if (!Utils.IsNumber(maxLenght))
                {
                    return "请输入正确的专辑长度";
                }
                string listId = Utils.ExtractIdFromAddress(id);
                if (!Utils.IsNumber(listId))
                {
                    string urlSearch = $"{neteaseApi}/search?keywords={id}&limit=1&type=10";
                    SearchZhuanJi searchgedan = await Utils.HttpGetAsync<SearchZhuanJi>(urlSearch);
                    if (searchgedan.result.albums.Length == 0)
                    {
                        return "未找到专辑";
                    }
                    listId = searchgedan.result.albums[0].id.ToString();
                }

                var zhuanjiDetail = await GetAlbumDetail(listId, playControl.GetHeader());
                string zhuanjiname = zhuanjiDetail.album.name;
                string imgurl = zhuanjiDetail.album.picUrl;
                await ts3Client.ChangeDescription(zhuanjiname);
                await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
                await ts3Client.SendChannelMessage("开始添加专辑");

                List<MusicInfo> songList = new List<MusicInfo>();
                await genAlbumList(listId, int.Parse(maxLenght), playControl.GetHeader(), songList, ts3Client);
                await ts3Client.SendChannelMessage("专辑添加完毕：" + zhuanjiname + " [" + songList.Count.ToString() + "]");
                playControl.SetPlayList(new PlayListMeta(listId, zhuanjiname, imgurl), songList);
                await playControl.PlayNextMusic();
            }
            catch (Exception e)
            {
                Log.Error(e, "playzhuanji error");
                return "播放专辑失败";
            }

            return "开始播放专辑";
        }


        [Command("yun gedan")]
        public async Task<string> CommandGedan(string data, Ts3Client ts3Client)
        {
            try
            {
                string[] sp = data.Split(' ');

                for (int i = 0; i < sp.Length; i++)
                {
                    if (sp[i] == "")
                    {
                        sp = sp.Take(i).Concat(sp.Skip(i + 1)).ToArray();
                    }
                }

                if (sp.Length == 0)
                {
                    return "请输入歌单ID或者歌单名";
                } else if (sp.Length > 2)
                {
                    return "参数过多";
                }

                string id = sp[0];
                string maxLenght = sp.Length > 1 ? sp[1] : "100";
                if (maxLenght == "max")
                {
                    maxLenght = "-1";
                }
                if (!Utils.IsNumber(maxLenght))
                {
                    return "请输入正确的歌单长度";
                }
                string listId = Utils.ExtractIdFromAddress(id);
                if (!Utils.IsNumber(listId))
                {
                    string urlSearch = $"{neteaseApi}/search?keywords={id}&limit=1&type=1000";
                    SearchGedan searchgedan = await Utils.HttpGetAsync<SearchGedan>(urlSearch);
                    if (searchgedan.result.playlists.Length == 0)
                    {
                        return "未找到歌单";
                    }
                    listId = searchgedan.result.playlists[0].id.ToString();
                }

                var gedanDetail = await GetPlayListDetail(listId, playControl.GetHeader());
                string gedanname = gedanDetail.playlist.name;
                string imgurl = gedanDetail.playlist.coverImgUrl;
                await ts3Client.ChangeDescription(gedanname);
                await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
                await ts3Client.SendChannelMessage("开始添加歌单");

                List<MusicInfo> songList = new List<MusicInfo>();
                await genList(listId, int.Parse(maxLenght), playControl.GetHeader(), songList, ts3Client);
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
            if (config.playMode != Mode.SeqPlay && config.playMode != Mode.RandomPlay)
            {
                // 如不是顺序播放或随机播放，添加到播放列表尾
                playControl.AddMusic(music, false);
            }
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

        [Command("yun reload")]
        public Task<string> CommandYunReload()
        {
            loadConfig(playControl);
            return Task.FromResult("配置已重新加载");
        }

        [Command("yun login")]
        public string CommandYunLogin()
        {
            return "请使用yun login qr 或者 yun login sms [手机号] {验证码}";
        }

        [Command("yun login qr")]
        public static async Task<string> CommanQrloginAsync(Ts3Client ts3Client, TsFullClient tsClient)
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
                i++;
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
            await ts3Client.ChangeDescription("已登陆");
            ChangeCookies(cookies, true);

            return result;
        }

        [Command("yun login sms")]
        public async Task<string> CommandSmsloginAsync(string phoneandcode)
        {
            var phoneAndCode = phoneandcode.Split(' ');
            string phone = phoneAndCode[0];
            string code;
            if (phoneAndCode.Length == 2)
            {
                code = phoneAndCode[1];
            }
            else
            {
                code = "";
            }

            if (!string.IsNullOrEmpty(code) && code.Length != 4)
            {
                return "请输入正确的验证码";
            }
            string url;
            Status1 status;
            if (string.IsNullOrEmpty(code))
            {
                url = $"{neteaseApi}/captcha/sent?phone={phone}&t={Utils.GetTimeStamp()}";
                status = await Utils.HttpGetAsync<Status1>(url);
                if (status.code == 200)
                {
                    return "验证码已发送";
                }
                else
                {
                    return "发送失败";
                }
            }

            url = $"{neteaseApi}/captcha/verify?phone={phone}&captcha={code}&t={Utils.GetTimeStamp()}";
            status = await Utils.HttpGetAsync<Status1>(url);
            if (status.code != 200)
            {
                return "验证码错误";
            }
            url = $"{neteaseApi}/login/cellphone?phone={phone}&captcha={code}&t={Utils.GetTimeStamp()}";
            status = await Utils.HttpGetAsync<Status1>(url);
            if (status.code == 200)
            {
                ChangeCookies(status.cookie, false);
                return "登陆成功";
            }
            else
            {
                return "登陆失败";
            }
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
        public async Task<string> CommandStatusAsync()
        {
            string result = $"\n网易云API: {neteaseApi}\n当前用户: ";

            try
            {
                if (!playControl.GetHeader().ContainsKey("Cookie") || string.IsNullOrEmpty(playControl.GetHeader()["Cookie"]))
                {
                    result += $"未登入";
                    return result;
                }

                var status = await GetLoginStatusAasync(neteaseApi, playControl.GetHeader());
                if (status == null || status.data == null || status.data.account == null)
                {
                    result += $"未登入";
                    return result;
                }

                if (status.data.code == 200 && status.data.account.status == 0)
                {
                    result += $"[URL=https://music.163.com/#/user/home?id={status.data.profile.userId}]{status.data.profile.nickname}[/URL]\n";
                }
                else
                {
                    result += $"未登入";
                }
            } catch (Exception e)
            {
                result += $"获取登录信息失败！";
                Log.Error(e, "GetLoginStatusAasync error");
            }

            return result;
        }

        [Command("here")]
        public async Task<string> CommandHere(Ts3Client ts3Client, ClientCall invoker, string password = null)
        {
            ChannelId channel = invoker.ChannelId.Value!;
            await ts3Client.MoveTo(channel, password);
            return "已移动到你所在的频道";
        }

        [Command("yun clear")]
        public async Task<string> CommandYunClear(PlayManager playManager)
        {
            playControl.Clear();
            if (playManager.IsPlaying)
            {
                await playManager.Stop();
            }
            return "已清除歌单";
        }

        [Command("yun fm")]
        public async Task<string> CommandYunFM()
        {
            if (!await IsUserLoggedIn())
            {
                return "私人FM需要登录网易云账号";
            }

            // Set mode to PrivateFM
            playControl.SetPrivateFM(true);
            config.isPrivateFMMode = true;
            config.Save();

            // Clear current playlist
            playControl.Clear();

            // Add first FM song and play
            await AddNextFMSong();
            await playControl.PlayNextMusic();

            return "已开启私人FM模式";
        }

        [Command("yun fm close")]
        public async Task<string> CommandYunFMClose()
        {
            // Set mode to PrivateFM
            playControl.SetPrivateFM(false);
            config.isPrivateFMMode = false;
            config.Save();

            // Clear current playlist
            playControl.Clear();

            return "已关闭私人FM模式";
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

        public async Task<GedanDetail> GetPlayListDetail(string id, Dictionary<string, string> header)
        {
            return await Utils.HttpGetAsync<GedanDetail>($"{neteaseApi}/playlist/detail?id={id}&timestamp={Utils.GetTimeStamp()}", header);
        }

        public async Task<ZhuanJi> GetAlbumDetail(string id, Dictionary<string, string> header)
        {
            return await Utils.HttpGetAsync<ZhuanJi>($"{neteaseApi}/album?id={id}&timestamp={Utils.GetTimeStamp()}", header);
        }

        public async Task<FM> GetFMDetail(string mode, string submode, Dictionary<string, string> header)
        {
            Log.Info($"{neteaseApi}/personal/fm/mode?mode={mode}&timestamp={Utils.GetTimeStamp()}", header);
            return await Utils.HttpGetAsync<FM>($"{neteaseApi}/personal/fm/mode?mode={mode}&timestamp={Utils.GetTimeStamp()}", header);
        }

        public static void ChangeCookies(string cookies, bool isQrlogin) //更改cookie
        {
            var cookie = Utils.ProcessCookie(cookies);
            Instance.playControl.GetHeader()["Cookie"] = cookie;
            config.Header["Cookie"] = cookie;
            config.isQrlogin = isQrlogin;
            config.Save();
        }

        public static async Task<RespStatus> GetLoginStatusAasync(string server, Dictionary<string, string> header)
        {
            return await Utils.HttpGetAsync<RespStatus>($"{server}/login/status?timestamp={Utils.GetTimeStamp()}", header);
        }

        public async Task genAlbumList(string id, int lenght, Dictionary<string, string> header, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            ZhuanJi ZhuanJis = await Utils.HttpGetAsync<ZhuanJi>($"{neteaseApi}/album?id={id}", header);
            long numOfSongs;
            if (lenght == -1)
            {
                numOfSongs = ZhuanJis.songs.Length;
            }
            else
            {
                numOfSongs = Math.Min(ZhuanJis.songs.Length, lenght);
            }
            if (numOfSongs > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{numOfSongs}]");
            }
            for (int i = 0; i < numOfSongs; i++)
            {
                long musicid = ZhuanJis.songs[i].id;
                if (musicid > 0)
                {
                    SongList.Add(new MusicInfo(musicid.ToString()));
                }
            }
        }

        public async Task genList(string id, int lenght, Dictionary<string, string> header, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            GedanDetail gedanDetail = await GetPlayListDetail(id, header);
            int trackCount = gedanDetail.playlist.trackCount;
            if (trackCount != 0)
            {
                await genListTrack(id, lenght, header, gedanDetail, SongList, ts3Client);
                return;
            }
            GeDan Gedans = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={id}", header);
            long numOfSongs;
            if (lenght == -1)
            {
                numOfSongs = Gedans.songs.Length;
            }
            else
            {
                numOfSongs = Math.Min(Gedans.songs.Length, lenght);
            }
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

        public async Task genListTrack(string id, int lenght, Dictionary<string, string> header, GedanDetail gedanDetail, List<MusicInfo> SongList, Ts3Client ts3Client) //生成歌单
        {
            int trackCount = gedanDetail.playlist.trackCount;
            int limit = 50;
            if (lenght != -1)
            {
                trackCount = Math.Min(gedanDetail.playlist.trackCount, lenght);
                limit = Math.Min(50, trackCount);
            }
            if (trackCount > 100)
            {
                await ts3Client.SendChannelMessage($"警告：歌单过大，可能需要一定的时间生成 [{trackCount}]");
            }
            for (int i = 0; i < trackCount; i += limit)
            {
                GeDan geDan = await Utils.HttpGetAsync<GeDan>($"{neteaseApi}/playlist/track/all?id={id}&limit={limit}&offset={i}", header);

                for (int j = 0; j < geDan.songs.Length; j++)
                {
                    SongList.Add(new MusicInfo(geDan.songs[j].id.ToString()));
                }

                await ts3Client.SendChannelMessage($"已添加歌曲 [{i + geDan.songs.Length}-{trackCount}]");
            }
        }
        
        //检查登录状态
        public async Task<bool> IsUserLoggedIn()
        {
            try

            {
                if (!playControl.GetHeader().ContainsKey("Cookie") || string.IsNullOrEmpty(playControl.GetHeader()["Cookie"]))
                {
                    return false;
                }

                var status = await GetLoginStatusAasync(neteaseApi, playControl.GetHeader());
                if (status == null || status.data == null || status.data.account == null)
                {
                    return false;
                }

                return status.data.code == 200 && status.data.account.status == 0;
            }
            catch (Exception e)
            {
                Log.Error(e, "IsUserLoggedIn error");
                return false;
            }
        }

        private async Task AddNextFMSong()
        {
            try
            {
                if (!await IsUserLoggedIn())
                {
                    await ts3Client.SendChannelMessage("私人FM需要登录网易云账号");
                    return;
                }

                // Get next FM song
                FM fmSongs = await GetFMDetail("DEFAULT", "", playControl.GetHeader());
                Log.Info(fmSongs.data.Length.ToString());
                FMData fmSong = fmSongs.data[0];
                Log.Info("FM ID:" + fmSong.id.ToString());
                Log.Info("FM Name:" + fmSong.name);
                if (fmSong?.id > 0)
                {
                    var music = new MusicInfo(fmSong.id.ToString(), false);
                    playControl.AddMusic(music);
                }
                else
                {
                    await ts3Client.SendChannelMessage("无法获取私人FM歌曲");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error getting FM song");
                await ts3Client.SendChannelMessage("获取私人FM歌曲失败");
            }
        }


        public void Dispose()
        {
            Instance = null;
            config = null;
            playControl = null;
            if (timer != null)
                timer.Dispose();
            timer = null;

            playManager.AfterResourceStarted -= PlayManager_AfterResourceStarted;
            playManager.PlaybackStopped -= PlayManager_PlaybackStopped;
            TS3FullClient.OnEachClientLeftView -= OnEachClientLeftView;
            TS3FullClient.OnEachClientEnterView -= OnEachClientEnterView;
            TS3FullClient.OnEachClientMoved -= OnEachClientMoved;
        }
    }
}