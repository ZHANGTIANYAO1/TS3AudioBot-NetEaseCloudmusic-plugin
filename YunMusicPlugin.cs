using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TS3AudioBot;
using TS3AudioBot.Audio;
using TS3AudioBot.CommandSystem;
using TS3AudioBot.Playlists;
using TS3AudioBot.Plugins;
using TS3AudioBot.ResourceFactories;
using TS3AudioBot.Sessions;
using TSLib.Full;
//{ get; set; }
public class ArtistsItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public long albumSize;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public string fansGroup;
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url;
    /// <summary>
    /// 
    /// </summary>
    public long img1v1;
    /// <summary>
    /// 
    /// </summary>
    public string trans;
}

public class Artist
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public long albumSize;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public string fansGroup;
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url;
    /// <summary>
    /// 
    /// </summary>
    public long img1v1;
    /// <summary>
    /// 
    /// </summary>
    public string trans;
}

public class Album
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public Artist artist;
    /// <summary>
    /// 
    /// </summary>
    public long publishTime;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId;
    /// <summary>
    /// 
    /// </summary>
    public long status;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public long mark;
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
    public List<ArtistsItem> artists;
    /// <summary>
    /// 
    /// </summary>
    public Album album;
    /// <summary>
    /// 
    /// </summary>
    public long duration;
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId;
    /// <summary>
    /// 
    /// </summary>
    public long status;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public long rtype;
    /// <summary>
    /// 
    /// </summary>
    public long ftype;
    /// <summary>
    /// 
    /// </summary>
    public long mvid;
    /// <summary>
    /// 
    /// </summary>
    public long fee;
    /// <summary>
    /// 
    /// </summary>
    public string rUrl;
    /// <summary>
    /// 
    /// </summary>
    public long mark;
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
    public bool hasMore;
    /// <summary>
    /// 
    /// </summary>
    public long songCount;
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
    public long code;
}

public class FreeTrialInfo
{
    /// <summary>
    /// 
    /// </summary>
    public long start;
    /// <summary>
    /// 
    /// </summary>
    public long end;
}

public class FreeTrialPrivilege
{
    /// <summary>
    /// 
    /// </summary>
    public bool resConsumable;
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string listenType;
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
    public bool resConsumable;
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable;
    /// <summary>
    /// 
    /// </summary>
    public long type;
    /// <summary>
    /// 
    /// </summary>
    public long remalongime;
}

public class DataItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long br;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public string md5;
    /// <summary>
    /// 
    /// </summary>
    public long code;
    /// <summary>
    /// 
    /// </summary>
    public long expi;
    /// <summary>
    /// 
    /// </summary>
    public string type;
    /// <summary>
    /// 
    /// </summary>
    public double gain;
    /// <summary>
    /// 
    /// </summary>
    public double peak;
    /// <summary>
    /// 
    /// </summary>
    public long fee;
    /// <summary>
    /// 
    /// </summary>
    public string uf;
    /// <summary>
    /// 
    /// </summary>
    public long payed;
    /// <summary>
    /// 
    /// </summary>
    public long flag;
    /// <summary>
    /// 
    /// </summary>
    public bool canExtend;
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialInfo freeTrialInfo;
    /// <summary>
    /// 
    /// </summary>
    public string level;
    /// <summary>
    /// 
    /// </summary>
    public string encodeType;
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivilege freeTrialPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public FreeTimeTrialPrivilege freeTimeTrialPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public long urlSource;
    /// <summary>
    /// 
    /// </summary>
    public long rightSource;
    /// <summary>
    /// 
    /// </summary>
    public string podcastCtrp;
    /// <summary>
    /// 
    /// </summary>
    public string effectTypes;
    /// <summary>
    /// 
    /// </summary>
    public long time;
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
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
}

public class Al
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns;
    /// <summary>
    /// 
    /// </summary>
    public long pic;
}

public class H
{
    /// <summary>
    /// 
    /// </summary>
    public long br;
    /// <summary>
    /// 
    /// </summary>
    public long fid;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
}

public class M
{
    /// <summary>
    /// 
    /// </summary>
    public long br;
    /// <summary>
    /// 
    /// </summary>
    public long fid;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
}

public class L
{
    /// <summary>
    /// 
    /// </summary>
    public long br;
    /// <summary>
    /// 
    /// </summary>
    public long fid;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public long vd;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
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
    public long pst;
    /// <summary>
    /// 
    /// </summary>
    public long t;
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem> ar;
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia;
    /// <summary>
    /// 
    /// </summary>
    public long pop;
    /// <summary>
    /// 
    /// </summary>
    public long st;
    /// <summary>
    /// 
    /// </summary>
    public string rt;
    /// <summary>
    /// 
    /// </summary>
    public long fee;
    /// <summary>
    /// 
    /// </summary>
    public long v;
    /// <summary>
    /// 
    /// </summary>
    public string crbt;
    /// <summary>
    /// 
    /// </summary>
    public string cf;
    /// <summary>
    /// 
    /// </summary>
    public Al al;
    /// <summary>
    /// 
    /// </summary>
    public long dt;
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
    public string cd;
    /// <summary>
    /// 
    /// </summary>
    public long no;
    /// <summary>
    /// 
    /// </summary>
    public string? rtUrl;
    /// <summary>
    /// 
    /// </summary>
    public long ftype;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls;
    /// <summary>
    /// 
    /// </summary>
    public long djId;
    /// <summary>
    /// 
    /// </summary>
    public long copyright;
    /// <summary>
    /// 
    /// </summary>
    public long s_id;
    /// <summary>
    /// 
    /// </summary>
    public long mark;
    /// <summary>
    /// 
    /// </summary>
    public long originCoverType;
    /// <summary>
    /// 
    /// </summary>
    public string? originSongSimpleData;
    /// <summary>
    /// 
    /// </summary>
    public string? tagPicList;
    /// <summary>
    /// 
    /// </summary>
    public bool resourceState;
    /// <summary>
    /// 
    /// </summary>
    public long version;
    /// <summary>
    /// 
    /// </summary>
    public string? songJumpInfo;
    /// <summary>
    /// 
    /// </summary>
    public string? entertainmentTags;
    /// <summary>
    /// 
    /// </summary>
    public string? awardTags;
    /// <summary>
    /// 
    /// </summary>
    public long single;
    /// <summary>
    /// 
    /// </summary>
    public bool noCopyrightRcmd;
    /// <summary>
    /// 
    /// </summary>
    public long mst;
    /// <summary>
    /// 
    /// </summary>
    public long cp;
    /// <summary>
    /// 
    /// </summary>
    public long rtype;
    /// <summary>
    /// 
    /// </summary>
    public string? rurl;
    /// <summary>
    /// 
    /// </summary>
    public long mv;
    /// <summary>
    /// 
    /// </summary>
    public long publishTime;
}

