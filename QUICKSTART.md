# Bangumi.NET 快速入门

5分钟快速上手 Bangumi.NET API 客户端。

## 1. 准备工作

### 获取 API Key

1. 访问 https://next.bgm.tv/demo/access-token
2. 登录你的 Bangumi 账号
3. 生成并复制 Access Token
4. 将 Token 保存到 `key.txt` 文件

### 安装要求

- .NET 8.0 SDK

## 2. 创建项目

```bash
# 克隆或下载项目
cd D:\code\Csharp\Bangumi.NET

# 确保 key.txt 存在并包含你的 API Key
echo "your-api-key-here" > key.txt

# 构建项目
dotnet build
```

## 3. 运行示例

```bash
cd Bangumi.NET.Example
dotnet run
```

你将看到示例程序运行各种 API 调用的结果。

## 4. 在你的项目中使用

### 步骤 1: 添加项目引用

```xml
<ItemGroup>
  <ProjectReference Include="..\Bangumi.NET\Bangumi.NET.csproj" />
</ItemGroup>
```

或者直接复制 Bangumi.NET 项目到你的解决方案。

### 步骤 2: 初始化客户端

```csharp
using Bangumi.NET;
using Bangumi.NET.Models;

var apiKey = "your-api-key";
var userAgent = "MyApp/1.0 (https://mywebsite.com)";

using var client = new BangumiClient(apiKey, userAgent);
```

### 步骤 3: 调用 API

```csharp
// 搜索条目
var searchRequest = new SubjectSearchRequest { Keyword = "CLANNAD" };
var result = await client.SearchSubjectsAsync(searchRequest);

foreach (var subject in result.Data)
{
    Console.WriteLine($"{subject.Name} - 评分: {subject.Rating?.Score}");
}

// 获取条目详情
var subject = await client.GetSubjectByIdAsync(2097);
Console.WriteLine($"名称: {subject?.Name}");
Console.WriteLine($"评分: {subject?.Rating?.Score}");

// 获取每日放送
var calendar = await client.GetCalendarAsync();
foreach (var day in calendar)
{
    Console.WriteLine($"{day.Weekday?.Cn}:");
    foreach (var item in day.Items?.Take(3) ?? new())
    {
        Console.WriteLine($"  - {item.Name}");
    }
}
```

## 5. 常用 API 速查

### 条目相关

```csharp
// 搜索
await client.SearchSubjectsAsync(request, limit: 10);

// 获取详情
await client.GetSubjectByIdAsync(subjectId);

// 获取角色
await client.GetRelatedCharactersBySubjectIdAsync(subjectId);

// 获取人物
await client.GetRelatedPersonsBySubjectIdAsync(subjectId);
```

### 角色相关

```csharp
// 搜索
await client.SearchCharactersAsync(request);

// 获取详情
await client.GetCharacterByIdAsync(characterId);
```

### 人物相关

```csharp
// 搜索
await client.SearchPersonsAsync(request);

// 获取详情
await client.GetPersonByIdAsync(personId);
```

### 用户相关

```csharp
// 获取当前用户
await client.GetMyselfAsync();

// 获取用户收藏
await client.GetUserCollectionsAsync(username);
```

### 收藏相关

```csharp
// 更新收藏
var request = new UserCollectionModifyRequest
{
    Type = 3, // 在看
    Rate = 9  // 评分
};
await client.UpdateUserCollectionAsync(subjectId, request);
```

## 6. 完整示例

```csharp
using Bangumi.NET;
using Bangumi.NET.Models;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = File.ReadAllText("key.txt").Trim();
        var userAgent = "MyApp/1.0";
        
        using var client = new BangumiClient(apiKey, userAgent);
        
        // 1. 获取用户信息
        var me = await client.GetMyselfAsync();
        Console.WriteLine($"你好, {me?.Nickname}!");
        
        // 2. 搜索条目
        var search = new SubjectSearchRequest 
        { 
            Keyword = "进击的巨人",
            Sort = "rank"
        };
        var result = await client.SearchSubjectsAsync(search, limit: 5);
        
        Console.WriteLine($"\n找到 {result.Total} 个结果:");
        foreach (var s in result.Data)
        {
            Console.WriteLine($"  [{s.Id}] {s.Name} - 评分: {s.Rating?.Score}");
        }
        
        // 3. 获取详情
        if (result.Data.Any())
        {
            var first = result.Data.First();
            var detail = await client.GetSubjectByIdAsync(first.Id);
            
            Console.WriteLine($"\n《{detail?.Name}》详情:");
            Console.WriteLine($"  中文名: {detail?.NameCn}");
            Console.WriteLine($"  类型: {(SubjectType)(detail?.Type ?? 0)}");
            Console.WriteLine($"  集数: {detail?.TotalEpisodes}");
            Console.WriteLine($"  评分: {detail?.Rating?.Score} (排名: {detail?.Rating?.Rank})");
        }
        
        // 4. 获取每日放送
        var calendar = await client.GetCalendarAsync();
        Console.WriteLine("\n本周放送:");
        foreach (var day in calendar.Take(3))
        {
            Console.WriteLine($"\n  {day.Weekday?.Cn}:");
            foreach (var item in day.Items?.Take(2) ?? new())
            {
                Console.WriteLine($"    - {item.Name}");
            }
        }
    }
}
```

## 7. 下一步

- 查看 [完整文档](README.md) 了解所有 API
- 查看 [使用示例](EXAMPLES.md) 学习高级用法
- 查看 [构建指南](BUILD_AND_TEST.md) 了解测试方法

## 常见问题

### Q: API Key 从哪里获取？
A: 访问 https://next.bgm.tv/demo/access-token 生成

### Q: User-Agent 应该填什么？
A: 格式为 `应用名称/版本 (联系方式)`，例如：`MyApp/1.0 (https://mywebsite.com)`

### Q: 遇到 401 错误怎么办？
A: 检查 API Key 是否正确，是否过期

### Q: 如何限制请求频率？
A: 在循环中添加 `await Task.Delay(1000);`

### Q: 支持哪些 .NET 版本？
A: 目前支持 .NET 8.0，可以修改 csproj 支持其他版本

## 获取帮助

- GitHub Issues: 提交bug或功能请求
- API 文档: https://github.com/bangumi/api
- 示例代码: `Bangumi.NET.Example` 项目

## 许可证

MIT License - 查看 LICENSE.txt