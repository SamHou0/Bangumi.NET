using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 人物信息
/// </summary>
public class Person
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

    [JsonPropertyName("short_summary")]
    public string ShortSummary { get; set; } = string.Empty;

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
/// 人物搜索请求
/// </summary>
public class PersonSearchRequest
{
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    [JsonPropertyName("filter")]
    public PersonSearchFilter? Filter { get; set; }
}

/// <summary>
/// 人物搜索过滤器
/// </summary>
public class PersonSearchFilter
{
    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; set; }
}

/// <summary>
/// 人物关联的角色
/// </summary>
public class PersonCharacter
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

/// <summary>
/// 人物关联的条目
/// </summary>
public class PersonSubject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public Images? Images { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("staff")]
    public string Staff { get; set; } = string.Empty;
}