public class FreeTrial
{
    /// <summary>
    /// 
    /// </summary>
    public bool resConsumable;
    /// <summary>
    /// 
    /// </summary>
    public bool userConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string listenType;
}

public class ChargeInfoListItem
{
    /// <summary>
    /// 
    /// </summary>
    public long rate;
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl;
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage;
    /// <summary>
    /// 
    /// </summary>
    public long chargeType;
}

public class PrivilegesItem
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long fee;
    /// <summary>
    /// 
    /// </summary>
    public long payed;
    /// <summary>
    /// 
    /// </summary>
    public long st;
    /// <summary>
    /// 
    /// </summary>
    public long pl;
    /// <summary>
    /// 
    /// </summary>
    public long dl;
    /// <summary>
    /// 
    /// </summary>
    public long sp;
    /// <summary>
    /// 
    /// </summary>
    public long cp;
    /// <summary>
    /// 
    /// </summary>
    public long subp;
    /// <summary>
    /// 
    /// </summary>
    public bool cs;
    /// <summary>
    /// 
    /// </summary>
    public long maxbr;
    /// <summary>
    /// 
    /// </summary>
    public long fl;
    /// <summary>
    /// 
    /// </summary>
    public bool toast;
    /// <summary>
    /// 
    /// </summary>
    public long flag;
    /// <summary>
    /// 
    /// </summary>
    public bool preSell;
    /// <summary>
    /// 
    /// </summary>
    public long playMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public long downloadMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string plLevel;
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel;
    /// <summary>
    /// 
    /// </summary>
    public string flLevel;
    /// <summary>
    /// 
    /// </summary>
    public string rscl;
    /// <summary>
    /// 
    /// </summary>
    public FreeTrial freeTrialPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItem> chargeInfoList;
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
    public List<PrivilegesItem> privileges;
    /// <summary>
    /// 
    /// </summary>
    public long code;
}


public class Creator
{
    /// <summary>
    /// 淋雨丶伞
    /// </summary>
    public string nickname;
    /// <summary>
    /// 
    /// </summary>
    public long userId;
    /// <summary>
    /// 
    /// </summary>
    public long userType;
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl;
    /// <summary>
    /// 
    /// </summary>
    public long authStatus;
    /// <summary>
    /// 
    /// </summary>
    public string expertTags;
    /// <summary>
    /// 
    /// </summary>
    public string experts;
}

public class ArtistsItems
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id;
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url;
    /// <summary>
    /// 
    /// </summary>
    public long albumSize;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public string trans;
    /// <summary>
    /// 
    /// </summary>
    public long musicSize;
}

public class Artists
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id;
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url;
    /// <summary>
    /// 
    /// </summary>
    public long albumSize;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public string trans;
    /// <summary>
    /// 
    /// </summary>
    public long musicSize;
}

public class ArtistsItemss
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public long img1v1Id;
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public string img1v1Url;
    /// <summary>
    /// 
    /// </summary>
    public long albumSize;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public string trans;
    /// <summary>
    /// 
    /// </summary>
    public long musicSize;
}

public class Albums
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public string idStr;
    /// <summary>
    /// 专辑
    /// </summary>
    public string type;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public long picId;
    /// <summary>
    /// 
    /// </summary>
    public string blurPicUrl;
    /// <summary>
    /// 
    /// </summary>
    public long companyId;
    /// <summary>
    /// 
    /// </summary>
    public long pic;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl;
    /// <summary>
    /// 
    /// </summary>
    public long publishTime;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public string tags;
    /// <summary>
    /// 独立发行
    /// </summary>
    public string company;
    /// <summary>
    /// 
    /// </summary>
    public string briefDesc;
    /// <summary>
    /// 
    /// </summary>
    public Artist artist;
    /// <summary>
    /// 
    /// </summary>
    public List<string> songs;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public long status;
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId;
    /// <summary>
    /// 
    /// </summary>
    public string commentThreadId;
    /// <summary>
    /// 
    /// </summary>
    public List<ArtistsItemss> artists;
}

public class BMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public string extension;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
    /// <summary>
    /// 
    /// </summary>
    public long dfsId;
    /// <summary>
    /// 
    /// </summary>
    public long bitrate;
    /// <summary>
    /// 
    /// </summary>
    public long playTime;
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta;
}

public class HMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public string extension;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
    /// <summary>
    /// 
    /// </summary>
    public long dfsId;
    /// <summary>
    /// 
    /// </summary>
    public long bitrate;
    /// <summary>
    /// 
    /// </summary>
    public long playTime;
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta;
}

public class MMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public string extension;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
    /// <summary>
    /// 
    /// </summary>
    public long dfsId;
    /// <summary>
    /// 
    /// </summary>
    public long bitrate;
    /// <summary>
    /// 
    /// </summary>
    public long playTime;
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta;
}

public class LMusic
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long size;
    /// <summary>
    /// 
    /// </summary>
    public string extension;
    /// <summary>
    /// 
    /// </summary>
    public long sr;
    /// <summary>
    /// 
    /// </summary>
    public long dfsId;
    /// <summary>
    /// 
    /// </summary>
    public long bitrate;
    /// <summary>
    /// 
    /// </summary>
    public long playTime;
    /// <summary>
    /// 
    /// </summary>
    public long volumeDelta;
}

