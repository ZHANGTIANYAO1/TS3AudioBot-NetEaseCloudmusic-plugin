# TS3AudioBot-NetEaseCloudmusic-plugin

> 此插件基于 Splamy/TS3AudioBot 项目 https://github.com/Splamy/TS3AudioBot  
> 以及网易云音乐 API 开发https://github.com/Binaryify/NeteaseCloudMusicApi  
> 此插件安装方法同样见 TS3AudioBot 项目 wiki  
> **2.0 版本之后可以不需要本地部署网易云 API 了，但是强烈建议自行部署防止隐私泄露 **  
> **最好给音乐机器人超管权限保证能正常更新头像和描述**

## 关于 2.0 版本

似乎在播放列表的 bug 应该已经修复了，如果还有问题请 github 创建 issue。

## 关于设置文件 YunSettings.ini

`playMode=`是播放模式  
`WangYiYunAPI_Address`是网易云 API 地址，目前默认的是一个大佬的远程 API，如果加载速度过慢或者无法访问，请自行部署 API 并修改 API 地址。（为了保护你的隐私强烈建议你自行部署 API）  
`cookies1=`是保存在你本地的身份验证，通过二维码登录获取。（不需要修改）

## 目前的指令：

正在播放的歌单的图片和名称可以点机器人看它的头像和描述  
vip 音乐想要先登陆才能播放完整版本:（输入指令后扫描机器人头像二维码登陆)  
`!yun login`

双击机器人，目前有以下指令（把[xxx]替换成对应信息，**包括中括号**）  
1.立即播放网易云音乐  
`!yun play [音乐名称/音乐网址/音乐ID]`

2.添加音乐到下一首  
`!yun add [音乐名称/音乐网址/音乐ID]`

3.播放网易云音乐歌单
`!yun gedan [歌单名称/歌单网址/歌单ID]`

4.播放列表中的下一首  
`!yun next`

5.修改播放模式  
`!yun mode [模式选择数字0-3]`  
`0 = 顺序播放`
`1 = 顺序循环`
`2 = 随机播放`
`3 = 随机循环`

6.查看播放列表
`!yun list`

7.查看状态
`!yun status`

需要注意的是如果歌单歌曲过多需要时间加载，期间一定一定不要输入其他指令

### TS 频道描述（复制代码到频道描述）

```
[COLOR=#ff5500][B]正在播放的歌单的图片和名称可以点机器人看它的头像和描述[/B][/COLOR]
[COLOR=#aa00ff]机器人现在可以通过歌单播放vip音乐，如果遇到其他问题可以联系Github[/COLOR]

[COLOR=#0055ff]双击机器人，目前有以下指令（[I]把[xxx]替换成对应信息，包括中括号[/I]）[/COLOR]
1.立即播放网易云音乐
[COLOR=#00aa00]!yun play [音乐名称/音乐网址/音乐ID][/COLOR]
2.添加音乐到下一首
[COLOR=#00aa00]!yun add [音乐名称/音乐网址/音乐ID][/COLOR]
3.播放网易云音乐歌单
[COLOR=#00aa00]!yun gedan [歌单名称/歌单网址/歌单ID][/COLOR]
4.播放列表中的下一首
[COLOR=#00aa00]!yun next[/COLOR]
5.播放模式选择【0=顺序播放 1=顺序循环 2=随机 3=随即循环】
[COLOR=#00aa00]!yun mode[/COLOR]
6.登陆账户
[COLOR=#00aa00]!yun login[/COLOR]
7.查看播放列表
[COLOR=#00aa00]!yun list[/COLOR]
8.查看状态
[COLOR=#00aa00]!yun status[/COLOR]
[COLOR=#aaaaff]如果想要播放会员音乐需要先登陆会员账户，输入上述命令后扫描机器人头像的二维码登陆（只需要一账户登陆一次即可）[/COLOR]
需要注意的是如果歌单歌曲过多需要时间加载（重写后应该只需要几秒），期间[B]一定一定不要[/B]输入其他指令
```
