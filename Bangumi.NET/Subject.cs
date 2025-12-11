using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 条目信息
/// </summary>
public class Subject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = string.Empty;

    [JsonPropertyName("nsfw")]
    public bool Nsfw { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("platform")]
    public string Platform { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public Images? Images { get; set; }

    [JsonPropertyName("infobox")]
    public List<InfoboxItem>? Infobox { get; set; }

    [JsonPropertyName("volumes")]
    public int Volumes { get; set; }

    [JsonPropertyName("eps")]
    public int Eps { get; set; }

    [JsonPropertyName("total_episodes")]
    public int TotalEpisodes { get; set; }

    [JsonPropertyName("rating")]
    public Rating? Rating { get; set; }

    [JsonPropertyName("collection")]
    public CollectionStats? Collection { get; set; }

    [JsonPropertyName("tags")]
    public List<Tag>? Tags { get; set; }

    [JsonPropertyName("series")]
    public bool Series { get; set; }

    [JsonPropertyName("meta_tags")]
    public List<string>? MetaTags { get; set; }
}

/// <summary>
/// 评分信息
/// </summary>
public class Rating
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("count")]
    public Dictionary<string, int>? Count { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }
}

/// <summary>
/// 收藏统计
/// </summary>
public class CollectionStats
{
    [JsonPropertyName("wish")]
    public int Wish { get; set; }

    [JsonPropertyName("collect")]
    public int Collect { get; set; }

    [JsonPropertyName("doing")]
    public int Doing { get; set; }

    [JsonPropertyName("on_hold")]
    public int OnHold { get; set; }

    [JsonPropertyName("dropped")]
    public int Dropped { get; set; }
}

/// <summary>
/// 标签
/// </summary>
public class Tag
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}

/// <summary>
/// Infobox 项
/// </summary>
public class InfoboxItem
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public object? Value { get; set; }
}

/// <summary>
/// 条目搜索请求
/// </summary>
public class SubjectSearchRequest
{
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    [JsonPropertyName("sort")]
    public string? Sort { get; set; }

    [JsonPropertyName("filter")]
    public SubjectSearchFilter? Filter { get; set; }
}

/// <summary>
/// 条目搜索过滤器
/// </summary>
public class SubjectSearchFilter
{
    [JsonPropertyName("type")]
    public List<int>? Type { get; set; }

    [JsonPropertyName("tag")]
    public List<string>? Tag { get; set; }

    [JsonPropertyName("meta_tags")]
    public List<string>? MetaTags { get; set; }

    [JsonPropertyName("air_date")]
    public List<string>? AirDate { get; set; }

    [JsonPropertyName("rating")]
    public List<string>? Rating { get; set; }

    [JsonPropertyName("rating_count")]
    public List<string>? RatingCount { get; set; }

    [JsonPropertyName("rank")]
    public List<string>? Rank { get; set; }

    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; set; }
}

/// <summary>
/// 条目关联关系
/// </summary>
public class SubjectRelation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public Images? Images { get; set; }

    [JsonPropertyName("relation")]
    public string Relation { get; set; } = string.Empty;
}

/// <summary>
/// 相关角色（来自条目）
/// </summary>
public class RelatedCharacter
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("images")]
    public PersonImages? Images { get; set; }

    [JsonPropertyName("relation")]
    public string Relation { get; set; } = string.Empty;

    [JsonPropertyName("actors")]
    public List<Actor>? Actors { get; set; }
}

/// <summary>
/// 声优
/// </summary>
public class Actor
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("images")]
    public PersonImages? Images { get; set; }

    [JsonPropertyName("subject_id")]
    public int SubjectId { get; set; }

    [JsonPropertyName("subject_name")]
    public string SubjectName { get; set; } = string.Empty;

    [JsonPropertyName("subject_name_cn")]
    public string SubjectNameCn { get; set; } = string.Empty;
}

/// <summary>
/// 相关人物（来自条目）
/// </summary>
public class RelatedPerson
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("career")]
    public List<string>? Career { get; set; }

    [JsonPropertyName("images")]
    public PersonImages? Images { get; set; }

    [JsonPropertyName("relation")]
    public string Relation { get; set; } = string.Empty;
}