public class Track
{
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public long position;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
    /// <summary>
    /// 
    /// </summary>
    public long status;
    /// <summary>
    /// 
    /// </summary>
    public long fee;
    /// <summary>
    /// 
    /// </summary>
    public long copyrightId;
    /// <summary>
    /// 
    /// </summary>
    public string disc;
    /// <summary>
    /// 
    /// </summary>
    public long no;
    /// <summary>
    /// 
    /// </summary>
    public List<ArtistsItemss> artists;
    /// <summary>
    /// 
    /// </summary>
    public Albums album;
    /// <summary>
    /// 
    /// </summary>
    public bool starred;
    /// <summary>
    /// 
    /// </summary>
    public long popularity;
    /// <summary>
    /// 
    /// </summary>
    public long score;
    /// <summary>
    /// 
    /// </summary>
    public long starredNum;
    /// <summary>
    /// 
    /// </summary>
    public long duration;
    /// <summary>
    /// 
    /// </summary>
    public long playedNum;
    /// <summary>
    /// 
    /// </summary>
    public long dayPlays;
    /// <summary>
    /// 
    /// </summary>
    public long hearTime;
    /// <summary>
    /// 
    /// </summary>
    public string ringtone;
    /// <summary>
    /// 
    /// </summary>
    public string crbt;
    /// <summary>
    /// 
    /// </summary>
    public string audition;
    /// <summary>
    /// 
    /// </summary>
    public string copyFrom;
    /// <summary>
    /// 
    /// </summary>
    public string commentThreadId;
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl;
    /// <summary>
    /// 
    /// </summary>
    public long ftype;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls;
    /// <summary>
    /// 
    /// </summary>
    public long copyright;
    /// <summary>
    /// 
    /// </summary>
    public long rtype;
    /// <summary>
    /// 
    /// </summary>
    public string rurl;
    /// <summary>
    /// 
    /// </summary>
    public BMusic bMusic;
    /// <summary>
    /// 
    /// </summary>
    public string mp3Url;
    /// <summary>
    /// 
    /// </summary>
    public long mvid;
    /// <summary>
    /// 
    /// </summary>
    public HMusic hMusic;
    /// <summary>
    /// 
    /// </summary>
    public MMusic mMusic;
    /// <summary>
    /// 
    /// </summary>
    public LMusic lMusic;
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
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string coverImgUrl;
    /// <summary>
    /// 
    /// </summary>
    public Creator creator;
    /// <summary>
    /// 
    /// </summary>
    public bool subscribed;
    /// <summary>
    /// 
    /// </summary>
    public long trackCount;
    /// <summary>
    /// 
    /// </summary>
    public long userId;
    /// <summary>
    /// 
    /// </summary>
    public long playCount;
    /// <summary>
    /// 
    /// </summary>
    public long bookCount;
    /// <summary>
    /// 
    /// </summary>
    public long specialType;
    /// <summary>
    /// 
    /// </summary>
    public List<string> officialTags;
    /// <summary>
    /// 
    /// </summary>
    public string action;
    /// <summary>
    /// 
    /// </summary>
    public string actionType;
    /// <summary>
    /// 
    /// </summary>
    public string recommendText;
    /// <summary>
    /// 
    /// </summary>
    public string score;
    /// <summary>
    /// 平时收集的一些纯音乐做了整理，希望大家喜欢
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public bool highQuality;
    /// <summary>
    /// 
    /// </summary>
    public Track track;
    /// <summary>
    /// alg_search_rec_playlist_tab_basic_rewrite_{"hit":"Name","id":"有些歌只能一个人戴耳机听","type":"basic"}
    /// </summary>
    public string alg;
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
    public bool hasMore;
    /// <summary>
    /// 
    /// </summary>
    public List<string> hlWords;
    /// <summary>
    /// 
    /// </summary>
    public long playlistCount;
    /// <summary>
    /// 
    /// </summary>
    public string searchQcReminder;
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
    public long code;
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
    public string qrurl;
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
    public string? message { get; set; }
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
    public int province;
    /// <summary>
    /// 
    /// </summary>
    public int authStatus;
    /// <summary>
    /// 
    /// </summary>
    public string followed;
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl;
    /// <summary>
    /// 
    /// </summary>
    public int accountStatus;
    /// <summary>
    /// 
    /// </summary>
    public int gender;
    /// <summary>
    /// 
    /// </summary>
    public int city;
    /// <summary>
    /// 
    /// </summary>
    public int birthday;
    /// <summary>
    /// 
    /// </summary>
    public long userId;
    /// <summary>
    /// 
    /// </summary>
    public int userType;
    /// <summary>
    /// 神明懿2104
    /// </summary>
    public string nickname;
    /// <summary>
    /// 
    /// </summary>
    public string signature;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public string detailDescription;
    /// <summary>
    /// 
    /// </summary>
    public int avatarImgId;
    /// <summary>
    /// 
    /// </summary>
    public int backgroundImgId;
    /// <summary>
    /// 
    /// </summary>
    public string backgroundUrl;
    /// <summary>
    /// 
    /// </summary>
    public int authority;
    /// <summary>
    /// 
    /// </summary>
    public string mutual;
    /// <summary>
    /// 
    /// </summary>
    public string expertTags;
    /// <summary>
    /// 
    /// </summary>
    public string experts;
    /// <summary>
    /// 
    /// </summary>
    public int djStatus;
    /// <summary>
    /// 
    /// </summary>
    public int vipType;
    /// <summary>
    /// 
    /// </summary>
    public string remarkName;
    /// <summary>
    /// 
    /// </summary>
    public int authenticationTypes;
    /// <summary>
    /// 
    /// </summary>
    public string avatarDetail;
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgIdStr;
    /// <summary>
    /// 
    /// </summary>
    public string backgroundImgIdStr;
    /// <summary>
    /// 
    /// </summary>
    public string anchor;
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgId_str;
}

