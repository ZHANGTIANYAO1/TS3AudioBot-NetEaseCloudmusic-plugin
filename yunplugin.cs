using System.Net;
using System.Text;
using System.Text.Json;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.CommandSystem;
using TS3AudioBot.Playlists;
using TS3AudioBot.Plugins;
using TS3AudioBot.ResourceFactories;
using TS3AudioBot.Sessions;
using TSLib.Full;

public class ArtistsItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long albumSize { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string fansGroup { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long img1v1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string trans { get; set; }
}

public class Artist
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long albumSize { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string fansGroup { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long img1v1 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string trans { get; set; }
}

public class Album
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Artist artist { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long publishTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mark { get; set; }
}

public class SongsItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArtistsItem> artists { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Album album { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long duration { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long rtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long ftype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mvid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mark { get; set; }
}

public class Result
{
    /// <summary>
    /// 
    /// </summary>
    public List<SongsItem> songs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool hasMore { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long songCount { get; set; }
}

public class yunSearchSong
{
    /// <summary>
    /// 
    /// </summary>
    public Result result { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
}

public class FreeTrialInfo
{
    /// <summary>
    /// 
    /// </summary>
    public long start { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long end { get; set; }
}

public class FreeTrialPrivilege
{
    /// <summary>
    /// 
    /// </summary>
    public bool resConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string listenType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cannotListenReason;
}

public class FreeTimeTrialPrivilege
{
    /// <summary>
    /// 
    /// </summary>
    public bool resConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long remalongime { get; set; }
}

public class DataItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string md5 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long expi { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double gain { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double peak;
    /// <summary>
    /// 
    /// </summary>
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string uf { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long payed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long flag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool canExtend { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialInfo freeTrialInfo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string level { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string encodeType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivilege freeTrialPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTimeTrialPrivilege freeTimeTrialPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long urlSource { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long rightSource { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string podcastCtrp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string effectTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long time { get; set; }
}

public class musicURL
{
    /// <summary>
    /// 
    /// </summary>
    public List<DataItem> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
}

public class ArItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
}

public class Al
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pic { get; set; }
}

public class H
{
    /// <summary>
    /// 
    /// </summary>
    public long br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
}

public class M
{
    /// <summary>
    /// 
    /// </summary>
    public long br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
}

public class L
{
    /// <summary>
    /// 
    /// </summary>
    public long br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
}

public class SongsItems
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long t { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem> ar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pop { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long v { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string crbt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cf { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Al al { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public H h;
    /// <summary>
    /// 
    /// </summary>
    public M m;
    /// <summary>
    /// 
    /// </summary>
    public L l;
    /// <summary>
    /// 
    /// </summary>
    public string? sq;
    /// <summary>
    /// 
    /// </summary>
    public string? hr;
    /// <summary>
    /// 
    /// </summary>
    public string? a;
    /// <summary>
    /// 
    /// </summary>
    public string cd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long no { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? rtUrl;
    /// <summary>
    /// 
    /// </summary>
    public long ftype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long djId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyright { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long s_id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long originCoverType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? originSongSimpleData;
    /// <summary>
    /// 
    /// </summary>
    public string? tagPicList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool resourceState;
    /// <summary>
    /// 
    /// </summary>
    public long version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? songJumpInfo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? entertainmentTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? awardTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long single { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool noCopyrightRcmd;
    /// <summary>
    /// 
    /// </summary>
    public long mst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long rtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? rurl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mv { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long publishTime { get; set; }
}

public class FreeTrial
{
    /// <summary>
    /// 
    /// </summary>
    public bool resConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string listenType { get; set; }
}

public class ChargeInfoListItem
{
    /// <summary>
    /// 
    /// </summary>
    public long rate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long chargeType { get; set; }
}

public class PrivilegesItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long payed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long sp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long subp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool cs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long maxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool toast { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long flag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool preSell { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long downloadMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string plLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string flLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rscl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTrial freeTrialPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItem> chargeInfoList { get; set; }
}

public class GeDan
{
    /// <summary>
    /// 
    /// </summary>
    public List<SongsItems> songs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<PrivilegesItem> privileges { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
}


public class Creator
{
    /// <summary>
    /// 淋雨丶伞
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long userId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long userType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long authStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string expertTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string experts { get; set; }
}

public class ArtistsItems
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long albumSize { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string trans { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long musicSize { get; set; }
}

public class Artists
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long albumSize { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string trans { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long musicSize { get; set; }
}

public class ArtistsItemss
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long albumSize { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string trans { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long musicSize { get; set; }
}

public class Albums
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string idStr { get; set; }
    /// <summary>
    /// 专辑
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long picId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string blurPicUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long companyId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long publishTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tags { get; set; }
    /// <summary>
    /// 独立发行
    /// </summary>
    public string company { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Artist artist { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> songs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string commentThreadId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArtistsItemss> artists { get; set; }
}

public class BMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string extension { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dfsId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long bitrate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta { get; set; }
}

public class HMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string extension { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dfsId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long bitrate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta { get; set; }
}

public class MMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string extension { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dfsId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long bitrate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta { get; set; }
}

public class LMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string extension { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long sr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dfsId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long bitrate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta { get; set; }
}

public class Track
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long position { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string disc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long no { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArtistsItemss> artists { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Albums album { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool starred { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long popularity { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long score { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long starredNum { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long duration { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playedNum { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long dayPlays { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long hearTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ringtone { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string crbt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string audition { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string copyFrom { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string commentThreadId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long ftype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyright { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long rtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rurl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public BMusic bMusic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mp3Url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mvid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public HMusic hMusic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public MMusic mMusic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public LMusic lMusic { get; set; }
}

public class PlaylistsItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 『纯音乐』有些歌只能一个人戴耳机听
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string coverImgUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Creator creator { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool subscribed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long trackCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long userId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long bookCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long specialType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> officialTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string action { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string actionType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string recommendText { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string score { get; set; }
    /// <summary>
    /// 平时收集的一些纯音乐做了整理，希望大家喜欢
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool highQuality { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Track track { get; set; }
    /// <summary>
    /// alg_search_rec_playlist_tab_basic_rewrite_{"hit":"Name","id":"有些歌只能一个人戴耳机听","type":"basic"}
    /// </summary>
    public string alg { get; set; }
}

public class Results
{
    /// <summary>
    /// 
    /// </summary>
    public List<PlaylistsItem> playlists { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool hasMore { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> hlWords { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long playlistCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string searchQcReminder { get; set; }
}

public class SearchGedan
{
    /// <summary>
    /// 
    /// </summary>
    public Results result { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
}

public class Data
{
    /// <summary>
    /// 
    /// </summary>
    public int code;
    /// <summary>
    /// 
    /// </summary>
    public string unikey { get; set; }
}

public class LoginKey
{
    /// <summary>
    /// 
    /// </summary>
    public Data data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int code;
}

public class Datas
{
    /// <summary>
    /// 
    /// </summary>
    public string qrurl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string qrimg { get; set; }
}

public class LoginImg
{
    /// <summary>
    /// 
    /// </summary>
    public int code;
    /// <summary>
    /// 
    /// </summary>
    public Datas data { get; set; }
}

public class Status1
{
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
    /// <summary>
    /// 等待扫码
    /// </summary>
    public string? message;
    /// <summary>
    /// 
    /// </summary>
    public string? cookie { get; set; }
}


//如果好用，请收藏地址，帮忙分享。
public class SubscribersItem
{
    /// <summary>
    /// 
    /// </summary>
    public string defaultAvatar;
    /// <summary>
    /// 
    /// </summary>
    public int province { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string followed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int accountStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int gender { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int city { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int birthday { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userType { get; set; }
    /// <summary>
    /// 神明懿2104
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string signature { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string detailDescription { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int avatarImgId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int backgroundImgId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string backgroundUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authority { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mutual { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string expertTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string experts { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int djStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vipType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string remarkName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authenticationTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarDetail { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgIdStr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string backgroundImgIdStr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string anchor { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgId_str { get; set; }
}

public class AvatarDetail
{
    /// <summary>
    /// 
    /// </summary>
    public int userType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int identityLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string identityIconUrl { get; set; }
}

public class Creators
{
    /// <summary>
    /// 
    /// </summary>
    public string defaultAvatar;
    /// <summary>
    /// 
    /// </summary>
    public int province { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string followed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int accountStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int gender { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int city { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int birthday { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userType { get; set; }
    /// <summary>
    /// 岩酱岩酱岩酱
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 这里是岩酱
    /// </summary>
    public string signature { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string detailDescription { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int avatarImgId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int backgroundImgId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string backgroundUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authority { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mutual { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string expertTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string experts { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int djStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vipType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string remarkName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int authenticationTypes { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public AvatarDetail avatarDetail { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgIdStr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string backgroundImgIdStr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string anchor { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgId_str { get; set; }
}

public class Sq
{
    /// <summary>
    /// 
    /// </summary>
    public int br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sr { get; set; }
}

public class TracksItem
{
    /// <summary>
    /// Reunion In The Wind(重逢风中）
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int t { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem> ar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pop { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int v { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string crbt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cf { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Al al { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int dt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public H h;
    /// <summary>
    /// 
    /// </summary>
    public M m;
    /// <summary>
    /// 
    /// </summary>
    public L l;
    /// <summary>
    /// 
    /// </summary>
    public Sq sq;
    /// <summary>
    /// 
    /// </summary>
    public string hr;
    /// <summary>
    /// 
    /// </summary>
    public string a { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int no { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ftype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int djId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int copyright { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int s_id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int mark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int originCoverType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string originSongSimpleData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tagPicList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool resourceState;
    /// <summary>
    /// 
    /// </summary>
    public int version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string songJumpInfo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string entertainmentTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int single { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string noCopyrightRcmd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rurl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int mst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int mv { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int rtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long publishTime;
}

public class TrackIdsItem
{
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int v { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int t { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int at { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string alg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rcmdReason { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string f { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sr { get; set; }
}

public class Playlist
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 睡觉听的纯音乐（与君梦中相会）
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long coverImgId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string coverImgUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string coverImgId_str { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int adType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int createTime;
    /// <summary>
    /// 
    /// </summary>
    public int status;
    /// <summary>
    /// 
    /// </summary>
    public bool opRecommend;
    /// <summary>
    /// 
    /// </summary>
    public bool highQuality;
    /// <summary>
    /// 
    /// </summary>
    public bool newImported;
    /// <summary>
    /// 
    /// </summary>
    public int updateTime;
    /// <summary>
    /// 
    /// </summary>
    public int trackCount;
    /// <summary>
    /// 
    /// </summary>
    public int specialType;
    /// <summary>
    /// 
    /// </summary>
    public int privacy;
    /// <summary>
    /// 
    /// </summary>
    public int trackUpdateTime;
    /// <summary>
    /// 
    /// </summary>
    public string commentThreadId;
    /// <summary>
    /// 
    /// </summary>
    public int playCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long trackNumberUpdateTime;
    /// <summary>
    /// 
    /// </summary>
    public int subscribedCount;
    /// <summary>
    /// 
    /// </summary>
    public int cloudTrackCount;
    /// <summary>
    /// 
    /// </summary>
    public bool ordered { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string updateFrequency { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int backgroundCoverId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string backgroundCoverUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int titleImage;
    /// <summary>
    /// 
    /// </summary>
    public string titleImageUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string englishTitle { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string officialPlaylistType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool copied { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string relateResType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<SubscribersItem> subscribers;
    /// <summary>
    /// 
    /// </summary>
    public bool subscribed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Creators creator;
    /// <summary>
    /// 
    /// </summary>
    public List<TracksItem> tracks;
    /// <summary>
    /// 
    /// </summary>
    public string videoIds { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string videos { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TrackIdsItem> trackIds;
    /// <summary>
    /// 
    /// </summary>
    public string bannedTrackIds { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mvResourceInfos { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int shareCount;
    /// <summary>
    /// 
    /// </summary>
    public int commentCount;
    /// <summary>
    /// 
    /// </summary>
    public string remixVideo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sharedUsers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string historySharedUsers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string gradeStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string score { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string algTags;
}

public class FreeTrialPrivileges
{
    /// <summary>
    /// 
    /// </summary>
    public string resConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string userConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string listenType { get; set; }
}

public class ChargeInfoListItems
{
    /// <summary>
    /// 
    /// </summary>
    public int rate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int chargeType { get; set; }
}

public class PrivilegesItems
{
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int payed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int realPayed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int dl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int subp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int maxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string toast { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int flag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string paidBigBang { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string preSell { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int playMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int downloadMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string plLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string flLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rscl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivileges freeTrialPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItems> chargeInfoList { get; set; }
}

public class GedanDetail
{
    /// <summary>
    /// 
    /// </summary>
    public long code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string relatedVideos { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Playlist playlist { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string urls { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<PrivilegesItems> privileges;
    /// <summary>
    /// 
    /// </summary>
    public string sharedPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string resEntrance { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string fromUsers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fromUserCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string songFromUsers { get; set; }
}


public class musicCheck
{
    /// <summary>
    /// 
    /// </summary>
    public bool success { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string message;
}

public class ArItem1
{
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias { get; set; }
}

public class Al1
{
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pic_str { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long pic;
}

public class H1
{
    /// <summary>
    /// 
    /// </summary>
    public int br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sr { get; set; }
}

public class M1
{
    /// <summary>
    /// 
    /// </summary>
    public int br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sr { get; set; }
}

public class L1
{
    /// <summary>
    /// 
    /// </summary>
    public int br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sr { get; set; }
}

public class Sq1
{
    /// <summary>
    /// 
    /// </summary>
    public int br { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sr { get; set; }
}

public class SongsItem1
{
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int t { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem1> ar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pop { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int v { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string crbt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cf { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Al1 al { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int dt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public H1 h { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public M1 m { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public L1 l { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Sq1 sq { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string hr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string a { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int no { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ftype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long djId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long copyright { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long s_id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long mark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int originCoverType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string originSongSimpleData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tagPicList { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string resourceState;
    /// <summary>
    /// 
    /// </summary>
    public int version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string songJumpInfo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string entertainmentTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string awardTags { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int single { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string noCopyrightRcmd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int mv { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int mst { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int rtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rurl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long publishTime;
}

public class FreeTrialPrivilege1
{
    /// <summary>
    /// 
    /// </summary>
    public string resConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string userConsumable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string listenType { get; set; }
}

public class ChargeInfoListItem1
{
    /// <summary>
    /// 
    /// </summary>
    public long rate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int chargeType { get; set; }
}

public class PrivilegesItem1
{
    /// <summary>
    /// 
    /// </summary>
    public long id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fee { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int payed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int st { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int dl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int cp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int subp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int maxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string toast { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int flag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string preSell { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int playMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int downloadMaxbr { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string plLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string flLevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string rscl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivilege1 freeTrialPrivilege { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItem1> chargeInfoList { get; set; }
}

public class MusicDetail
{
    /// <summary>
    /// 
    /// </summary>
    public List<SongsItem1> songs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<PrivilegesItem1> privileges;
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; }
}


public class YunPlgun : IBotPlugin /* or ICorePlugin */
{
    PlayManager playManager;
    Ts3Client ts3Client;
    InvokerData invoker;
    PlaylistManager playlistManager;
    ResolveContext resourceFactory;
    private TsFullClient tsClient;
    public static string cookies1 = "";
    public async void Initialize()
    {
        this.playManager = playManager;
        this.ts3Client = ts3Client;
        this.playlistManager = playlistManager;
        this.invoker = invoker;
        this.resourceFactory = resourceFactory;
        this.tsClient = tsClient;
        Console.WriteLine("Yun Plugin loaded");
    }

    [Command("hello")]
    public static string CommandHello()
    {
        return ("hello");
    }

    [Command("yun play")]
    public static async Task<string> CommandYunPlay(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string urlSearch = "http://localhost:3000/search?keywords=" + arguments + "&limit=1";
        string Searchjson = HttpGet(urlSearch);
        yunSearchSong? yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
        long firstmusicid = yunSearchSong.result.songs[0].id;
        string firstmusicname = yunSearchSong.result.songs[0].name;
        Console.WriteLine(firstmusicid + firstmusicname);
        string musicurl = getMusicUrl(firstmusicid, true);
        string musicdetailurl = "http://localhost:3000/song/detail?ids=" + firstmusicid.ToString();
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicimgurl = musicDetail.songs[0].al.picUrl;
        string musicname = musicDetail.songs[0].name;
        await MainCommands.CommandBotAvatarSet(ts3Client, musicimgurl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, musicname);
        Console.WriteLine(musicurl);

        if (musicurl != "error")
        {
            MainCommands.CommandPlay(playManager, invoker, musicurl);
            MainCommands.CommandBotDescriptionSet(ts3Client, firstmusicname);
            ts3Client.SendChannelMessage("正在播放音乐：" + firstmusicname);
            string result = "正在播放音乐：" + firstmusicname;
            return (result);
        }
        return ("发生未知错误");
    }

    [Command("yun add")]
    public static async Task<string> CommandYunAdd(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string urlSearch = "http://localhost:3000/search?keywords=" + arguments + "&limit=1";
        string Searchjson = HttpGet(urlSearch);
        yunSearchSong? yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
        long firstmusicid = yunSearchSong.result.songs[0].id;
        string firstmusicname = yunSearchSong.result.songs[0].name;
        Console.WriteLine(firstmusicid + firstmusicname);
        string musicurl = getMusicUrl(firstmusicid, true);
        string musicdetailurl = "http://localhost:3000/song/detail?ids=" + firstmusicid.ToString();
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicimgurl = musicDetail.songs[0].al.picUrl;
        string musicname = musicDetail.songs[0].name;
        await MainCommands.CommandBotAvatarSet(ts3Client, musicimgurl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, musicname);
        Console.WriteLine(musicurl);
        if (musicurl != "error")
        {
            MainCommands.CommandAdd(playManager, invoker, musicurl);
            MainCommands.CommandBotDescriptionSet(ts3Client, firstmusicname);
            ts3Client.SendChannelMessage("以下音乐已经添加到播放列表中：" + firstmusicname);
            string result = "以下音乐已经添加到播放列表中：" + firstmusicname;
            return (result);
        }
        return ("发生未知错误");
    }

    [Command("yun playid")]
    public static async Task<string> CommandYunPlayId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string musicurl = getMusicUrl(arguments, true);
        string musicdetailurl = "http://localhost:3000/song/detail?ids=" + arguments;
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicimgurl = musicDetail.songs[0].al.picUrl;
        string musicname = musicDetail.songs[0].name;
        await MainCommands.CommandBotAvatarSet(ts3Client, musicimgurl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, musicname);
        Console.WriteLine(musicurl);
        if (musicurl != "error")
        {
            MainCommands.CommandPlay(playManager, invoker, musicurl);
            ts3Client.SendChannelMessage("正在播放音乐id为：" + arguments.ToString());
            string result = "正在播放音乐id为：" + arguments.ToString();
            return (result);
        }
        return ("发生未知错误");
    }

    [Command("yun addid")]
    public static async Task<string> CommandYunAddId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string musicurl = getMusicUrl(arguments, true);
        Console.WriteLine(musicurl);
        string musicdetailurl = "http://localhost:3000/song/detail?ids=" + arguments;
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicimgurl = musicDetail.songs[0].al.picUrl;
        string musicname = musicDetail.songs[0].name;
        await MainCommands.CommandBotAvatarSet(ts3Client, musicimgurl);
        await MainCommands.CommandBotDescriptionSet(ts3Client, musicname);
        if (musicurl != "error")
        {
            MainCommands.CommandAdd(playManager, invoker, musicurl);
            ts3Client.SendChannelMessage("以下id的音乐已经添加到播放列表中：" + arguments.ToString());
            string result = "以下id的音乐已经添加到播放列表中：" + arguments.ToString();
            return (result);
        }
        return ("发生未知错误");
    }

    [Command("yun gedan")]
    public static async Task<string> CommandGedan(string name, PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string urlSearch = "http://localhost:3000/search?keywords=" + name + "&limit=1&type=1000";
        string json = HttpGet(urlSearch);
        SearchGedan searchgedan = JsonSerializer.Deserialize<SearchGedan>(json);
        long gedanid = searchgedan.result.playlists[0].id;
        string strid = gedanid.ToString();
        string imgurl = searchgedan.result.playlists[0].coverImgUrl;
        await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
        string gedanname = searchgedan.result.playlists[0].name;
        await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
        if (!playlistManager.ExistsPlaylist(strid))
        {
            Console.WriteLine("正在生成歌单请勿输入其他指令");
            await ts3Client.SendChannelMessage("正在生成歌单请勿输入其他指令");
            await genGeDan(gedanid, playlistManager, resourceFactory, ts3Client);
        }
        if (playlistManager.ExistsPlaylist(strid))
        {
            await MainCommands.CommandListPlayInternal(playlistManager, playManager, invoker, strid);
            await ts3Client.SendChannelMessage("开始播放歌单：" + gedanname + " （如果没有播放，重新输入指令即可解决）");
            return ("开始播放歌单：" + gedanname + " （如果没有播放，重新输入指令即可解决）");
        }
        return ("重新输入相同指令即可播放");
    }

    [Command("yun gedanid")]
    public static async Task<string> CommandGedanid(long id, PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, TsFullClient tsClient, UserSession session)
    {
        string strid = id.ToString();
        string url = "http://localhost:3000/playlist/detail?id=" + strid;
        string json = HttpGet(url);
        GedanDetail gedanDetail = JsonSerializer.Deserialize<GedanDetail>(json);
        string gedanname = gedanDetail.playlist.name;
        string imgurl = gedanDetail.playlist.coverImgUrl;
        await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
        await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
        if (!playlistManager.ExistsPlaylist(strid))
        {
            Console.WriteLine("正在生成歌单请勿输入其他指令");
            await ts3Client.SendChannelMessage("正在生成歌单请勿输入其他指令");
            await genGeDan(id, playlistManager, resourceFactory, ts3Client);
        }
        if (playlistManager.ExistsPlaylist(strid))
        {
            await MainCommands.CommandListPlayInternal(playlistManager, playManager, invoker, strid);
            await ts3Client.SendChannelMessage("开始播放歌单：" + gedanname + "的歌单 （如果没有播放，重新输入指令即可解决）");
            return ("开始播放歌单：" + gedanname + " （如果没有播放，重新输入指令即可解决）");
        }
        return ("重新输入相同指令即可播放");
    }

    public static async Task genGeDan(long id, PlaylistManager playlistManager, ResolveContext resourceFactory, Ts3Client ts3Client)
    {
        string gedanid = id.ToString();
        string url = "http://localhost:3000/playlist/track/all?id=" + gedanid;
        string gedanjson = HttpGet(url);
        GeDan Gedans = JsonSerializer.Deserialize<GeDan>(gedanjson);
        MainCommands.CommandListCreate(playlistManager, gedanid);
        long numOfSongs = Gedans.songs.Count();
        if (numOfSongs > 100)
        {
            Console.WriteLine("警告歌单过大，可能需要一定的时间生成");
            await ts3Client.SendChannelMessage("警告歌单过大，可能需要一定的时间生成");
        }
        for (int i = 0; i < numOfSongs; i++)
        {
            long musicid = Gedans.songs[i].id;
            string musicUrl = getMusicUrl(musicid, false);
            if (musicUrl != null)
            {
                MainCommands.CommandListAdd(resourceFactory, playlistManager, gedanid, musicUrl);
            }
        }
    }

    public static string getMusicUrl(long id, bool usingcookie = false)
    {
        string cookie = getCookies();
        string url;
        if (usingcookie)
        {
            url = "http://localhost:3000/song/url?id=" + id.ToString() + "&cookie=" + cookie;         
        }
        else
        {
            url = "http://localhost:3000/song/url?id=" + id.ToString();
        }
        string musicurljson = HttpGet(url);
        musicURL musicurl = JsonSerializer.Deserialize<musicURL>(musicurljson);
        long code = musicurl.code;
        string mp3 = musicurl.data[0].url;
        return mp3;
    }

    public static string HttpGet(string url)
    {
        //ServicePolongManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        Encoding encoding = Encoding.UTF8;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Accept = "text/html, application/xhtml+xml, */*";
        request.ContentType = "application/json";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    public static string getCookies()
    {
        return cookies1;
    }

    public static void changeCookies(string newcookies)
    {
        cookies1 = newcookies;
    }

    [Command("yun login")]
    public static async Task<string> CommanloginAsync(Ts3Client ts3Client, TsFullClient tsClient)
    {
        string url1 = "http://localhost:3000/login/qr/key" + "?timestamp=" + GetTimeStamp();
        string json1 = HttpGet(url1);
        Console.WriteLine(json1);
        LoginKey loginKey = JsonSerializer.Deserialize<LoginKey>(json1);
        string key = loginKey.data.unikey;
        string url2 = "http://localhost:3000/login/qr/create?key=" + key + "&qrimg=true&timestamp=" + GetTimeStamp();
        string json2 = HttpGet(url2);
        LoginImg loginImg = JsonSerializer.Deserialize<LoginImg>(json2);
        string base64String = loginImg.data.qrimg;
        await ts3Client.SendChannelMessage("正在生成二维码");
        await ts3Client.SendChannelMessage(loginImg.data.qrimg);
        Console.WriteLine(base64String);
        string[] img = base64String.Split(",");
        byte[] bytes = Convert.FromBase64String(img[1]);
        Stream stream = new MemoryStream(bytes);
        await tsClient.UploadAvatar(stream);
        await ts3Client.ChangeDescription("请用网易云APP扫描二维码登陆");
        int i = 0;
        long code;
        string result;
        while (true)
        {
            string url3 = "http://localhost:3000/login/qr/check?key=" + key + "&timestamp=" + GetTimeStamp();
            string json3 = HttpGet(url3);
            Console.WriteLine(json3);
            Status1 status1 = JsonSerializer.Deserialize<Status1>(json3);
            code = status1.code;
            cookies1 = getCookies();
            cookies1 = status1.cookie;
            changeCookies(cookies1);
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
        changeCookies(cookies1);
        return result;
    }

    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public void Dispose()
    {

    }
}
