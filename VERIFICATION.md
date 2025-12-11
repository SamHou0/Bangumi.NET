# Bangumi.NET 验证清单

使用此清单验证项目是否正确构建和运行。

## 前置检查

### ✅ 环境检查

```bash
# 检查 .NET SDK 版本
dotnet --version
# 应显示: 8.0.x 或更高版本
```

### ✅ 文件检查

确认以下文件存在：

**核心库文件 (10个)**
- [ ] `Bangumi.NET\BangumiClient.cs`
- [ ] `Bangumi.NET\Common.cs`
- [ ] `Bangumi.NET\Subject.cs`
- [ ] `Bangumi.NET\Episode.cs`
- [ ] `Bangumi.NET\Character.cs`
- [ ] `Bangumi.NET\Person.cs`
- [ ] `Bangumi.NET\User.cs`
- [ ] `Bangumi.NET\Collection.cs`
- [ ] `Bangumi.NET\Calendar.cs`
- [ ] `Bangumi.NET\Bangumi.NET.csproj`

**示例项目 (2个)**
- [ ] `Bangumi.NET.Example\Program.cs`
- [ ] `Bangumi.NET.Example\Bangumi.NET.Example.csproj`

**文档文件 (6个)**
- [ ] `README.md`
- [ ] `QUICKSTART.md`
- [ ] `EXAMPLES.md`
- [ ] `BUILD_AND_TEST.md`
- [ ] `PROJECT_SUMMARY.md`
- [ ] `VERIFICATION.md` (本文件)

**配置文件 (2个)**
- [ ] `key.txt` (包含有效的 API Key)
- [ ] `Bangumi.NET.sln`

### ✅ API Key 检查

```bash
# Windows PowerShell
type key.txt

# 或手动打开文件检查
# 应该是一行字符串，类似: DmsGwsZGgJ1zgPjRpNtTQr4lNTPFRtV4IzkUyAqq
```

确认：
- [ ] key.txt 文件存在
- [ ] 文件包含有效的 API Key
- [ ] 没有多余的空格或换行
- [ ] API Key 未过期

## 构建验证

### 步骤 1: 清理项目

```bash
cd D:\code\Csharp\Bangumi.NET
dotnet clean
```

预期输出：
```
Build succeeded.
```

### 步骤 2: 恢复依赖

```bash
dotnet restore
```

预期输出：
```
Restored D:\code\Csharp\Bangumi.NET\Bangumi.NET\Bangumi.NET.csproj
Restored D:\code\Csharp\Bangumi.NET\Bangumi.NET.Example\Bangumi.NET.Example.csproj
```

验证点：
- [ ] 两个项目都成功恢复
- [ ] 没有错误消息
- [ ] 没有警告消息（或只有可忽略的警告）

### 步骤 3: 构建项目

```bash
dotnet build
```

预期输出：
```
Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:XX.XX
```

验证点：
- [ ] 构建成功
- [ ] 0 个错误
- [ ] 生成了 dll 文件

检查生成的文件：
```bash
# 应该存在以下文件
ls Bangumi.NET\bin\Debug\net8.0\Bangumi.NET.dll
ls Bangumi.NET.Example\bin\Debug\net8.0\Bangumi.NET.Example.dll
```

## 运行验证

### 步骤 4: 运行示例程序

```bash
cd Bangumi.NET.Example
dotnet run
```

### 预期输出结构

程序应该输出类似以下内容：

```
=== Bangumi.NET API 客户端示例 ===

1. 获取当前用户信息
   用户名: [你的用户名]
   昵称: [你的昵称]
   签名: [你的签名]

2. 获取每日放送（前3个条目）
   星期: [星期X]
   - [条目1]
   - [条目2]
   - [条目3]

3. 搜索条目：'CLANNAD'
   找到 [数字] 个结果，显示前 3 个:
   - [2097] CLANNAD (CLANNAD)
     评分: 8.9, 排名: 21
   [更多结果...]

4. 获取条目详情 (ID: 2097)
   名称: CLANNAD
   中文名: CLANNAD
   类型: Anime
   评分: [评分], 排名: [排名]
   集数: 24
   放送日期: 2007-10-04
   简介: [简介内容...]

5. 获取条目角色（前3个）
   - [角色名1] (主角)
   - [角色名2] (主角)
   - [角色名3] (配角)

6. 获取条目人物（前3个）
   - [人物名1] (原作)
   - [人物名2] (导演)
   - [人物名3] (音乐)

7. 获取剧集列表（前5集）
   共 24 集，显示前 5 集:
   - EP1: [集名] ([中文名])
   [更多集数...]

8. 搜索角色：'古河渚'
   找到 [数字] 个结果:
   - [ID] 古河渚

9. 获取角色详情 (ID: [ID])
   名称: 古河渚
   性别: 女
   生日: 12/24
   简介: [简介...]

10. 获取用户收藏（前3个）
   共 [数字] 个收藏，显示前 3 个:
   - [条目名] 状态: [状态], 评分: [评分]
   [更多收藏...]

=== 所有测试完成 ===
```

### 验证检查表

- [ ] 程序成功启动，没有异常
- [ ] 测试 1: 成功获取用户信息
- [ ] 测试 2: 成功获取每日放送
- [ ] 测试 3: 成功搜索条目
- [ ] 测试 4: 成功获取条目详情
- [ ] 测试 5: 成功获取条目角色
- [ ] 测试 6: 成功获取条目人物
- [ ] 测试 7: 成功获取剧集列表
- [ ] 测试 8: 成功搜索角色
- [ ] 测试 9: 成功获取角色详情
- [ ] 测试 10: 尝试获取用户收藏（可能需要权限）
- [ ] 程序正常结束，显示"所有测试完成"

## 错误排查

