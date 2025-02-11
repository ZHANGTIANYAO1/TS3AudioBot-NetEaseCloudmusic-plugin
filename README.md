# TS3AudioBot-NetEaseCloudmusic-plugin  
>此插件基于Splamy/TS3AudioBot项目 https://github.com/Splamy/TS3AudioBot   
>以及网易云音乐 API开发 https://gitlab.com/Binaryify/neteasecloudmusicapi  
此插件安装方法同样见TS3AudioBot项目wiki  
**2.0版本之后可以不需要本地部署网易云API了，但是强烈建议自行部署防止隐私泄露  **  
**最好给音乐机器人超管权限保证能正常更新头像和描述**

## 关于API和机器人部署
目前网易云可能有什么改动,导致如果使用部署在海外的API将无法播放部分VIP歌曲,已经在ISSUE中添加新的国内公开API

推荐将API和机器人都部署在国内的服务器上

## 关于 DEV 版本
此版本基于 Splamy/TS3AudioBot 项目和 NeteaseCloudMusicApi 开发。DEV 版本由 @577fkj 大佬重构和增强，感谢他的贡献！

由于原开发环境的丢失，无法继续之前兼容 stable 版本的音乐机器人插件的开发。此 DEV 版本是在 @577fkj 的重构和增强版本基础上进一步开发的。

此版本新增了多项功能，包括查看播放列表、验证码登录、无人自动暂停、清除歌单、获取歌单最大长度限制、让机器人前往当前频道、播放专辑功能、私人 FM 模式等。

### 链接
- DEV 版本 GitHub 仓库：https://github.com/ZHANGTIANYAO1/TS3AudioBot-NetEaseCloudmusic-plugin/tree/DEV
- pre-release(测试版)下载链接：https://github.com/ZHANGTIANYAO1/TS3AudioBot-NetEaseCloudmusic-plugin/releases/tag/3.0.0

### 注意事项
- 此版本windows系统需要使用新的机器人程序，已经打包在pre-release中
- 虽然添加了第三方网易云API，但是为了大家的安全考虑，强烈建议自行部署API
- 需要使用最新版本的网易云API：https://gitlab.com/Binaryify/neteasecloudmusicapi
- 此版本在开发中，可能会有bug，遇到bug请提交issue，如果有想要添加的功能也可以提交issue
- 将机器人整合包的音质默认设置为了最高bitrate
- 权限文件修改为所有人有全部权限
- 使用版本过新的linux可能会有困难，比如使用Ubuntu（24，22）版本的需要自己去安装老的lib库，例子：（libicu70_70.1-2_amd64.deb libssl1.1_1.1.1f-1ubuntu2_amd64.deb）
- 推荐使用Ubuntu20

## 关于设置文件YunSettings.ini
`playMode=`是播放模式   
`WangYiYunAPI_Address`是网易云API地址，目前默认的是一个大佬的远程API，如果加载速度过慢或者无法访问，请自行部署API并修改API地址。（为了保护你的隐私强烈建议你自行部署API）   
`cookies1=`是保存在你本地的身份验证，通过二维码登录获取。（不需要修改）   

## 目前的指令：
正在播放的歌单的图片和名称可以点机器人看它的头像和描述  
vip音乐想要先登陆才能播放完整版本:（输入指令后扫描机器人头像二维码登陆)  
`!yun login`  

双击机器人，目前有以下指令（把[xxx]替换成对应信息，**包括中括号**）  
1.立即播放网易云音乐  
`!yun play [音乐名称]`  
  
2.添加音乐到下一首  
`!yun add [音乐名称]`  
  
3.播放网易云音乐歌单(如果提示Error: Nothing to play...重新输入指令解决)  
`!yun gedan [歌单名称]`  
  
4.播放网易云音乐歌单id  
`!yun gedanid [歌单名称]`  
  
5.立即播放网易云音乐id  
`!yun playid [歌单id]`  
  
6.添加指定音乐id到下一首  
`!yun add [音乐id]`  
  
7.播放列表中的下一首    
`!yun next`  

8.修改播放模式    
`!yun mode [模式选择数字0-3]`  
`0 = 顺序播放`
`1 = 顺序循环`
`2 = 随机播放`
`3 = 随机循环`

需要注意的是如果歌单歌曲过多需要时间加载，期间一定一定不要输入其他指令  

### TS频道描述（复制代码到频道描述）
```
[COLOR=#ff5500][B]正在播放的歌单的图片和名称可以点机器人看它的头像和描述[/B][/COLOR]
[COLOR=#aa00ff]机器人现在可以通过歌单播放vip音乐，如果遇到其他问题可以联系Github[/COLOR]

[COLOR=#0055ff]双击机器人，目前有以下指令（[I]把[xxx]替换成对应信息，包括中括号[/I]）[/COLOR]
1.立即播放网易云音乐
[COLOR=#00aa00]!yun play [音乐名称][/COLOR]
2.添加音乐到下一首
[COLOR=#00aa00]!yun add [音乐名称][/COLOR]
3.播放网易云音乐歌单
[COLOR=#00aa00]!yun gedan [歌单名称][/COLOR]
4.播放网易云音乐歌单id
[COLOR=#00aa00]!yun gedanid [歌单名称][/COLOR]
5.立即播放网易云音乐id
[COLOR=#00aa00]!yun playid [歌单id][/COLOR]
6.添加指定音乐id到下一首
[COLOR=#00aa00]!yun add [音乐id][/COLOR]
7.播放列表中的下一首
[COLOR=#00aa00]!yun next[/COLOR]
8.播放模式选择【0=顺序播放 1=顺序循环 2=随机 3=随即循环】
[COLOR=#00aa00]!yun mode[/COLOR]
9.登陆账户
[COLOR=#00aa00]!yun login[/COLOR]
[COLOR=#aaaaff]如果想要播放会员音乐需要先登陆会员账户，输入上述命令后扫描机器人头像的二维码登陆（只需要一账户登陆一次即可）[/COLOR]
需要注意的是如果歌单歌曲过多需要时间加载（重写后应该只需要几秒），期间[B]一定一定不要[/B]输入其他指令

以下例子加粗的就是音乐或者歌单id
[URL]https://music.163.com/#/my/m/music/playlist?id=[B]2139305008[I][/I][/B][/URL]

```

### 已知问题  
使用官方构建的ts3aduiobot会报错，因为官方的编译环境有问题。  
解决方法：1.自行构建 2.使用我打包的版本  

重复卸载加载插件会出现问题，如果需要重新加载请先重启Bot

Linux Docker无法正常显示description
解决方法：docker版本启用了web管理界面，在里面的Show song in bot description冲突了。关掉就可以正常显示了

#### 写在最后的话
本人完全不会C#，纯粹是自己花两天自学的，所以请各位大佬轻点喷我。  
还有很多地方需要完善以及很多功能可以添加，但是本人无法做出任何承诺  
同时欢迎各位来修改和完善这个插件  
最后的最后请不要轻易信任使用他人提供的公开服务，以免发生安全问题,泄露自己的账号和密码。如果决定使用默认的远程API请自行承担任何可能的后续风险。
