using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 用户条目收藏
/// </summary>
public class UserSubjectCollection
{
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; set; }

    [JsonPropertyName("subject_type")]
    public int SubjectType { get; set; }

    [JsonPropertyName("rate")]
    public int Rate { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("ep_status")]
    public int EpStatus { get; set; }

    [JsonPropertyName("vol_status")]
    public int VolStatus { get; set; }

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; } = string.Empty;

    [JsonPropertyName("private")]
    public bool Private { get; set; }

    [JsonPropertyName("subject")]
    public Subject? Subject { get; set; }
}

/// <summary>
/// 修改收藏请求
/// </summary>
public class UserCollectionModifyRequest
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("rate")]
    public int? Rate { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}

/// <summary>
/// 用户章节收藏
/// </summary>
public class UserEpisodeCollection
{
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; set; }

    [JsonPropertyName("episodes")]
    public List<EpisodeCollectionInfo>? Episodes { get; set; }
}

/// <summary>
/// 章节收藏信息
/// </summary>
public class EpisodeCollectionInfo
{
    [JsonPropertyName("episode")]
    public Episode? Episode { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }
}

/// <summary>
/// 批量更新章节收藏请求
/// </summary>
public class EpisodeCollectionBatchRequest
{
    [JsonPropertyName("episode_id")]
    public List<int>? EpisodeId { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }
}

/// <summary>
/// 更新单集收藏请求
/// </summary>
public class EpisodeCollectionRequest
{
    [JsonPropertyName("type")]
    public int Type { get; set; }
}

/// <summary>
/// 用户角色收藏
/// </summary>
public class UserCharacterCollection
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("images")]
    public PersonImages? Images { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;
}

/// <summary>
/// 用户人物收藏
/// </summary>
public class UserPersonCollection
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

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;
}