### 常见错误 1: 找不到 key.txt

**错误消息**:
```
System.IO.FileNotFoundException: Could not find file 'D:\...\key.txt'
```

**解决方案**:
1. 确认 key.txt 在项目根目录
2. 检查文件路径
3. 确认当前工作目录正确

**验证命令**:
```bash
# 从项目根目录运行
cd D:\code\Csharp\Bangumi.NET
dir key.txt
```

### 常见错误 2: HTTP 401 Unauthorized

**错误消息**:
```
HttpRequestException: Response status code does not indicate success: 401 (Unauthorized)
```

**解决方案**:
1. 检查 key.txt 内容是否正确
2. 重新生成 API Key: https://next.bgm.tv/demo/access-token
3. 确保 API Key 没有过期

**验证命令**:
```bash
type key.txt
# 应该是一行完整的字符串，没有空格或换行
```

### 常见错误 3: HTTP 403 Forbidden

**错误消息**:
```
HttpRequestException: Response status code does not indicate success: 403 (Forbidden)
```

**原因**: User-Agent 不符合要求

**解决方案**:
修改 `Bangumi.NET.Example\Program.cs` 第14行：
```csharp
var userAgent = "YourAppName/1.0 (https://your-contact-url.com)";
```

格式要求：`应用名/版本 (联系方式)`

### 常见错误 4: JSON 反序列化失败

**错误消息**:
```
System.Text.Json.JsonException: ...
```

**可能原因**:
1. API 返回格式变化
2. 模型定义不匹配
3. 网络返回了错误页面

**排查步骤**:
1. 查看完整错误消息
2. 检查网络连接
3. 使用浏览器直接访问 API 确认格式
4. 查看 Bangumi API 文档是否更新

### 常见错误 5: 超时

**错误消息**:
```
System.Threading.Tasks.TaskCanceledException: ...
```

**解决方案**:
1. 检查网络连接
2. 重试请求
3. 增加超时时间

## 功能测试

### 测试 1: 基础搜索

```csharp
var request = new SubjectSearchRequest { Keyword = "test" };
var result = await client.SearchSubjectsAsync(request);
Console.WriteLine($"找到 {result.Total} 个结果");
```

验证：
- [ ] 能够返回结果
- [ ] Total 字段有值
- [ ] Data 列表不为 null

### 测试 2: 高级搜索

```csharp
var request = new SubjectSearchRequest
{
    Keyword = "fate",
    Sort = "rank",
    Filter = new SubjectSearchFilter
    {
        Type = new List<int> { 2 },
        Rating = new List<string> { ">=8" }
    }
};
var result = await client.SearchSubjectsAsync(request);
```

验证：
- [ ] 过滤器生效
- [ ] 排序正确
- [ ] 结果符合条件

### 测试 3: 分页

```csharp
var page1 = await client.SearchSubjectsAsync(request, limit: 10, offset: 0);
var page2 = await client.SearchSubjectsAsync(request, limit: 10, offset: 10);
```

验证：
- [ ] 两页数据不重复
- [ ] 分页参数生效

### 测试 4: 并发请求

```csharp
var tasks = new[]
{
    client.GetSubjectByIdAsync(2097),
    client.GetSubjectByIdAsync(20),
    client.GetSubjectByIdAsync(328)
};
var results = await Task.WhenAll(tasks);
```

验证：
- [ ] 所有请求都成功
- [ ] 没有冲突或错误

### 测试 5: 错误处理

```csharp
try
{
    await client.GetSubjectByIdAsync(9999999);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"预期的错误: {ex.StatusCode}");
}
```

验证：
- [ ] 能够捕获异常
- [ ] StatusCode 正确（404）

## 性能测试

### 响应时间测试

```csharp
var sw = Stopwatch.StartNew();
var subject = await client.GetSubjectByIdAsync(2097);
sw.Stop();
Console.WriteLine($"请求耗时: {sw.ElapsedMilliseconds}ms");
```

预期：
- [ ] < 1000ms: 优秀
- [ ] < 3000ms: 良好
- [ ] > 3000ms: 需要优化或检查网络

### 内存测试

```csharp
var before = GC.GetTotalMemory(true);
for (int i = 0; i < 100; i++)
{
    await client.GetSubjectByIdAsync(2097);
}
var after = GC.GetTotalMemory(true);
Console.WriteLine($"内存增长: {(after - before) / 1024}KB");
```

验证：
- [ ] 没有明显的内存泄漏
- [ ] 内存使用合理

## 最终验证

所有测试通过后，确认：

- [ ] ✅ 项目成功构建，无错误
- [ ] ✅ 示例程序成功运行
- [ ] ✅ 所有 10 个测试场景都通过
- [ ] ✅ API 调用返回正确数据
- [ ] ✅ 错误处理正常工作
- [ ] ✅ 文档齐全且准确
- [ ] ✅ 代码风格一致

## 下一步

验证完成后，你可以：

1. **开始使用**
   - 在你的项目中引用 Bangumi.NET
   - 参考 EXAMPLES.md 学习更多用法

2. **贡献代码**
   - Fork 项目
   - 添加新功能或修复bug
   - 提交 Pull Request

3. **报告问题**
   - 如果发现 bug，请提交 Issue
   - 包含详细的错误信息和复现步骤

4. **分享反馈**
   - 分享使用体验
   - 提出改进建议

## 支持

如果验证过程中遇到问题：

1. 查看 README.md 了解详细文档
2. 查看 BUILD_AND_TEST.md 了解构建指南
3. 查看 EXAMPLES.md 了解使用示例
4. 提交 GitHub Issue 获取帮助

## 验证通过！

如果所有检查都通过了，恭喜你！Bangumi.NET 已经准备就绪，可以开始使用了！

✨ Happy Coding! ✨