public class AvatarDetail
{
    /// <summary>
    /// 
    /// </summary>
    public int userType;
    /// <summary>
    /// 
    /// </summary>
    public int identityLevel;
    /// <summary>
    /// 
    /// </summary>
    public string identityIconUrl;
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
    public int province;
    /// <summary>
    /// 
    /// </summary>
    public int authStatus;
    /// <summary>
    /// 
    /// </summary>
    public string followed;
    /// <summary>
    /// 
    /// </summary>
    public string avatarUrl;
    /// <summary>
    /// 
    /// </summary>
    public int accountStatus;
    /// <summary>
    /// 
    /// </summary>
    public int gender;
    /// <summary>
    /// 
    /// </summary>
    public int city;
    /// <summary>
    /// 
    /// </summary>
    public int birthday;
    /// <summary>
    /// 
    /// </summary>
    public long userId;
    /// <summary>
    /// 
    /// </summary>
    public int userType;
    /// <summary>
    /// 岩酱岩酱岩酱
    /// </summary>
    public string nickname;
    /// <summary>
    /// 这里是岩酱
    /// </summary>
    public string signature;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public string detailDescription;
    /// <summary>
    /// 
    /// </summary>
    public int avatarImgId;
    /// <summary>
    /// 
    /// </summary>
    public int backgroundImgId;
    /// <summary>
    /// 
    /// </summary>
    public string backgroundUrl;
    /// <summary>
    /// 
    /// </summary>
    public int authority;
    /// <summary>
    /// 
    /// </summary>
    public string mutual;
    /// <summary>
    /// 
    /// </summary>
    public string expertTags;
    /// <summary>
    /// 
    /// </summary>
    public string experts;
    /// <summary>
    /// 
    /// </summary>
    public int djStatus;
    /// <summary>
    /// 
    /// </summary>
    public int vipType;
    /// <summary>
    /// 
    /// </summary>
    public string remarkName;
    /// <summary>
    /// 
    /// </summary>
    public int authenticationTypes;
    /// <summary>
    /// 
    /// </summary>
    public AvatarDetail avatarDetail;
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgIdStr;
    /// <summary>
    /// 
    /// </summary>
    public string backgroundImgIdStr;
    /// <summary>
    /// 
    /// </summary>
    public string anchor;
    /// <summary>
    /// 
    /// </summary>
    public string avatarImgId_str;
}

public class Sq
{
    /// <summary>
    /// 
    /// </summary>
    public int br;
    /// <summary>
    /// 
    /// </summary>
    public int fid;
    /// <summary>
    /// 
    /// </summary>
    public int size;
    /// <summary>
    /// 
    /// </summary>
    public int vd;
    /// <summary>
    /// 
    /// </summary>
    public int sr;
}

public class TracksItem
{
    /// <summary>
    /// Reunion In The Wind(重逢风中）
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public int id;
    /// <summary>
    /// 
    /// </summary>
    public int pst;
    /// <summary>
    /// 
    /// </summary>
    public int t;
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem> ar;
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia;
    /// <summary>
    /// 
    /// </summary>
    public int pop;
    /// <summary>
    /// 
    /// </summary>
    public int st;
    /// <summary>
    /// 
    /// </summary>
    public string rt;
    /// <summary>
    /// 
    /// </summary>
    public int fee;
    /// <summary>
    /// 
    /// </summary>
    public int v;
    /// <summary>
    /// 
    /// </summary>
    public string crbt;
    /// <summary>
    /// 
    /// </summary>
    public string cf;
    /// <summary>
    /// 
    /// </summary>
    public Al al;
    /// <summary>
    /// 
    /// </summary>
    public int dt;
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
    public string a;
    /// <summary>
    /// 
    /// </summary>
    public string cd;
    /// <summary>
    /// 
    /// </summary>
    public int no;
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl;
    /// <summary>
    /// 
    /// </summary>
    public int ftype;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls;
    /// <summary>
    /// 
    /// </summary>
    public int djId;
    /// <summary>
    /// 
    /// </summary>
    public int copyright;
    /// <summary>
    /// 
    /// </summary>
    public int s_id;
    /// <summary>
    /// 
    /// </summary>
    public int mark;
    /// <summary>
    /// 
    /// </summary>
    public int originCoverType;

    /// <summary>
    /// 
    /// </summary>
    public string originSongSimpleData;
    /// <summary>
    /// 
    /// </summary>
    public string tagPicList;
    /// <summary>
    /// 
    /// </summary>
    public bool resourceState;
    /// <summary>
    /// 
    /// </summary>
    public int version;
    /// <summary>
    /// 
    /// </summary>
    public string songJumpInfo;
    /// <summary>
    /// 
    /// </summary>
    public string entertainmentTags;
    /// <summary>
    /// 
    /// </summary>
    public int single;
    /// <summary>
    /// 
    /// </summary>
    public string noCopyrightRcmd;
    /// <summary>
    /// 
    /// </summary>
    public string rurl;
    /// <summary>
    /// 
    /// </summary>
    public int mst;
    /// <summary>
    /// 
    /// </summary>
    public int cp;
    /// <summary>
    /// 
    /// </summary>
    public int mv;
    /// <summary>
    /// 
    /// </summary>
    public int rtype;
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
    public int id;
    /// <summary>
    /// 
    /// </summary>
    public int v;
    /// <summary>
    /// 
    /// </summary>
    public int t;
    /// <summary>
    /// 
    /// </summary>
    public int at;
    /// <summary>
    /// 
    /// </summary>
    public string alg;
    /// <summary>
    /// 
    /// </summary>
    public int uid;
    /// <summary>
    /// 
    /// </summary>
    public string rcmdReason;
    /// <summary>
    /// 
    /// </summary>
    public string sc;
    /// <summary>
    /// 
    /// </summary>
    public string f;
    /// <summary>
    /// 
    /// </summary>
    public string sr;
}

public class Playlist
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 睡觉听的纯音乐（与君梦中相会）
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public long coverImgId;
    /// <summary>
    /// 
    /// </summary>
    public string coverImgUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string coverImgId_str;
    /// <summary>
    /// 
    /// </summary>
    public int adType;
    /// <summary>
    /// 
    /// </summary>
    public long userId;
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
    public int playCount;
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
    public bool ordered;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public List<string> tags;
    /// <summary>
    /// 
    /// </summary>
    public string updateFrequency;
    /// <summary>
    /// 
    /// </summary>
    public int backgroundCoverId;
    /// <summary>
    /// 
    /// </summary>
    public string backgroundCoverUrl;
    /// <summary>
    /// 
    /// </summary>
    public int titleImage;
    /// <summary>
    /// 
    /// </summary>
    public string titleImageUrl;
    /// <summary>
    /// 
    /// </summary>
    public string englishTitle;
    /// <summary>
    /// 
    /// </summary>
    public string officialPlaylistType;
    /// <summary>
    /// 
    /// </summary>
    public bool copied;
    /// <summary>
    /// 
    /// </summary>
    public string relateResType;
    /// <summary>
    /// 
    /// </summary>
    public List<SubscribersItem> subscribers;
    /// <summary>
    /// 
    /// </summary>
    public bool subscribed;
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
    public string videoIds;
    /// <summary>
    /// 
    /// </summary>
    public string videos;
    /// <summary>
    /// 
    /// </summary>
    public List<TrackIdsItem> trackIds;
    /// <summary>
    /// 
    /// </summary>
    public string bannedTrackIds;
    /// <summary>
    /// 
    /// </summary>
    public string mvResourceInfos;
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
    public string remixVideo;
    /// <summary>
    /// 
    /// </summary>
    public string sharedUsers;
    /// <summary>
    /// 
    /// </summary>
    public string historySharedUsers;
    /// <summary>
    /// 
    /// </summary>
    public string gradeStatus;
    /// <summary>
    /// 
    /// </summary>
    public string score;
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
    public string resConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string userConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string listenType;
}

