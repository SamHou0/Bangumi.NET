using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 每日放送项
/// </summary>
public class CalendarItem
{
    [JsonPropertyName("weekday")]
    public Weekday? Weekday { get; set; }

    [JsonPropertyName("items")]
    public List<LegacySubjectSmall>? Items { get; set; }
}

/// <summary>
/// 星期信息
/// </summary>
public class Weekday
{
    [JsonPropertyName("en")]
    public string En { get; set; } = string.Empty;

    [JsonPropertyName("cn")]
    public string Cn { get; set; } = string.Empty;

    [JsonPropertyName("ja")]
    public string Ja { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }
}

/// <summary>
/// 旧版小条目信息（用于兼容calendar接口）
/// </summary>
public class LegacySubjectSmall
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = string.Empty;

    [JsonPropertyName("air_date")]
    public string AirDate { get; set; } = string.Empty;

    [JsonPropertyName("air_weekday")]
    public int AirWeekday { get; set; }

    [JsonPropertyName("images")]
    public LegacyImages? Images { get; set; }

    [JsonPropertyName("eps")]
    public int? Eps { get; set; }

    [JsonPropertyName("eps_count")]
    public int? EpsCount { get; set; }

    [JsonPropertyName("rating")]
    public LegacyRating? Rating { get; set; }

    [JsonPropertyName("rank")]
    public int? Rank { get; set; }

    [JsonPropertyName("collection")]
    public LegacyCollection? Collection { get; set; }
}

/// <summary>
/// 旧版图片信息
/// </summary>
public class LegacyImages
{
    [JsonPropertyName("large")]
    public string? Large { get; set; }

    [JsonPropertyName("common")]
    public string? Common { get; set; }

    [JsonPropertyName("medium")]
    public string? Medium { get; set; }

    [JsonPropertyName("small")]
    public string? Small { get; set; }

    [JsonPropertyName("grid")]
    public string? Grid { get; set; }
}

/// <summary>
/// 旧版评分信息
/// </summary>
public class LegacyRating
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("count")]
    public Dictionary<string, int>? Count { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }
}

/// <summary>
/// 旧版收藏信息
/// </summary>
public class LegacyCollection
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
