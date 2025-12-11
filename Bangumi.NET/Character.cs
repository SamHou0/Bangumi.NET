using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 角色信息
/// </summary>
public class Character
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("images")]
    public PersonImages? Images { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = string.Empty;

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("infobox")]
    public List<InfoboxItem>? Infobox { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("blood_type")]
    public int? BloodType { get; set; }

    [JsonPropertyName("birth_year")]
    public int? BirthYear { get; set; }

    [JsonPropertyName("birth_mon")]
    public int? BirthMon { get; set; }

    [JsonPropertyName("birth_day")]
    public int? BirthDay { get; set; }

    [JsonPropertyName("stat")]
    public Stat? Stat { get; set; }
}

/// <summary>
/// 角色搜索请求
/// </summary>
public class CharacterSearchRequest
{
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    [JsonPropertyName("filter")]
    public CharacterSearchFilter? Filter { get; set; }
}

/// <summary>
/// 角色搜索过滤器
/// </summary>
public class CharacterSearchFilter
{
    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; set; }
}

/// <summary>
/// 角色关联的条目
/// </summary>
public class CharacterSubject
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

    [JsonPropertyName("staff")]
    public string Staff { get; set; } = string.Empty;
}

/// <summary>
/// 角色关联的人物
/// </summary>
public class CharacterPerson
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

    [JsonPropertyName("subject_type")]
    public int SubjectType { get; set; }

    [JsonPropertyName("subject_name")]
    public string SubjectName { get; set; } = string.Empty;

    [JsonPropertyName("subject_name_cn")]
    public string SubjectNameCn { get; set; } = string.Empty;

    [JsonPropertyName("staff")]
    public string Staff { get; set; } = string.Empty;
}
