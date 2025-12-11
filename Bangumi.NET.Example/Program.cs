using Bangumi.NET;
using Bangumi.NET.Models;

namespace Bangumi.NET.Example;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Bangumi.NET API 客户端示例 ===\n");

        // 从文件读取 API Key
        var apiKey = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "key.txt")).Trim();
        var userAgent = "SamHou.Bangumi.NET.Example 1.0";

        // 创建客户端
        using var client = new BangumiClient(apiKey, userAgent);

        try
        {
            // 1. 获取当前用户信息
            Console.WriteLine("1. 获取当前用户信息");
            var me = await client.GetMyselfAsync();
            if (me != null)
            {
                Console.WriteLine($"   用户名: {me.Username}");
                Console.WriteLine($"   昵称: {me.Nickname}");
                Console.WriteLine($"   签名: {me.Sign}");
            }
            Console.WriteLine();

            // 2. 获取每日放送
            Console.WriteLine("2. 获取每日放送（前3个条目）");
            var calendar = await client.GetCalendarAsync();
            if (calendar.Any())
            {
                var today = calendar.FirstOrDefault();
                if (today?.Weekday != null)
                {
                    Console.WriteLine($"   星期: {today.Weekday.Cn}");
                    var items = today.Items?.Take(3) ?? new List<LegacySubjectSmall>();
                    foreach (var item in items)
                    {
                        Console.WriteLine($"   - {item.Name} ({item.NameCn})");
                    }
                }
            }
            Console.WriteLine();

            // 3. 搜索条目
            Console.WriteLine("3. 搜索条目：'CLANNAD'");
            var searchRequest = new SubjectSearchRequest
            {
                Keyword = "CLANNAD",
                Sort = "rank"
            };
            var searchResult = await client.SearchSubjectsAsync(searchRequest, limit: 3);
            Console.WriteLine($"   找到 {searchResult.Total} 个结果，显示前 {searchResult.Data.Count} 个:");
            foreach (var subjectData in searchResult.Data)
            {
                Console.WriteLine($"   - [{subjectData.Id}] {subjectData.Name} ({subjectData.NameCn})");
                Console.WriteLine($"     评分: {subjectData.Rating?.Score}, 排名: {subjectData.Rating?.Rank}");
            }
            Console.WriteLine();

            // 4. 获取条目详情（CLANNAD）
            var subjectId = 2097; // CLANNAD
            Console.WriteLine($"4. 获取条目详情 (ID: {subjectId})");
            var subject = await client.GetSubjectByIdAsync(subjectId);
            if (subject != null)
            {
                Console.WriteLine($"   名称: {subject.Name}");
                Console.WriteLine($"   中文名: {subject.NameCn}");
                Console.WriteLine($"   类型: {(SubjectType)subject.Type}");
                Console.WriteLine($"   评分: {subject.Rating?.Score}, 排名: {subject.Rating?.Rank}");
                Console.WriteLine($"   集数: {subject.TotalEpisodes}");
                Console.WriteLine($"   放送日期: {subject.Date}");
                Console.WriteLine($"   简介: {(subject.Summary.Length > 100 ? subject.Summary.Substring(0, 100) + "..." : subject.Summary)}");
            }
            Console.WriteLine();

            // 5. 获取条目角色
            Console.WriteLine($"5. 获取条目角色（前3个）");
            var characters = await client.GetRelatedCharactersBySubjectIdAsync(subjectId);
            foreach (var character in characters.Take(3))
            {
                Console.WriteLine($"   - {character.Name} ({character.Relation})");
            }
            Console.WriteLine();

            // 6. 获取条目人物
            Console.WriteLine($"6. 获取条目人物（前3个）");
            var persons = await client.GetRelatedPersonsBySubjectIdAsync(subjectId);
            foreach (var person in persons.Take(3))
            {
                Console.WriteLine($"   - {person.Name} ({person.Relation})");
            }
            Console.WriteLine();

            // 7. 获取剧集列表
            Console.WriteLine($"7. 获取剧集列表（前5集）");
            var episodes = await client.GetEpisodesAsync(subjectId, limit: 5);
            Console.WriteLine($"   共 {episodes.Total} 集，显示前 {episodes.Data.Count} 集:");
            foreach (var episode in episodes.Data)
            {
                Console.WriteLine($"   - EP{episode.Ep}: {episode.Name} ({episode.NameCn})");
            }
            Console.WriteLine();

            // 8. 搜索角色
            Console.WriteLine("8. 搜索角色：'古河渚'");
            var characterSearch = new CharacterSearchRequest { Keyword = "古河渚" };
            var characterResult = await client.SearchCharactersAsync(characterSearch, limit: 2);
            Console.WriteLine($"   找到 {characterResult.Total} 个结果:");
            foreach (var chara in characterResult.Data)
            {
                Console.WriteLine($"   - [{chara.Id}] {chara.Name}");
            }
            Console.WriteLine();

            // 9. 获取角色详情
            if (characterResult.Data.Any())
            {
                var characterId = characterResult.Data.First().Id;
                Console.WriteLine($"9. 获取角色详情 (ID: {characterId})");
                var characterDetail = await client.GetCharacterByIdAsync(characterId);
                if (characterDetail != null)
                {
                    Console.WriteLine($"   名称: {characterDetail.Name}");
                    Console.WriteLine($"   性别: {characterDetail.Gender}");
                    Console.WriteLine($"   生日: {characterDetail.BirthMon}/{characterDetail.BirthDay}");
                    Console.WriteLine($"   简介: {(characterDetail.Summary.Length > 100 ? characterDetail.Summary.Substring(0, 100) + "..." : characterDetail.Summary)}");
                }
                Console.WriteLine();
            }

            // 10. 获取用户收藏（如果有的话）
            if (me != null)
            {
                Console.WriteLine($"10. 获取用户收藏（前3个）");
                try
                {
                    var collections = await client.GetUserCollectionsAsync(me.Username, limit: 3);
                    Console.WriteLine($"   共 {collections.Total} 个收藏，显示前 {collections.Data.Count} 个:");
                    foreach (var collection in collections.Data)
                    {
                        var collectionType = (SubjectCollectionType)collection.Type;
                        Console.WriteLine($"   - [{collection.Subject?.Name}] 状态: {collectionType}, 评分: {collection.Rate}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   获取收藏失败: {ex.Message}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("=== 所有测试完成 ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
            Console.WriteLine($"堆栈: {ex.StackTrace}");
        }
    }
}
