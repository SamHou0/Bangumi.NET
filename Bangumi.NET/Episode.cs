using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 剧集信息
/// </summary>
public class Episode
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("sort")]
    public double Sort { get; set; }

    [JsonPropertyName("ep")]
    public double? Ep { get; set; }

    [JsonPropertyName("airdate")]
    public string Airdate { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public int Comment { get; set; }

    [JsonPropertyName("duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("desc")]
    public string Desc { get; set; } = string.Empty;

    [JsonPropertyName("disc")]
    public int Disc { get; set; }

    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }
}

/// <summary>
/// 剧集详情
/// </summary>
public class EpisodeDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("sort")]
    public double Sort { get; set; }

    [JsonPropertyName("ep")]
    public double? Ep { get; set; }

    [JsonPropertyName("airdate")]
    public string Airdate { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public int Comment { get; set; }

    [JsonPropertyName("duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("desc")]
    public string Desc { get; set; } = string.Empty;

    [JsonPropertyName("disc")]
    public int Disc { get; set; }

    [JsonPropertyName("subject_id")]
    public int SubjectId { get; set; }

    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }
}
