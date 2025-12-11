# Bangumi.NET

一个功能完整的 Bangumi API .NET 客户端库，使用 C# 编写，支持 .NET 8.0。

## 功能特性

- ✅ 完整的 Bangumi API v0 支持
- ✅ 强类型模型定义
- ✅ 异步/等待 (async/await) 模式
- ✅ 简单易用的 API 接口
- ✅ 支持所有主要功能模块：
  - 条目 (Subject) 搜索和查询
  - 角色 (Character) 搜索和查询
  - 人物 (Person) 搜索和查询
  - 剧集 (Episode) 查询
  - 用户 (User) 信息和收藏
  - 每日放送 (Calendar)

## 安装

### 从源码构建

```bash
git clone <repository-url>
cd Bangumi.NET
dotnet build
```

## 快速开始

### 1. 获取 API Key

访问 [Bangumi Access Token](https://next.bgm.tv/demo/access-token) 生成你的访问令牌。

### 2. 创建客户端

```csharp
using Bangumi.NET;
using Bangumi.NET.Models;

// 初始化客户端
var apiKey = "your-api-key";
var userAgent = "YourApp/1.0 (https://your-website.com)";

using var client = new BangumiClient(apiKey, userAgent);
```

### 3. 使用 API

```csharp
// 获取当前用户信息
var me = await client.GetMyselfAsync();
Console.WriteLine($"用户名: {me.Username}");

// 搜索条目
var searchRequest = new SubjectSearchRequest
{
    Keyword = "CLANNAD",
    Sort = "rank"
};
var result = await client.SearchSubjectsAsync(searchRequest, limit: 10);

// 获取条目详情
var subject = await client.GetSubjectByIdAsync(2097);
Console.WriteLine($"名称: {subject.Name}");
Console.WriteLine($"评分: {subject.Rating?.Score}");

// 获取每日放送
var calendar = await client.GetCalendarAsync();
foreach (var day in calendar)
{
    Console.WriteLine($"{day.Weekday?.Cn}:");
    foreach (var item in day.Items ?? new())
    {
        Console.WriteLine($"  - {item.Name}");
    }
}
```

## API 文档

### BangumiClient 类

#### 构造函数

```csharp
BangumiClient(string apiKey, string userAgent)
```

- `apiKey`: Bangumi API 访问令牌
- `userAgent`: User-Agent 标识（必须包含应用名称和联系方式）

#### 条目相关方法

##### SearchSubjectsAsync
搜索条目

```csharp
Task<PagedResult<Subject>> SearchSubjectsAsync(
    SubjectSearchRequest request, 
    int limit = 30, 
    int offset = 0, 
    CancellationToken cancellationToken = default)
```

**参数：**
- `request`: 搜索请求对象
  - `Keyword`: 搜索关键词（必需）
  - `Sort`: 排序方式 (`"match"`, `"heat"`, `"rank"`, `"score"`)
  - `Filter`: 过滤条件
    - `Type`: 条目类型列表
    - `Tag`: 标签列表
    - `AirDate`: 放送日期范围
    - `Rating`: 评分范围
    - `Rank`: 排名范围
    - `Nsfw`: 是否包含 NSFW 内容

**示例：**
```csharp
var request = new SubjectSearchRequest
{
    Keyword = "fate",
    Sort = "rank",
    Filter = new SubjectSearchFilter
    {
        Type = new List<int> { 2 }, // 动画
        Rating = new List<string> { ">=7", "<9" }
    }
};
var result = await client.SearchSubjectsAsync(request, limit: 20);
```

##### GetSubjectByIdAsync
获取条目详情

```csharp
Task<Subject?> GetSubjectByIdAsync(int subjectId, CancellationToken cancellationToken = default)
```

##### GetSubjectsAsync
批量获取条目信息

```csharp
Task<Dictionary<string, Subject>> GetSubjectsAsync(
    List<int> subjectIds, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedCharactersBySubjectIdAsync
获取条目相关角色

```csharp
Task<List<RelatedCharacter>> GetRelatedCharactersBySubjectIdAsync(
    int subjectId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedPersonsBySubjectIdAsync
获取条目相关人物

```csharp
Task<List<RelatedPerson>> GetRelatedPersonsBySubjectIdAsync(
    int subjectId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedSubjectsBySubjectIdAsync
获取条目关联条目

```csharp
Task<List<SubjectRelation>> GetRelatedSubjectsBySubjectIdAsync(
    int subjectId, 
    CancellationToken cancellationToken = default)
```

#### 剧集相关方法

##### GetEpisodesAsync
获取剧集列表

```csharp
Task<PagedResult<Episode>> GetEpisodesAsync(
    int subjectId, 
    int? episodeType = null, 
    int limit = 100, 
    int offset = 0, 
    CancellationToken cancellationToken = default)
```

##### GetEpisodeByIdAsync
获取剧集详情

```csharp
Task<EpisodeDetail?> GetEpisodeByIdAsync(
    int episodeId, 
    CancellationToken cancellationToken = default)
```

#### 角色相关方法

##### SearchCharactersAsync
搜索角色

```csharp
Task<PagedResult<Character>> SearchCharactersAsync(
    CharacterSearchRequest request, 
    int limit = 30, 
    int offset = 0, 
    CancellationToken cancellationToken = default)
```

##### GetCharacterByIdAsync
获取角色信息

```csharp
Task<Character?> GetCharacterByIdAsync(
    int characterId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedSubjectsByCharacterIdAsync
获取角色相关条目

```csharp
Task<List<CharacterSubject>> GetRelatedSubjectsByCharacterIdAsync(
    int characterId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedPersonsByCharacterIdAsync
获取角色相关人物

```csharp
Task<List<CharacterPerson>> GetRelatedPersonsByCharacterIdAsync(
    int characterId, 
    CancellationToken cancellationToken = default)
```

#### 人物相关方法

##### SearchPersonsAsync
搜索人物

```csharp
Task<PagedResult<Person>> SearchPersonsAsync(
    PersonSearchRequest request, 
    int limit = 30, 
    int offset = 0, 
    CancellationToken cancellationToken = default)
```

##### GetPersonByIdAsync
获取人物信息

```csharp
Task<Person?> GetPersonByIdAsync(
    int personId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedCharactersByPersonIdAsync
获取人物相关角色

```csharp
Task<List<PersonCharacter>> GetRelatedCharactersByPersonIdAsync(
    int personId, 
    CancellationToken cancellationToken = default)
```

##### GetRelatedSubjectsByPersonIdAsync
获取人物相关条目

```csharp
Task<List<PersonSubject>> GetRelatedSubjectsByPersonIdAsync(
    int personId, 
    CancellationToken cancellationToken = default)
```

#### 用户相关方法

##### GetUserByNameAsync
获取用户信息

```csharp
Task<User?> GetUserByNameAsync(
    string username, 
    CancellationToken cancellationToken = default)
```

##### GetMyselfAsync
获取当前用户信息

```csharp
Task<User?> GetMyselfAsync(CancellationToken cancellationToken = default)
```

#### 收藏相关方法

##### GetUserCollectionsAsync
获取用户收藏列表

```csharp
Task<PagedResult<UserSubjectCollection>> GetUserCollectionsAsync(
    string username, 
    int? subjectType = null, 
    int? collectionType = null, 
    int limit = 30, 
    int offset = 0, 
    CancellationToken cancellationToken = default)
```

##### GetUserCollectionAsync
获取指定条目收藏信息

```csharp
Task<UserSubjectCollection?> GetUserCollectionAsync(
    string username, 
    int subjectId, 
    CancellationToken cancellationToken = default)
```

##### UpdateUserCollectionAsync
创建或修改收藏

```csharp
Task<UserSubjectCollection?> UpdateUserCollectionAsync(
    int subjectId, 
    UserCollectionModifyRequest request, 
    CancellationToken cancellationToken = default)
```

**示例：**
```csharp
var request = new UserCollectionModifyRequest
{
    Type = 2, // 看过
    Rate = 10, // 评分 10 分
    Comment = "非常好看！",
    Tags = new List<string> { "感动", "治愈" }
};
await client.UpdateUserCollectionAsync(subjectId, request);
```

##### PatchUserCollectionAsync
更新收藏

```csharp
Task PatchUserCollectionAsync(
    int subjectId, 
    UserCollectionModifyRequest request, 
    CancellationToken cancellationToken = default)
```

##### GetUserSubjectEpisodeCollectionAsync
获取用户章节收藏

```csharp
Task<UserEpisodeCollection?> GetUserSubjectEpisodeCollectionAsync(
    string username, 
    int subjectId, 
    int? episodeType = null, 
    CancellationToken cancellationToken = default)
```

##### PatchUserSubjectEpisodeCollectionAsync
批量更新章节收藏

```csharp
Task PatchUserSubjectEpisodeCollectionAsync(
    int subjectId, 
    EpisodeCollectionBatchRequest request, 
    CancellationToken cancellationToken = default)
```

##### GetUserEpisodeCollectionAsync
获取单集收藏状态

```csharp
Task<EpisodeCollectionInfo?> GetUserEpisodeCollectionAsync(
    string username, 
    int episodeId, 
    CancellationToken cancellationToken = default)
```

##### PutUserEpisodeCollectionAsync
更新单集收藏状态

```csharp
Task PutUserEpisodeCollectionAsync(
    int episodeId, 
    EpisodeCollectionRequest request, 
    CancellationToken cancellationToken = default)
```

#### 每日放送

##### GetCalendarAsync
获取每日放送

```csharp
Task<List<CalendarItem>> GetCalendarAsync(CancellationToken cancellationToken = default)
```

### 数据模型

#### 枚举类型

##### SubjectType - 条目类型
```csharp
public enum SubjectType
{
    Book = 1,    // 书籍
    Anime = 2,   // 动画
    Music = 3,   // 音乐
    Game = 4,    // 游戏
    Real = 6     // 三次元
}
```

##### SubjectCollectionType - 收藏类型
```csharp
public enum SubjectCollectionType
{
    Wish = 1,    // 想看
    Done = 2,    // 看过
    Doing = 3,   // 在看
    OnHold = 4,  // 搁置
    Dropped = 5  // 抛弃
}
```

##### EpisodeCollectionType - 章节收藏类型
```csharp
public enum EpisodeCollectionType
{
    NotCollected = 0,  // 未收藏
    Wish = 1,          // 想看
    Done = 2,          // 看过
    Dropped = 3        // 抛弃
}
```

##### EpType - 剧集类型
```csharp
public enum EpType
{
    MainStory = 0,  // 本篇
    SP = 1,         // 特别篇
    OP = 2,         // 片头曲
    ED = 3,         // 片尾曲
    PV = 4,         // 预告/宣传/广告
    MAD = 5,        // MAD
    Other = 6       // 其他
}
```

## 运行示例

项目包含一个完整的示例程序，展示了所有主要功能的使用方法。

### 运行步骤

1. 确保 `key.txt` 文件包含有效的 API Key
2. 构建项目：
   ```bash
   dotnet build
   ```
3. 运行示例：
   ```bash
   cd Bangumi.NET.Example
   dotnet run
   ```

示例程序将依次演示：
1. 获取当前用户信息
2. 获取每日放送
3. 搜索条目
4. 获取条目详情
5. 获取条目角色
6. 获取条目人物
7. 获取剧集列表
8. 搜索角色
9. 获取角色详情
10. 获取用户收藏

## 错误处理

所有 API 方法在遇到 HTTP 错误时会抛出 `HttpRequestException`。建议使用 try-catch 进行错误处理：

```csharp
try
{
    var subject = await client.GetSubjectByIdAsync(12345);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"API 请求失败: {ex.Message}");
}
```

## 注意事项

1. **User-Agent 要求**：根据 Bangumi API 规范，User-Agent 必须包含应用名称和联系方式
2. **API Key 安全**：请勿将 API Key 提交到公共仓库
3. **请求频率**：请合理控制 API 请求频率，避免对服务器造成压力
4. **异步编程**：所有 API 方法都是异步的，请使用 `await` 关键字调用

## 依赖项

- .NET 8.0
- System.Text.Json 8.0.0

## 许可证

查看 LICENSE.txt 文件了解详情。

## 贡献

欢迎提交 Issue 和 Pull Request！

## 相关链接

- [Bangumi API 文档](https://github.com/bangumi/api)
- [Bangumi 官网](https://bgm.tv/)
- [获取 Access Token](https://next.bgm.tv/demo/access-token)