public class ChargeInfoListItems
{
    /// <summary>
    /// 
    /// </summary>
    public int rate;
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl;
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage;
    /// <summary>
    /// 
    /// </summary>
    public int chargeType;
}

public class PrivilegesItems
{
    /// <summary>
    /// 
    /// </summary>
    public int id;
    /// <summary>
    /// 
    /// </summary>
    public int fee;
    /// <summary>
    /// 
    /// </summary>
    public int payed;
    /// <summary>
    /// 
    /// </summary>
    public int realPayed;
    /// <summary>
    /// 
    /// </summary>
    public int st;
    /// <summary>
    /// 
    /// </summary>
    public int pl;
    /// <summary>
    /// 
    /// </summary>
    public int dl;
    /// <summary>
    /// 
    /// </summary>
    public int sp;
    /// <summary>
    /// 
    /// </summary>
    public int cp;
    /// <summary>
    /// 
    /// </summary>
    public int subp;
    /// <summary>
    /// 
    /// </summary>
    public string cs;
    /// <summary>
    /// 
    /// </summary>
    public int maxbr;
    /// <summary>
    /// 
    /// </summary>
    public int fl;
    /// <summary>
    /// 
    /// </summary>
    public string pc;
    /// <summary>
    /// 
    /// </summary>
    public string toast;
    /// <summary>
    /// 
    /// </summary>
    public int flag;
    /// <summary>
    /// 
    /// </summary>
    public string paidBigBang;
    /// <summary>
    /// 
    /// </summary>
    public string preSell;
    /// <summary>
    /// 
    /// </summary>
    public int playMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public int downloadMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string plLevel;
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel;
    /// <summary>
    /// 
    /// </summary>
    public string flLevel;
    /// <summary>
    /// 
    /// </summary>
    public string rscl;
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivileges freeTrialPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItems> chargeInfoList;
}

