using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 分页结果
/// </summary>
public class PagedResult<T>
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new();
}

/// <summary>
/// 图片信息
/// </summary>
public class Images
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
/// 头像
/// </summary>
public class Avatar
{
    [JsonPropertyName("large")]
    public string? Large { get; set; }

    [JsonPropertyName("medium")]
    public string? Medium { get; set; }

    [JsonPropertyName("small")]
    public string? Small { get; set; }
}

/// <summary>
/// 人物图片
/// </summary>
public class PersonImages
{
    [JsonPropertyName("large")]
    public string? Large { get; set; }

    [JsonPropertyName("medium")]
    public string? Medium { get; set; }

    [JsonPropertyName("small")]
    public string? Small { get; set; }

    [JsonPropertyName("grid")]
    public string? Grid { get; set; }
}

/// <summary>
/// 统计信息
/// </summary>
public class Stat
{
    [JsonPropertyName("comments")]
    public int Comments { get; set; }

    [JsonPropertyName("collects")]
    public int Collects { get; set; }
}

/// <summary>
/// 条目类型
/// </summary>
public enum SubjectType
{
    Book = 1,
    Anime = 2,
    Music = 3,
    Game = 4,
    Real = 6
}

/// <summary>
/// 收藏类型
/// </summary>
public enum SubjectCollectionType
{
    Wish = 1,
    Done = 2,
    Doing = 3,
    OnHold = 4,
    Dropped = 5
}

/// <summary>
/// 章节收藏类型
/// </summary>
public enum EpisodeCollectionType
{
    NotCollected = 0,
    Wish = 1,
    Done = 2,
    Dropped = 3
}

/// <summary>
/// 剧集类型
/// </summary>
public enum EpType
{
    MainStory = 0,
    SP = 1,
    OP = 2,
    ED = 3,
    PV = 4,
    MAD = 5,
    Other = 6
}

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType
{
    Character = 1,
    Mechanic = 2,
    Ship = 3,
    Organization = 4
}

/// <summary>
/// 人物类型
/// </summary>
public enum PersonType
{
    Individual = 1,
    Company = 2,
    Association = 3
}

/// <summary>
/// 人物职业
/// </summary>
public enum PersonCareer
{
    Producer,
    Mangaka,
    Artist,
    Seiyu,
    Writer,
    Illustrator,
    Actor
}

/// <summary>
/// 用户组
/// </summary>
public enum UserGroup
{
    Admin = 1,
    BangumiAdmin = 2,
    DoujinAdmin = 3,
    MutedUser = 4,
    BlockedUser = 5,
    PersonAdmin = 8,
    WikiAdmin = 9,
    User = 10,
    WikiUser = 11
}

/// <summary>
/// 血型
/// </summary>
public enum BloodType
{
    A = 1,
    B = 2,
    AB = 3,
    O = 4
}
