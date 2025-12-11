using System.Text.Json.Serialization;

namespace Bangumi.NET.Models;

/// <summary>
/// 用户信息
/// </summary>
public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("user_group")]
    public int UserGroup { get; set; }

    [JsonPropertyName("avatar")]
    public Avatar? Avatar { get; set; }

    [JsonPropertyName("sign")]
    public string Sign { get; set; } = string.Empty;
}