public class GedanDetail
{
    /// <summary>
    /// 
    /// </summary>
    public long code;
    /// <summary>
    /// 
    /// </summary>
    public string relatedVideos;
    /// <summary>
    /// 
    /// </summary>
    public Playlist playlist { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string urls;
    /// <summary>
    /// 
    /// </summary>
    public List<PrivilegesItems> privileges;
    /// <summary>
    /// 
    /// </summary>
    public string sharedPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public string resEntrance;
    /// <summary>
    /// 
    /// </summary>
    public string fromUsers;
    /// <summary>
    /// 
    /// </summary>
    public int fromUserCount;
    /// <summary>
    /// 
    /// </summary>
    public string songFromUsers;
}


public class musicCheck
{
    /// <summary>
    /// 
    /// </summary>
    public bool success;
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
    public int id;
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns;
    /// <summary>
    /// 
    /// </summary>
    public List<string> @alias;
}

public class Al1
{
    /// <summary>
    /// 
    /// </summary>
    public int id;
    /// <summary>
    /// 海阔天空
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string picUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> tns;
    /// <summary>
    /// 
    /// </summary>
    public string pic_str;
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
    public int br;
    /// <summary>
    /// 
    /// </summary>
    public int fid;
    /// <summary>
    /// 
    /// </summary>
    public int size;
    /// <summary>
    /// 
    /// </summary>
    public int vd;
    /// <summary>
    /// 
    /// </summary>
    public int sr;
}

public class M1
{
    /// <summary>
    /// 
    /// </summary>
    public int br;
    /// <summary>
    /// 
    /// </summary>
    public int fid;
    /// <summary>
    /// 
    /// </summary>
    public int size;
    /// <summary>
    /// 
    /// </summary>
    public int vd;
    /// <summary>
    /// 
    /// </summary>
    public int sr;
}

public class L1
{
    /// <summary>
    /// 
    /// </summary>
    public int br;
    /// <summary>
    /// 
    /// </summary>
    public int fid;
    /// <summary>
    /// 
    /// </summary>
    public int size;
    /// <summary>
    /// 
    /// </summary>
    public int vd;
    /// <summary>
    /// 
    /// </summary>
    public int sr;
}

public class Sq1
{
    /// <summary>
    /// 
    /// </summary>
    public int br;
    /// <summary>
    /// 
    /// </summary>
    public int fid;
    /// <summary>
    /// 
    /// </summary>
    public int size;
    /// <summary>
    /// 
    /// </summary>
    public int vd;
    /// <summary>
    /// 
    /// </summary>
    public int sr;
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
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public int pst;
    /// <summary>
    /// 
    /// </summary>
    public int t;
    /// <summary>
    /// 
    /// </summary>
    public List<ArItem1> ar;
    /// <summary>
    /// 
    /// </summary>
    public List<string> alia;
    /// <summary>
    /// 
    /// </summary>
    public int pop;
    /// <summary>
    /// 
    /// </summary>
    public int st;
    /// <summary>
    /// 
    /// </summary>
    public string rt;
    /// <summary>
    /// 
    /// </summary>
    public int fee;
    /// <summary>
    /// 
    /// </summary>
    public int v;
    /// <summary>
    /// 
    /// </summary>
    public string crbt;
    /// <summary>
    /// 
    /// </summary>
    public string cf;
    /// <summary>
    /// 
    /// </summary>
    public Al1 al { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int dt;
    /// <summary>
    /// 
    /// </summary>
    public H1 h;
    /// <summary>
    /// 
    /// </summary>
    public M1 m;
    /// <summary>
    /// 
    /// </summary>
    public L1 l;
    /// <summary>
    /// 
    /// </summary>
    public Sq1 sq;

    /// <summary>
    /// 
    /// </summary>
    public string? hr;
    /// <summary>
    /// 
    /// </summary>
    public string a;
    /// <summary>
    /// 
    /// </summary>
    public string cd;
    /// <summary>
    /// 
    /// </summary>
    public int no;
    /// <summary>
    /// 
    /// </summary>
    public string rtUrl;
    /// <summary>
    /// 
    /// </summary>
    public int ftype;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rtUrls;
    /// <summary>
    /// 
    /// </summary>
    public long djId;
    /// <summary>
    /// 
    /// </summary>
    public long copyright;
    /// <summary>
    /// 
    /// </summary>
    public long s_id;
    /// <summary>
    /// 
    /// </summary>
    public long mark;
    /// <summary>
    /// 
    /// </summary>
    public int originCoverType;
    /// <summary>
    /// 
    /// </summary>
    public string originSongSimpleData;
    /// <summary>
    /// 
    /// </summary>
    public string tagPicList;
    /// <summary>
    /// 
    /// </summary>
    public string resourceState;
    /// <summary>
    /// 
    /// </summary>
    public int version;
    /// <summary>
    /// 
    /// </summary>
    public string songJumpInfo;
    /// <summary>
    /// 
    /// </summary>
    public string entertainmentTags;
    /// <summary>
    /// 
    /// </summary>
    public string awardTags;
    /// <summary>
    /// 
    /// </summary>
    public int single;
    /// <summary>
    /// 
    /// </summary>
    public string noCopyrightRcmd;
    /// <summary>
    /// 
    /// </summary>
    public int mv;
    /// <summary>
    /// 
    /// </summary>
    public int mst;
    /// <summary>
    /// 
    /// </summary>
    public int cp;
    /// <summary>
    /// 
    /// </summary>
    public int rtype;
    /// <summary>
    /// 
    /// </summary>
    public string rurl;
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
    public string resConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string userConsumable;
    /// <summary>
    /// 
    /// </summary>
    public string listenType;
}

public class ChargeInfoListItem1
{
    /// <summary>
    /// 
    /// </summary>
    public long rate;
    /// <summary>
    /// 
    /// </summary>
    public string chargeUrl;
    /// <summary>
    /// 
    /// </summary>
    public string chargeMessage;
    /// <summary>
    /// 
    /// </summary>
    public int chargeType;
}

public class PrivilegesItem1
{
    /// <summary>
    /// 
    /// </summary>
    public long id;
    /// <summary>
    /// 
    /// </summary>
    public int fee;
    /// <summary>
    /// 
    /// </summary>
    public int payed;
    /// <summary>
    /// 
    /// </summary>
    public int st;
    /// <summary>
    /// 
    /// </summary>
    public int pl;
    /// <summary>
    /// 
    /// </summary>
    public int dl;
    /// <summary>
    /// 
    /// </summary>
    public int sp;
    /// <summary>
    /// 
    /// </summary>
    public int cp;
    /// <summary>
    /// 
    /// </summary>
    public int subp;
    /// <summary>
    /// 
    /// </summary>
    public string cs;
    /// <summary>
    /// 
    /// </summary>
    public int maxbr;
    /// <summary>
    /// 
    /// </summary>
    public int fl;
    /// <summary>
    /// 
    /// </summary>
    public string toast;
    /// <summary>
    /// 
    /// </summary>
    public int flag;
    /// <summary>
    /// 
    /// </summary>
    public string preSell;
    /// <summary>
    /// 
    /// </summary>
    public int playMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public int downloadMaxbr;
    /// <summary>
    /// 
    /// </summary>
    public string maxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string playMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string downloadMaxBrLevel;
    /// <summary>
    /// 
    /// </summary>
    public string plLevel;
    /// <summary>
    /// 
    /// </summary>
    public string dlLevel;
    /// <summary>
    /// 
    /// </summary>
    public string flLevel;
    /// <summary>
    /// 
    /// </summary>
    public string rscl;
    /// <summary>
    /// 
    /// </summary>
    public FreeTrialPrivilege1 freeTrialPrivilege;
    /// <summary>
    /// 
    /// </summary>
    public List<ChargeInfoListItem1> chargeInfoList;
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
    public int code;
}

class IniFile   // ini设置文件读取
{
    string Path;
    string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(string IniPath = null)
    {
        Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
    }

    public string Read(string Key, string Section = null)
    {
        var RetVal = new StringBuilder(6000);
        GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 6000, Path);
        return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section).Length > 0;
    }
}

public class YunPlgun : IBotPlugin /* or ICorePlugin */
{
    static IniFile MyIni = new IniFile("plugins/YunMusicSettings.ini");
    PlayManager playManager;
    Ts3Client ts3Client;
    InvokerData invoker;
    PlaylistManager playlistManager;
    ResolveContext resourceFactory;
    private TsFullClient tsClient;
    PlayManager tempplayManager;
    InvokerData tempinvoker;
    public static string cookies1;
    public static bool isEventnotadded = true;
    public static int playMode;
    public static int randomPotsition = 0;
    Player player;
    public static ArrayList SongQueue = new ArrayList();
    SemaphoreSlim slimlock = new SemaphoreSlim(1, 1);
    public static string WangYiYunAPI_Address;
    public async void Initialize()
    {
        this.playManager = playManager;
        this.ts3Client = ts3Client;
        this.playlistManager = playlistManager;
        this.invoker = invoker;
        this.resourceFactory = resourceFactory;
        this.tsClient = tsClient;
        this.player = player;
        playMode = int.Parse(MyIni.Read("playMode"));
        cookies1 = MyIni.Read("cookies1");
        WangYiYunAPI_Address = MyIni.Read("WangYiYunAPI_Address");
        Console.WriteLine("Yun Plugin loaded");
        Console.WriteLine(playMode);
        Console.WriteLine(cookies1);
        Console.WriteLine(WangYiYunAPI_Address);
    }

    public PlayManager GetplayManager()
    {
        return this.tempplayManager;
    }

    public void setPlplayManager(PlayManager playManager)
    {
        this.tempplayManager = playManager;
    }
    public InvokerData getinvoker()
    {
        return this.tempinvoker;
    }

    public void setInvoker(InvokerData invoker)
    {
        this.tempinvoker = invoker;
    }

