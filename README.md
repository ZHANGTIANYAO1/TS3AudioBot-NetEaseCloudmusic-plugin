# TS3AudioBot-NetEaseCloudmusic-plugin  
>此插件基于Splamy/TS3AudioBot项目 https://github.com/Splamy/TS3AudioBot   
>以及网易云音乐 API开发https://github.com/Binaryify/NeteaseCloudMusicApi  
>若是想要使用此插件，请先根据这两个项目的wiki安装到和ts服务器本地  
此插件安装方法同样见TS3AudioBot项目wiki  
**最好给音乐机器人超管权限保证能正常更新头像和描述**  

## 目前的指令：
正在播放的歌单的图片和名称可以点机器人看它的头像和描述  
vip音乐想要先登陆才能播放完整版本:（输入指令后扫描机器人头像二维码登陆)  
`!yun login`  

双击机器人，目前有以下指令（把[xxx]替换成对应信息，包括中括号）  
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
`!next`  
  
需要注意的是如果歌单歌曲过多需要时间加载，期间一定一定不要输入其他指令  

### 已知问题  
无法通过歌单播放vip音乐，vip音乐想要用播放单曲的指令  
#### 写在最后的话
本人完全不会C#，纯粹是自己花两天自学的，所以请各位大佬轻点喷我。  
还有很多地方需要完善以及很多功能可以添加，但是本人无法做出任何承诺  
同时欢迎各位来修改和完善这个插件  
