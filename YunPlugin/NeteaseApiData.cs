using System.Collections.Generic;

#pragma warning disable CS8632
namespace NeteaseApiData
{
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
        public string sq;
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
        public long no;
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
        public long version;
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
        public string rurl;
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
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cookie { get; set; }
    }

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
        public bool success;
        public string message;
    }

    public class SongDetail
    {
        public string id { get; set; }
        public string program_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string picUrl { get; set; }
    }
    public class JsonSongDetailSongAlbum
    {
        public string? name { get; set; }
        public string? picUrl { get; set; }
    }
    public class JsonSongDetailSongAuthor
    {
        public int? id { get; set; }
        public string? name { get; set; }
    }
    public class JsonSongDetailSong
    {
        public long id { get; set; }
        public string? name { get; set; }
        public object? noCopyrightRcmd { get; set; }
        public JsonSongDetailSongAlbum? al { get; set; }
        public JsonSongDetailSongAuthor[]? ar { get; set; }
    }
    public class JsonSongDetail
    {
        public int code { get; set; }
        public JsonSongDetailSong[]? songs { get; set; }
    }

    public class MusicCheck
    {
        public bool success { get; set; }

        public string message { get; set; }
    }

    public class RespStatus
    {
        public class Account
        {
            public bool anonimousUser { get; set; }
            public int ban { get; set; }
            public int baoyueVersion { get; set; }
            public long createTime { get; set; }
            public int donateVersion { get; set; }
            public long id { get; set; }
            public bool paidFee { get; set; }
            public int status { get; set; }
            public int tokenVersion { get; set; }
            public int type { get; set; }
            public string userName { get; set; }
            public int vipType { get; set; }
            public int whitelistAuthority { get; set; }
        }

        public class Profile
        {
            public int accountStatus { get; set; }
            public int accountType { get; set; }
            public string avatarUrl { get; set; }
            public string nickname { get; set; }
            public int userId { get; set; }
            public int userType { get; set; }

            public int vipType { get; set; }
            public long viptypeVersion { get; set; }
        }
        public class Data
        {
            public int code { get; set; }
            public Account? account { get; set; }
            public Profile? profile { get; set; }
        }
        public Data data { get; set; }
    }
}