    public async Task AudioService_Playstoped(object? sender, EventArgs e) //当上一首音乐播放完触发
    {
        await slimlock.WaitAsync();
        try
        {
            if (playMode == 0)
            {
                var invoker = getinvoker();
                var playManager = GetplayManager();
                if (SongQueue.Count == 0)
                {
                    return;
                }

                SongQueue.RemoveAt(0);
                string nextsong = (string)SongQueue[0];
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
            }

            if (playMode == 1)
            {
                var invoker = getinvoker();
                var playManager = GetplayManager();
                if (SongQueue.Count == 0)
                {
                    return;
                }
                string prevSong = (string)SongQueue[0];
                SongQueue.RemoveAt(0);
                string nextsong = (string)SongQueue[0];
                SongQueue.Add(prevSong);
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
            }

            if (playMode == 2)
            {
                Random ran = new Random();
                var invoker = getinvoker();
                var playManager = GetplayManager();
                if (SongQueue.Count == 0)
                {
                    return;
                }
                string prevSong = (string)SongQueue[randomPotsition];
                SongQueue.RemoveAt(randomPotsition);
                randomPotsition = ran.Next(0, SongQueue.Count - 1);
                string nextsong = (string)SongQueue[randomPotsition];
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
            }

            if (playMode == 3)
            {
                Random ran = new Random();
                var invoker = getinvoker();
                var playManager = GetplayManager();
                if (SongQueue.Count == 0)
                {
                    return;
                }
                string prevSong = (string)SongQueue[randomPotsition];
                SongQueue.RemoveAt(randomPotsition);
                randomPotsition = ran.Next(0, SongQueue.Count - 1);
                string nextsong = (string)SongQueue[randomPotsition];
                SongQueue.Add(prevSong);
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
            }
        }
        finally
        {
            slimlock.Release();
        }
    }

    [Command("yun mode")]
    public async Task<string> playmode(int mode)
    {
        playMode = mode;
        MyIni.Write("playMode", mode.ToString());
        if (mode == 0)
        {
            return ("当前播放模式为顺序播放");
        }

        else if (mode == 1)
        {
            return ("当前播放模式为顺序循环");
        }

        else if (mode == 2)
        {
            return ("当前播放模式为随机播放");
        }

        else if (mode == 3)
        {
            return ("当前播放模式为随机循环");
        }

        else
        {
            return ("请输入正确的播放模式");
        }
    }

    [Command("yun gedanid")]
    public async Task<string> playgedan(long id, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        if (isEventnotadded)
        {
            player.OnSongEnd += AudioService_Playstoped;
            Console.WriteLine("event added");
            isEventnotadded = false;
        }
        SongQueue.Clear();
        setInvoker(invoker);
        setPlplayManager(playManager);
        string strid = id.ToString();
        string url = WangYiYunAPI_Address +"/playlist/detail?id=" + strid;
        string json = HttpGet(url);
        GedanDetail gedanDetail = JsonSerializer.Deserialize<GedanDetail>(json);
        string gedanname = gedanDetail.playlist.name;
        string imgurl = gedanDetail.playlist.coverImgUrl;
        await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
        await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
        await genList(id, SongQueue, ts3Client);
        string firstmusicid;
        if (playMode == 2 || playMode == 3)
        {
            Random ran = new Random();
            randomPotsition = ran.Next(0, SongQueue.Count - 1);
            firstmusicid = (string)SongQueue[randomPotsition];
        }
        else
        {
            randomPotsition = 0;
            firstmusicid = (string)SongQueue[randomPotsition];
        }
        SongQueue.RemoveAt(randomPotsition);
        string musicurl = getMusicUrl(firstmusicid, true);
        Console.WriteLine(firstmusicid);
        await MainCommands.CommandPlay(playManager, invoker, musicurl);
        ts3Client.SendChannelMessage("正在播放音乐：" + getMusicName(firstmusicid));
        return ("开始播放歌单");
    }
   
    [Command("yun gedan")]
    public async Task<string> CommandGedan(string name, PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client, Player player)
    {
        string urlSearch = WangYiYunAPI_Address + "/search?keywords=" + name + "&limit=1&type=1000";
        string json = HttpGet(urlSearch);
        SearchGedan searchgedan = JsonSerializer.Deserialize<SearchGedan>(json);
        long gedanid = searchgedan.result.playlists[0].id;
        if (isEventnotadded)
        {
            player.OnSongEnd += AudioService_Playstoped;
            Console.WriteLine("event added");
            isEventnotadded = false;
        }
        SongQueue.Clear();
        setInvoker(invoker);
        setPlplayManager(playManager);
        string strid = gedanid.ToString();
        string url = WangYiYunAPI_Address + "/playlist/detail?id=" + strid;
        string jsons = HttpGet(url);
        GedanDetail gedanDetail = JsonSerializer.Deserialize<GedanDetail>(jsons);
        string gedanname = gedanDetail.playlist.name;
        string imgurl = gedanDetail.playlist.coverImgUrl;
        await MainCommands.CommandBotDescriptionSet(ts3Client, gedanname);
        await MainCommands.CommandBotAvatarSet(ts3Client, imgurl);
        await genList(gedanid, SongQueue, ts3Client);
        string firstmusicid;
        if (playMode == 2 || playMode == 3)
        {
            Random ran = new Random();
            randomPotsition = ran.Next(0, SongQueue.Count - 1);
            firstmusicid = (string)SongQueue[randomPotsition];
        }
        else
        {
            randomPotsition = 0;
            firstmusicid = (string)SongQueue[randomPotsition];
        }
        SongQueue.RemoveAt(randomPotsition);
        string musicurl = getMusicUrl(firstmusicid, true);
        Console.WriteLine(firstmusicid);
        await MainCommands.CommandPlay(playManager, invoker, musicurl);
        ts3Client.SendChannelMessage("正在播放音乐：" + getMusicName(firstmusicid));
        return ("开始播放歌单");
    }

