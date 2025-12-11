# Bangumi.NET 使用示例

本文档提供了 Bangumi.NET 客户端库的详细使用示例。

## 目录

- [基础设置](#基础设置)
- [条目 API](#条目-api)
- [角色 API](#角色-api)
- [人物 API](#人物-api)
- [剧集 API](#剧集-api)
- [用户 API](#用户-api)
- [收藏 API](#收藏-api)
- [每日放送 API](#每日放送-api)
- [高级用法](#高级用法)

## 基础设置

### 创建客户端实例

```csharp
using Bangumi.NET;
using Bangumi.NET.Models;

// 方法1：直接传入参数
var client = new BangumiClient(
    apiKey: "your-api-key",
    userAgent: "MyApp/1.0 (https://mywebsite.com)"
);

// 方法2：从配置文件读取
var apiKey = File.ReadAllText("key.txt").Trim();
var userAgent = "MyApp/1.0";
using var client = new BangumiClient(apiKey, userAgent);
```

### 使用 Using 语句自动释放资源

```csharp
using var client = new BangumiClient(apiKey, userAgent);
// 客户端会在作用域结束时自动释放
```

## 条目 API

### 1. 搜索条目

#### 基础搜索

```csharp
var request = new SubjectSearchRequest
{
    Keyword = "进击的巨人"
};
var result = await client.SearchSubjectsAsync(request);

Console.WriteLine($"找到 {result.Total} 个结果");
foreach (var subject in result.Data)
{
    Console.WriteLine($"{subject.Name} - 评分: {subject.Rating?.Score}");
}
```

#### 高级搜索 - 使用过滤器

```csharp
var request = new SubjectSearchRequest
{
    Keyword = "fate",
    Sort = "rank", // 按排名排序
    Filter = new SubjectSearchFilter
    {
        Type = new List<int> { 2 }, // 只搜索动画
        Rating = new List<string> { ">=8", "<10" }, // 评分 8-10
        AirDate = new List<string> { ">=2015-01-01" }, // 2015年后
        Tag = new List<string> { "战斗" } // 包含"战斗"标签
    }
};

var result = await client.SearchSubjectsAsync(request, limit: 20, offset: 0);
```

#### 搜索排序选项

```csharp
// 按匹配度排序（默认）
Sort = "match"

// 按热度排序（收藏人数）
Sort = "heat"

// 按排名排序
Sort = "rank"

// 按评分排序
Sort = "score"
```

### 2. 获取条目详情

```csharp
int subjectId = 2097; // CLANNAD
var subject = await client.GetSubjectByIdAsync(subjectId);

if (subject != null)
{
    Console.WriteLine($"名称: {subject.Name}");
    Console.WriteLine($"中文名: {subject.NameCn}");
    Console.WriteLine($"类型: {(SubjectType)subject.Type}");
    Console.WriteLine($"评分: {subject.Rating?.Score}");
    Console.WriteLine($"排名: {subject.Rating?.Rank}");
    Console.WriteLine($"总集数: {subject.TotalEpisodes}");
    Console.WriteLine($"放送日期: {subject.Date}");
    Console.WriteLine($"平台: {subject.Platform}");
    
    // 收藏统计
    var collection = subject.Collection;
    Console.WriteLine($"想看: {collection?.Wish}, 看过: {collection?.Collect}, 在看: {collection?.Doing}");
    
    // 标签
    Console.WriteLine("标签:");
    foreach (var tag in subject.Tags ?? new())
    {
        Console.WriteLine($"  {tag.Name} ({tag.Count})");
    }
}
```

### 3. 批量获取条目

```csharp
var subjectIds = new List<int> { 2097, 20, 328 };
var subjects = await client.GetSubjectsAsync(subjectIds);

foreach (var kvp in subjects)
{
    Console.WriteLine($"ID {kvp.Key}: {kvp.Value.Name}");
}
```

### 4. 获取条目关联的角色

```csharp
var characters = await client.GetRelatedCharactersBySubjectIdAsync(subjectId);

foreach (var character in characters)
{
    Console.WriteLine($"角色: {character.Name}");
    Console.WriteLine($"关系: {character.Relation}");
    
    // 声优信息
    if (character.Actors != null)
    {
        foreach (var actor in character.Actors)
        {
            Console.WriteLine($"  声优: {actor.Name}");
        }
    }
}
```

### 5. 获取条目关联的人物

```csharp
var persons = await client.GetRelatedPersonsBySubjectIdAsync(subjectId);

foreach (var person in persons)
{
    Console.WriteLine($"人物: {person.Name}");
    Console.WriteLine($"职位: {person.Relation}");
    Console.WriteLine($"职业: {string.Join(", ", person.Career ?? new())}");
}
```

### 6. 获取关联条目

```csharp
var related = await client.GetRelatedSubjectsBySubjectIdAsync(subjectId);

foreach (var rel in related)
{
    Console.WriteLine($"{rel.Relation}: {rel.Name} (ID: {rel.Id})");
}
```

## 角色 API

### 1. 搜索角色

```csharp
var request = new CharacterSearchRequest
{
    Keyword = "古河渚"
};
var result = await client.SearchCharactersAsync(request);

foreach (var character in result.Data)
{
    Console.WriteLine($"[{character.Id}] {character.Name}");
    Console.WriteLine($"性别: {character.Gender}");
    Console.WriteLine($"简介: {character.Summary}");
}
```

### 2. 获取角色详情

```csharp
int characterId = 1005;
var character = await client.GetCharacterByIdAsync(characterId);

if (character != null)
{
    Console.WriteLine($"名称: {character.Name}");
    Console.WriteLine($"性别: {character.Gender}");
    Console.WriteLine($"血型: {character.BloodType}");
    Console.WriteLine($"生日: {character.BirthYear}/{character.BirthMon}/{character.BirthDay}");
    Console.WriteLine($"简介: {character.Summary}");
    
    // Infobox 信息
    if (character.Infobox != null)
    {
        foreach (var info in character.Infobox)
        {
            Console.WriteLine($"{info.Key}: {info.Value}");
        }
    }
}
```

### 3. 获取角色相关条目

```csharp
var subjects = await client.GetRelatedSubjectsByCharacterIdAsync(characterId);

foreach (var subject in subjects)
{
    Console.WriteLine($"{subject.Name} ({subject.NameCn})");
    Console.WriteLine($"职位: {subject.Staff}");
}
```

### 4. 获取角色相关人物（声优等）

```csharp
var persons = await client.GetRelatedPersonsByCharacterIdAsync(characterId);

foreach (var person in persons)
{
    Console.WriteLine($"{person.Name} - {person.Staff}");
    Console.WriteLine($"在 {person.SubjectName} 中");
}
```

## 人物 API

### 1. 搜索人物

```csharp
var request = new PersonSearchRequest
{
    Keyword = "新海诚"
};
var result = await client.SearchPersonsAsync(request);

foreach (var person in result.Data)
{
    Console.WriteLine($"[{person.Id}] {person.Name}");
    Console.WriteLine($"职业: {string.Join(", ", person.Career ?? new())}");
}
```

### 2. 获取人物详情

```csharp
int personId = 2404;
var person = await client.GetPersonByIdAsync(personId);

if (person != null)
{
    Console.WriteLine($"名称: {person.Name}");
    Console.WriteLine($"类型: {(PersonType)person.Type}");
    Console.WriteLine($"职业: {string.Join(", ", person.Career ?? new())}");
    Console.WriteLine($"简介: {person.ShortSummary}");
}
```

### 3. 获取人物相关角色

```csharp
var characters = await client.GetRelatedCharactersByPersonIdAsync(personId);

foreach (var character in characters)
{
    Console.WriteLine($"角色: {character.Name}");
    Console.WriteLine($"在 {character.SubjectName} 中担任 {character.Staff}");
}
```

### 4. 获取人物相关条目

```csharp
var subjects = await client.GetRelatedSubjectsByPersonIdAsync(personId);

foreach (var subject in subjects)
{
    Console.WriteLine($"{subject.Name} ({subject.NameCn})");
    Console.WriteLine($"职位: {subject.Staff}");
}
```

## 剧集 API

### 1. 获取剧集列表

```csharp
int subjectId = 2097;

// 获取所有类型的剧集
var episodes = await client.GetEpisodesAsync(subjectId);

// 只获取本篇
var mainEpisodes = await client.GetEpisodesAsync(subjectId, episodeType: 0);

// 分页获取
var pagedEpisodes = await client.GetEpisodesAsync(subjectId, limit: 20, offset: 0);

foreach (var episode in episodes.Data)
{
    Console.WriteLine($"EP{episode.Ep}: {episode.Name}");
    Console.WriteLine($"  类型: {(EpType)episode.Type}");
    Console.WriteLine($"  放送日期: {episode.Airdate}");
    Console.WriteLine($"  时长: {episode.Duration}");
}
```

### 2. 获取剧集详情

```csharp
int episodeId = 8;
var episode = await client.GetEpisodeByIdAsync(episodeId);

if (episode != null)
{
    Console.WriteLine($"名称: {episode.Name}");
    Console.WriteLine($"中文名: {episode.NameCn}");
    Console.WriteLine($"所属条目 ID: {episode.SubjectId}");
    Console.WriteLine($"集数: {episode.Ep}");
    Console.WriteLine($"简介: {episode.Desc}");
}
```

## 用户 API

### 1. 获取当前用户信息

```csharp
var me = await client.GetMyselfAsync();

if (me != null)
{
    Console.WriteLine($"ID: {me.Id}");
    Console.WriteLine($"用户名: {me.Username}");
    Console.WriteLine($"昵称: {me.Nickname}");
    Console.WriteLine($"签名: {me.Sign}");
    Console.WriteLine($"用户组: {(UserGroup)me.UserGroup}");
    
    if (me.Avatar != null)
    {
        Console.WriteLine($"头像: {me.Avatar.Large}");
    }
}
```

### 2. 获取其他用户信息

```csharp
string username = "sai";
var user = await client.GetUserByNameAsync(username);

if (user != null)
{
    Console.WriteLine($"用户名: {user.Username}");
    Console.WriteLine($"昵称: {user.Nickname}");
}
```

## 收藏 API

### 1. 获取用户收藏列表

```csharp
string username = "your_username";

// 获取所有收藏
var collections = await client.GetUserCollectionsAsync(username);

// 只获取动画收藏
var animeCollections = await client.GetUserCollectionsAsync(
    username, 
    subjectType: 2 // 动画
);

// 只获取"在看"的收藏
var doingCollections = await client.GetUserCollectionsAsync(
    username,
    collectionType: 3 // 在看
);

// 分页获取
var pagedCollections = await client.GetUserCollectionsAsync(
    username,
    limit: 20,
    offset: 0
);

foreach (var collection in collections.Data)
{
    var subject = collection.Subject;
    var type = (SubjectCollectionType)collection.Type;
    
    Console.WriteLine($"[{type}] {subject?.Name}");
    Console.WriteLine($"  评分: {collection.Rate}/10");
    Console.WriteLine($"  进度: {collection.EpStatus}/{subject?.TotalEpisodes}");
    Console.WriteLine($"  标签: {string.Join(", ", collection.Tags ?? new())}");
    Console.WriteLine($"  评论: {collection.Comment}");
}
```

### 2. 获取指定条目的收藏信息

```csharp
var collection = await client.GetUserCollectionAsync(username, subjectId);

if (collection != null)
{
    Console.WriteLine($"收藏类型: {(SubjectCollectionType)collection.Type}");
    Console.WriteLine($"评分: {collection.Rate}");
    Console.WriteLine($"观看进度: {collection.EpStatus}");
}
```

### 3. 添加或更新收藏

```csharp
var request = new UserCollectionModifyRequest
{
    Type = (int)SubjectCollectionType.Doing, // 在看
    Rate = 9, // 评分 9 分
    Comment = "非常精彩的作品！",
    Tags = new List<string> { "治愈", "感动", "校园" },
    Private = false // 公开收藏
};

var result = await client.UpdateUserCollectionAsync(subjectId, request);
Console.WriteLine($"收藏成功: {result?.Subject?.Name}");
```

### 4. 部分更新收藏

```csharp
var request = new UserCollectionModifyRequest
{
    Rate = 10, // 只更新评分
};

await client.PatchUserCollectionAsync(subjectId, request);
```

### 5. 批量更新章节收藏

```csharp
// 标记多集为"看过"
var request = new EpisodeCollectionBatchRequest
{
    EpisodeId = new List<int> { 1, 2, 3, 4, 5 },
    Type = (int)EpisodeCollectionType.Done
};

await client.PatchUserSubjectEpisodeCollectionAsync(subjectId, request);
```

### 6. 更新单集收藏状态

```csharp
int episodeId = 100;
var request = new EpisodeCollectionRequest
{
    Type = (int)EpisodeCollectionType.Done // 标记为看过
};

await client.PutUserEpisodeCollectionAsync(episodeId, request);
```

### 7. 获取用户章节收藏状态

```csharp
var episodeCollection = await client.GetUserSubjectEpisodeCollectionAsync(
    username, 
    subjectId
);

if (episodeCollection != null)
{
    foreach (var ep in episodeCollection.Episodes ?? new())
    {
        var status = (EpisodeCollectionType)ep.Type;
        Console.WriteLine($"EP{ep.Episode?.Ep}: {status}");
    }
}
```

## 每日放送 API

### 获取每日放送

```csharp
var calendar = await client.GetCalendarAsync();

foreach (var day in calendar)
{
    if (day.Weekday != null)
    {
        Console.WriteLine($"\n=== {day.Weekday.Cn} ({day.Weekday.En}) ===");
        
        foreach (var item in day.Items ?? new())
        {
            Console.WriteLine($"{item.Name} ({item.NameCn})");
            Console.WriteLine($"  放送时间: {item.AirDate}");
            Console.WriteLine($"  评分: {item.Rating?.Score}");
            Console.WriteLine($"  排名: {item.Rank}");
        }
    }
}
```

## 高级用法

### 1. 使用 CancellationToken

```csharp
using var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(30)); // 30秒超时

try
{
    var subject = await client.GetSubjectByIdAsync(2097, cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("请求超时");
}
```

### 2. 错误处理

```csharp
try
{
    var subject = await client.GetSubjectByIdAsync(9999999);
}
catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    Console.WriteLine("条目不存在");
}
catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
{
    Console.WriteLine("API Key 无效");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP 错误: {ex.StatusCode} - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"未知错误: {ex.Message}");
}
```

### 3. 批量操作

```csharp
// 批量获取多个条目的详细信息
var subjectIds = new List<int> { 2097, 20, 328, 12 };
var tasks = subjectIds.Select(id => client.GetSubjectByIdAsync(id));
var subjects = await Task.WhenAll(tasks);

foreach (var subject in subjects.Where(s => s != null))
{
    Console.WriteLine($"{subject.Name}: {subject.Rating?.Score}");
}
```

### 4. 分页遍历

```csharp
async Task GetAllUserCollections(string username)
{
    var allCollections = new List<UserSubjectCollection>();
    int offset = 0;
    int limit = 50;
    
    while (true)
    {
        var result = await client.GetUserCollectionsAsync(
            username, 
            limit: limit, 
            offset: offset
        );
        
        allCollections.AddRange(result.Data);
        
        if (result.Data.Count < limit)
        {
            break; // 已经获取所有数据
        }
        
        offset += limit;
        await Task.Delay(1000); // 避免请求过快
    }
    
    return allCollections;
}
```

### 5. 使用依赖注入

```csharp
// Startup.cs 或 Program.cs
services.AddSingleton<BangumiClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new BangumiClient(
        config["Bangumi:ApiKey"],
        config["Bangumi:UserAgent"]
    );
});

// 在控制器或服务中使用
public class MyService
{
    private readonly BangumiClient _client;
    
    public MyService(BangumiClient client)
    {
        _client = client;
    }
    
    public async Task<Subject?> GetSubject(int id)
    {
        return await _client.GetSubjectByIdAsync(id);
    }
}
```

### 6. 缓存结果

```csharp
using Microsoft.Extensions.Caching.Memory;

public class CachedBangumiService
{
    private readonly BangumiClient _client;
    private readonly IMemoryCache _cache;
    
    public CachedBangumiService(BangumiClient client, IMemoryCache cache)
    {
        _client = client;
        _cache = cache;
    }
    
    public async Task<Subject?> GetSubjectAsync(int id)
    {
        var cacheKey = $"subject_{id}";
        
        if (_cache.TryGetValue(cacheKey, out Subject? cached))
        {
            return cached;
        }
        
        var subject = await _client.GetSubjectByIdAsync(id);
        
        if (subject != null)
        {
            _cache.Set(cacheKey, subject, TimeSpan.FromHours(1));
        }
        
        return subject;
    }
}
```

## 完整示例应用

查看 `Bangumi.NET.Example` 项目获取完整的工作示例。

## 更多资源

- [Bangumi API 官方文档](https://github.com/bangumi/api)
- [项目 README](README.md)
- [构建和测试指南](BUILD_AND_TEST.md)