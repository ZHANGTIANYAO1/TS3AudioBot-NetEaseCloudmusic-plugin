# TS3AudioBot-NetEaseCloudmusic-plugin

> 此插件基于 [Splamy/TS3AudioBot](https://github.com/Splamy/TS3AudioBot) 项目以及[网易云音乐 API](https://www.npmjs.com/package/NeteaseCloudMusicApi) 开发。

这是一个用 C# 给 TS3AudioBot 编写的网易云音乐插件，让你的 TeamSpeak 服务器可以有一个音乐机器人。

## 功能特性

- 搜索并播放网易云音乐歌曲
- 播放歌单（按名称或ID）
- 多种播放模式：顺序播放、顺序循环、随机播放、随机循环
- 二维码登录网易云账户（支持 VIP 音乐播放）
- **UnblockNeteaseMusic** 支持（可选），解锁无版权音乐
- **Web 控制界面**，通过浏览器远程控制播放
- **智能图片压缩**，防止头像上传失败
- 自动跳过无法播放的歌曲
- 跨平台支持（Windows / Linux / macOS）

## 环境要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本
- [TS3AudioBot](https://github.com/Splamy/TS3AudioBot)（已包含在本仓库中）
- [NeteaseCloudMusicApi](https://www.npmjs.com/package/NeteaseCloudMusicApi)（强烈建议自行部署）

## 快速开始

### 1. 构建

```bash
git clone https://github.com/ZHANGTIANYAO1/TS3AudioBot-NetEaseCloudmusic-plugin.git
cd TS3AudioBot-NetEaseCloudmusic-plugin
dotnet build ClassLibrary4.sln -c Release
```

构建产物在 `YunBot/bin/Release/net8.0/YunBot.dll`。

### 2. 安装

将 `YunBot.dll` 复制到 TS3AudioBot 的 `plugins/` 目录下。

### 3. 配置

插件首次运行会自动生成 `plugins/YunSettings.json` 配置文件：

```json
{
  "playMode": 0,
  "apiAddress": "https://127.0.0.1:3000",
  "cookie": "",
  "unblockerEnabled": false,
  "unblockerAddress": "http://127.0.0.1:8080"
}
```

| 字段 | 说明 |
|------|------|
| `playMode` | 播放模式：0=顺序, 1=循环, 2=随机, 3=随机循环 |
| `apiAddress` | 网易云 API 地址（强烈建议自行部署以保护隐私） |
| `cookie` | 登录凭证（通过二维码登录自动获取，无需手动修改） |
| `unblockerEnabled` | 是否启用 UnblockNeteaseMusic |
| `unblockerAddress` | UnblockNeteaseMusic 服务地址 |

> 如果之前使用过旧版的 `YunSettings.ini`，插件会自动迁移到新的 JSON 格式。

## 指令列表

双击机器人，输入以下指令（把 `[xxx]` 替换成对应信息，**包括中括号**）：

### 播放控制

| 指令 | 说明 |
|------|------|
| `!yun play [音乐名称]` | 搜索并立即播放 |
| `!yun playid [音乐ID]` | 按 ID 播放 |
| `!yun add [音乐名称]` | 搜索并添加到播放列表 |
| `!yun addid [音乐ID]` | 按 ID 添加到播放列表 |
| `!yun next` | 播放下一首 |

### 歌单

| 指令 | 说明 |
|------|------|
| `!yun gedan [歌单名称]` | 搜索并播放歌单 |
| `!yun gedanid [歌单ID]` | 按 ID 播放歌单 |

### 播放列表管理

| 指令 | 说明 |
|------|------|
| `!yun list` | 查看播放列表信息 |
| `!yun clear` | 清空播放列表 |
| `!yun mode [0-3]` | 设置播放模式 |
| `!yun status` | 查看当前状态（JSON 格式） |

### 账户与设置

| 指令 | 说明 |
|------|------|
| `!yun login` | 二维码登录网易云账户 |
| `!yun api [地址]` | 修改 API 地址 |
| `!yun unblocker on/off [地址]` | 启用/禁用 UnblockNeteaseMusic |

## Web 控制界面

TS3AudioBot 自带 Web 服务器，本插件的所有指令均可通过 Web API 调用。插件内置了一个 Web 控制页面，支持：

- 歌曲/歌单搜索与播放
- 播放队列管理
- 播放模式切换
- 实时状态显示
- UnblockNeteaseMusic 设置

Web 页面通过 TS3AudioBot 的 API 接口（默认 `/api/bot/use/{botId}/yun/...`）与机器人交互。

## UnblockNeteaseMusic（可选）

本插件可选集成 [UnblockNeteaseMusic](https://github.com/UnblockNeteaseMusic/server)，用于解锁因版权限制无法播放的歌曲。**此功能默认关闭**，不部署 UnblockNeteaseMusic 不影响插件正常使用。

### 使用方法

1. 自行部署 UnblockNeteaseMusic 服务（参考其项目文档）
2. 通过指令启用：`!yun unblocker on http://127.0.0.1:8080`
3. 关闭：`!yun unblocker off`

启用后，当官方 API 无法获取歌曲链接时，插件会自动尝试通过 UnblockNeteaseMusic 获取替代音源。如果 UnblockNeteaseMusic 服务不可用，插件会正常跳过该歌曲继续播放。

## 自动构建

本项目使用 GitHub Actions 自动构建：

- **DEV 分支推送**：自动生成 beta 预发布版本
- **Tag 推送** (`v*`)：自动生成正式 Release
- 构建平台：`win-x64`、`linux-x64`、`linux-arm64`、`osx-x64`

## 项目结构

```
YunBot/                     # 插件主项目
  Models/ApiModels.cs       # API 响应模型
  Services/
    NeteaseApiClient.cs     # 网易云 API 客户端（含 UnblockNeteaseMusic 支持）
    ConfigManager.cs        # 跨平台 JSON 配置管理
    ImageProcessor.cs       # 图片压缩处理（防止头像上传失败）
  Web/index.html            # Web 控制界面
  YunPlugin.cs              # 插件主类
TS3AudioBot/                # TS3AudioBot 核心（依赖）
TSLib/                      # TeamSpeak 客户端库（依赖）
.github/workflows/          # GitHub Actions CI/CD
```

## 已知问题

- 歌单歌曲过多时加载需要一定时间，期间请勿输入其他指令
- 重复卸载/加载插件可能出现问题，如需重新加载请先重启 Bot
- Linux Docker 环境下 `description` 显示可能异常，需在 Web 管理界面关闭 "Show song in bot description"

## 致谢

- [Splamy/TS3AudioBot](https://github.com/Splamy/TS3AudioBot) - TeamSpeak 3 音乐机器人框架
- [Binaryify/NeteaseCloudMusicApi](https://www.npmjs.com/package/NeteaseCloudMusicApi) - 网易云音乐 API
- [UnblockNeteaseMusic](https://github.com/UnblockNeteaseMusic/server) - 解锁无版权音乐
- [@577fkj](https://github.com/577fkj) - DEV 版本重构与增强

## 许可

本项目使用 OSL-3.0 许可证。

> 请不要轻易信任使用他人提供的公开服务，以免发生安全问题。强烈建议自行部署 API。