    [Command("yun play")]
    public static async Task<string> CommandYunPlay(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string urlSearch = WangYiYunAPI_Address + "/search?keywords=" + arguments + "&limit=1";
        string Searchjson = HttpGet(urlSearch);
        yunSearchSong? yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
        long firstmusicid = yunSearchSong.result.songs[0].id;
        string firstmusicname = yunSearchSong.result.songs[0].name;
        Console.WriteLine(firstmusicid + firstmusicname);
        string musicurl = getMusicUrl(firstmusicid, true);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + firstmusicid.ToString();
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

    [Command("yun playid")]
    public static async Task<string> CommandYunPlayId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string musicurl = getMusicUrl(arguments, true);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + arguments;
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

    [Command("yun add")]
    public static async Task<string> CommandYunAdd(string arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string urlSearch = WangYiYunAPI_Address + "/search?keywords=" + arguments + "&limit=1";
        string Searchjson = HttpGet(urlSearch);
        yunSearchSong? yunSearchSong = JsonSerializer.Deserialize<yunSearchSong>(Searchjson);
        long firstmusicid = yunSearchSong.result.songs[0].id;
        string firstmusicname = yunSearchSong.result.songs[0].name;
        Console.WriteLine(firstmusicid + firstmusicname);
        string musicurl = getMusicUrl(firstmusicid, true);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + firstmusicid.ToString();
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


    [Command("yun addid")]
    public static async Task<string> CommandYunAddId(long arguments, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        string musicurl = getMusicUrl(arguments, true);
        Console.WriteLine(musicurl);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + arguments;
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
    
    [Command("yun next")]
    public async Task<string> CommandYunNext(PlaylistManager playlistManager, ResolveContext resourceFactory, PlayManager playManager, InvokerData invoker, Ts3Client ts3Client)
    {
        if (playManager.IsPlaying && SongQueue.Count >= 1)
        {
            await playManager.Stop();
            if (SongQueue.Count == 0)
            {
                return ("播放列表为空");
            }

            if (playMode == 2 || playMode == 3)
            {
                Random ran = new Random();
                randomPotsition = ran.Next(0, SongQueue.Count - 1);
                string nextsong = (string)SongQueue[randomPotsition];
                SongQueue.RemoveAt(randomPotsition);
                if (playMode == 3)
                {
                    SongQueue.Add(nextsong);
                }
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
                return ("开始播放下一首音乐");
            }
            else
            {
                string nextsong = (string)SongQueue[0];
                SongQueue.RemoveAt(0);
                if (playMode == 1)
                {
                    SongQueue.Add(nextsong);
                }
                Console.WriteLine(SongQueue.Count.ToString());
                Console.WriteLine(nextsong);
                string musicurl = getMusicUrl(nextsong, true);
                string musicname = getMusicName(nextsong);
                await ts3Client.SendChannelMessage(musicname);
                await MainCommands.CommandPlay(playManager, invoker, musicurl);
                return ("开始播放下一首音乐");
            }
        }
        else
        {
            return ("无法播放下一首音乐");
        }
    }

    [Command("yun login")]
    public static async Task<string> CommanloginAsync(Ts3Client ts3Client, TsFullClient tsClient)
    {
        string url1 = WangYiYunAPI_Address + "/login/qr/key" + "?timestamp=" + GetTimeStamp();
        string json1 = HttpGet(url1);
        Console.WriteLine(json1);
        LoginKey loginKey = JsonSerializer.Deserialize<LoginKey>(json1);
        string key = loginKey.data.unikey;
        string url2 = WangYiYunAPI_Address + "/login/qr/create?key=" + key + "&qrimg=true&timestamp=" + GetTimeStamp();
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
            string url3 = WangYiYunAPI_Address + "/login/qr/check?key=" + key + "&timestamp=" + GetTimeStamp();
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
        MyIni.Write("cookies1", cookies1);
        return result;
    }

//以下全是功能性函数
    public static string getMusicUrl(long id, bool usingcookie = false) //获得歌曲URL
    {
        string cookie = getCookies();
        string url;
        if (usingcookie && cookie != "")
        {
            url = WangYiYunAPI_Address + "/song/url?id=" + id.ToString() + "&cookie=" + cookie;
        }
        else
        {
            url = WangYiYunAPI_Address + "/song/url?id=" + id.ToString();
        }
        string musicurljson = HttpGet(url);
        musicURL musicurl = JsonSerializer.Deserialize<musicURL>(musicurljson);
        long code = musicurl.code;
        string mp3 = musicurl.data[0].url;
        return mp3;
    }

    public static string getMusicUrl(string id, bool usingcookie = false)//获得歌曲URL
    {
        string cookie = getCookies();
        string url;
        if (usingcookie && cookie != "")
        {
            url = WangYiYunAPI_Address + "/song/url?id=" + id + "&cookie=" + cookie;
        }
        else
        {
            url = WangYiYunAPI_Address + "/song/url?id=" + id;
        }
        string musicurljson = HttpGet(url);
        musicURL musicurl = JsonSerializer.Deserialize<musicURL>(musicurljson);
        string mp3 = musicurl.data[0].url;
        return mp3;
    }

    public static string getMusicName(long arguments)//获得歌曲名称
    {
        string musicurl = getMusicUrl(arguments, true);
        Console.WriteLine(musicurl);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + arguments;
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicname = musicDetail.songs[0].name;
        return musicname;
    }

    public static string getMusicName(string arguments)//获得歌曲名称
    {
        string musicurl = getMusicUrl(arguments, true);
        Console.WriteLine(musicurl);
        string musicdetailurl = WangYiYunAPI_Address + "/song/detail?ids=" + arguments;
        string musicdetailjson = HttpGet(musicdetailurl);
        MusicDetail musicDetail = JsonSerializer.Deserialize<MusicDetail>(musicdetailjson);
        string musicname = musicDetail.songs[0].name;
        return musicname;
    }

    public static string HttpGet(string url)//Http请求
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

    public static string getCookies() //读取cookie
    {
        return cookies1;
    }

    public static void changeCookies(string newcookies) //更改cookie
    {
        cookies1 = newcookies;
    }

    public static string GetTimeStamp() //获得时间戳
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public static async Task genList(long id, ArrayList SongQueue, Ts3Client ts3Client) //生成歌单
    {
        string gedanid = id.ToString();
        string url = WangYiYunAPI_Address + "/playlist/track/all?id=" + gedanid;
        string gedanjson = HttpGet(url);
        GeDan Gedans = JsonSerializer.Deserialize<GeDan>(gedanjson);
        long numOfSongs = Gedans.songs.Count();
        if (numOfSongs > 100)
        {
            Console.WriteLine("警告歌单过大，可能需要一定的时间生成");
        }
        for (int i = 0; i < numOfSongs; i++)
        {
            long musicid = Gedans.songs[i].id;
            if (musicid > 0)
            {
                SongQueue.Add(musicid.ToString());
            }
        }
    }


    public void Dispose() 
    {

    }
}