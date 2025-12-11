# Bangumi.NET 构建和测试指南

## 环境要求

- .NET 8.0 SDK
- Windows / Linux / macOS
- API Key (从 https://next.bgm.tv/demo/access-token 获取)

## 构建步骤

### 1. 克隆或进入项目目录

```bash
cd D:\code\Csharp\Bangumi.NET
```

### 2. 确保 API Key 已配置

确保 `key.txt` 文件存在于项目根目录，并包含有效的 API Key。

### 3. 恢复依赖项

```bash
dotnet restore
```

### 4. 构建项目

```bash
dotnet build
```

如果构建成功，你将看到类似以下输出：

```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### 5. 运行示例项目

```bash
cd Bangumi.NET.Example
dotnet run
```

## 测试指南

示例项目 (`Bangumi.NET.Example`) 包含完整的功能测试，将依次测试以下功能：

### 测试清单

- [x] 获取当前用户信息
- [x] 获取每日放送
- [x] 搜索条目
- [x] 获取条目详情
- [x] 获取条目角色
- [x] 获取条目人物
- [x] 获取剧集列表
- [x] 搜索角色
- [x] 获取角色详情
- [x] 获取用户收藏

### 预期输出

运行示例程序后，你应该看到类似以下的输出：

```
=== Bangumi.NET API 客户端示例 ===

1. 获取当前用户信息
   用户名: your_username
   昵称: Your Nickname
   签名: Your signature

2. 获取每日放送（前3个条目）
   星期: 星期一
   - Title 1 (中文名1)
   - Title 2 (中文名2)
   - Title 3 (中文名3)

3. 搜索条目：'CLANNAD'
   找到 XX 个结果，显示前 3 个:
   - [2097] CLANNAD (CLANNAD)
     评分: 8.9, 排名: 21
   ...

=== 所有测试完成 ===
```

## 故障排除

### 问题：构建失败 - 找不到 .NET SDK

**解决方案**：
```bash
# 检查 .NET SDK 版本
dotnet --version

# 如果未安装，下载并安装 .NET 8.0 SDK
# https://dotnet.microsoft.com/download/dotnet/8.0
```

### 问题：运行时出现 HTTP 401 错误

**原因**：API Key 无效或过期

**解决方案**：
1. 检查 `key.txt` 文件内容是否正确
2. 访问 https://next.bgm.tv/demo/access-token 重新生成 API Key
3. 确保文件中没有多余的空格或换行符

### 问题：运行时出现 HTTP 403 错误

**原因**：User-Agent 不符合要求

**解决方案**：
修改 `Program.cs` 中的 `userAgent` 变量，确保包含应用名称和联系方式：
```csharp
var userAgent = "YourAppName/1.0 (https://your-contact-url.com)";
```

### 问题：运行时出现 JsonException

**原因**：API 返回的数据结构与模型不匹配

**解决方案**：
1. 检查 API 版本是否更新
2. 查看完整错误消息
3. 提交 Issue 报告问题

### 问题：某些 API 调用返回 null

**原因**：可能的原因包括：
- 资源不存在（如无效的 ID）
- 权限不足
- API 限流

**解决方案**：
1. 验证请求的资源 ID 是否正确
2. 检查 API Key 是否有足够权限
3. 添加重试逻辑和延迟

## 单元测试

目前项目使用示例程序进行集成测试。如需添加单元测试：

### 1. 创建测试项目

```bash
cd D:\code\Csharp\Bangumi.NET
dotnet new xunit -n Bangumi.NET.Tests
dotnet sln add Bangumi.NET.Tests/Bangumi.NET.Tests.csproj
```

### 2. 添加项目引用

```bash
cd Bangumi.NET.Tests
dotnet add reference ../Bangumi.NET/Bangumi.NET.csproj
```

### 3. 运行测试

```bash
dotnet test
```

## 性能测试

### 基准测试建议

- 测试单个 API 调用的响应时间
- 测试并发请求的处理能力
- 测试大数据量查询的性能

### 示例基准测试代码

```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();
var subject = await client.GetSubjectByIdAsync(2097);
sw.Stop();
Console.WriteLine($"请求耗时: {sw.ElapsedMilliseconds}ms");
```

## 调试技巧

### 启用详细日志

```csharp
// 在 BangumiClient 中添加日志记录
// 可以使用 ILogger 或其他日志框架
```

### 查看 HTTP 请求详情

```csharp
// 使用 Fiddler 或 Wireshark 捕获 HTTP 流量
// 检查请求头、请求体和响应内容
```

### 断点调试

在 Visual Studio 或 VS Code 中：
1. 在关键代码行设置断点
2. 按 F5 启动调试
3. 逐步执行代码，查看变量值

## 持续集成

### GitHub Actions 示例配置

```yaml
name: .NET

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 8.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

## 发布

### 发布为 NuGet 包

```bash
# 打包
dotnet pack Bangumi.NET/Bangumi.NET.csproj -c Release

# 发布到 NuGet.org
dotnet nuget push Bangumi.NET/bin/Release/Bangumi.NET.*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

## 贡献代码

如果你想为项目贡献代码：

1. Fork 项目
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 创建 Pull Request

## 联系方式

如果遇到问题或有建议，请：
- 提交 GitHub Issue
- 发送邮件到维护者邮箱
- 在 Bangumi 小组讨论

## 更新日志

### v1.0.0 (2024)
- 初始版本
- 完整支持 Bangumi API v0
- 包含所有主要功能模块
- 提供完整示例程序

## 致谢

感谢 Bangumi 团队提供优秀的 API 服务。