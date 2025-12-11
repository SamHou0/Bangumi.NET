using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Bangumi.NET.Models;

namespace Bangumi.NET;

/// <summary>
/// Bangumi API 客户端
/// </summary>
public class BangumiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _userAgent;
    private const string BaseUrl = "https://api.bgm.tv";

    /// <summary>
    /// 初始化 Bangumi API 客户端
    /// </summary>
    /// <param name="apiKey">API 访问令牌</param>
    /// <param name="userAgent">User-Agent 标识</param>
    public BangumiClient(string apiKey, string userAgent)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentNullException(nameof(apiKey));
        if (string.IsNullOrWhiteSpace(userAgent))
            throw new ArgumentNullException(nameof(userAgent));

        _apiKey = apiKey;
        _userAgent = userAgent;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", _userAgent);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #region Subject APIs

    /// <summary>
    /// 搜索条目
    /// </summary>
    public async Task<PagedResult<Subject>> SearchSubjectsAsync(SubjectSearchRequest request, int limit = 30, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"/v0/search/subjects?limit={limit}&offset={offset}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<Subject>>(cancellationToken) ?? new PagedResult<Subject>();
    }

    /// <summary>
    /// 获取条目信息
    /// </summary>
    public async Task<Subject?> GetSubjectByIdAsync(int subjectId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/subjects/{subjectId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Subject>(cancellationToken);
    }

    /// <summary>
    /// 批量获取条目信息
    /// </summary>
    public async Task<Dictionary<string, Subject>> GetSubjectsAsync(List<int> subjectIds, CancellationToken cancellationToken = default)
    {
        var ids = string.Join(",", subjectIds);
        var response = await _httpClient.GetAsync($"/v0/subjects?ids={ids}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Dictionary<string, Subject>>(cancellationToken) ?? new Dictionary<string, Subject>();
    }

    /// <summary>
    /// 获取条目相关角色
    /// </summary>
    public async Task<List<RelatedCharacter>> GetRelatedCharactersBySubjectIdAsync(int subjectId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/subjects/{subjectId}/characters", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<RelatedCharacter>>(cancellationToken) ?? new List<RelatedCharacter>();
    }

    /// <summary>
    /// 获取条目相关人物
    /// </summary>
    public async Task<List<RelatedPerson>> GetRelatedPersonsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/subjects/{subjectId}/persons", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<RelatedPerson>>(cancellationToken) ?? new List<RelatedPerson>();
    }

    /// <summary>
    /// 获取条目关联条目
    /// </summary>
    public async Task<List<SubjectRelation>> GetRelatedSubjectsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/subjects/{subjectId}/subjects", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<SubjectRelation>>(cancellationToken) ?? new List<SubjectRelation>();
    }

    #endregion

    #region Episode APIs

    /// <summary>
    /// 获取剧集列表
    /// </summary>
    public async Task<PagedResult<Episode>> GetEpisodesAsync(int subjectId, int? episodeType = null, int limit = 100, int offset = 0, CancellationToken cancellationToken = default)
    {
        var url = $"/v0/episodes?subject_id={subjectId}&limit={limit}&offset={offset}";
        if (episodeType.HasValue)
            url += $"&type={episodeType.Value}";

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<Episode>>(cancellationToken) ?? new PagedResult<Episode>();
    }

    /// <summary>
    /// 获取剧集详情
    /// </summary>
    public async Task<EpisodeDetail?> GetEpisodeByIdAsync(int episodeId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/episodes/{episodeId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EpisodeDetail>(cancellationToken);
    }

    #endregion

    #region Character APIs

    /// <summary>
    /// 搜索角色
    /// </summary>
    public async Task<PagedResult<Character>> SearchCharactersAsync(CharacterSearchRequest request, int limit = 30, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"/v0/search/characters?limit={limit}&offset={offset}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<Character>>(cancellationToken) ?? new PagedResult<Character>();
    }

    /// <summary>
    /// 获取角色信息
    /// </summary>
    public async Task<Character?> GetCharacterByIdAsync(int characterId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/characters/{characterId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Character>(cancellationToken);
    }

    /// <summary>
    /// 获取角色相关条目
    /// </summary>
    public async Task<List<CharacterSubject>> GetRelatedSubjectsByCharacterIdAsync(int characterId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/characters/{characterId}/subjects", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CharacterSubject>>(cancellationToken) ?? new List<CharacterSubject>();
    }

    /// <summary>
    /// 获取角色相关人物
    /// </summary>
    public async Task<List<CharacterPerson>> GetRelatedPersonsByCharacterIdAsync(int characterId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/characters/{characterId}/persons", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CharacterPerson>>(cancellationToken) ?? new List<CharacterPerson>();
    }

    #endregion

    #region Person APIs

    /// <summary>
    /// 搜索人物
    /// </summary>
    public async Task<PagedResult<Person>> SearchPersonsAsync(PersonSearchRequest request, int limit = 30, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"/v0/search/persons?limit={limit}&offset={offset}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<Person>>(cancellationToken) ?? new PagedResult<Person>();
    }

    /// <summary>
    /// 获取人物信息
    /// </summary>
    public async Task<Person?> GetPersonByIdAsync(int personId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/persons/{personId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Person>(cancellationToken);
    }

    /// <summary>
    /// 获取人物相关角色
    /// </summary>
    public async Task<List<PersonCharacter>> GetRelatedCharactersByPersonIdAsync(int personId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/persons/{personId}/characters", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<PersonCharacter>>(cancellationToken) ?? new List<PersonCharacter>();
    }

    /// <summary>
    /// 获取人物相关条目
    /// </summary>
    public async Task<List<PersonSubject>> GetRelatedSubjectsByPersonIdAsync(int personId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/persons/{personId}/subjects", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<PersonSubject>>(cancellationToken) ?? new List<PersonSubject>();
    }

    #endregion

    #region User APIs

    /// <summary>
    /// 获取用户信息
    /// </summary>
    public async Task<User?> GetUserByNameAsync(string username, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken);
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    public async Task<User?> GetMyselfAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/v0/me", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>(cancellationToken);
    }

    #endregion

    #region Collection APIs

    /// <summary>
    /// 获取用户收藏
    /// </summary>
    public async Task<PagedResult<UserSubjectCollection>> GetUserCollectionsAsync(
        string username, 
        int? subjectType = null, 
        int? collectionType = null, 
        int limit = 30, 
        int offset = 0, 
        CancellationToken cancellationToken = default)
    {
        var url = $"/v0/users/{username}/collections?limit={limit}&offset={offset}";
        if (subjectType.HasValue)
            url += $"&subject_type={subjectType.Value}";
        if (collectionType.HasValue)
            url += $"&type={collectionType.Value}";

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<UserSubjectCollection>>(cancellationToken) ?? new PagedResult<UserSubjectCollection>();
    }

    /// <summary>
    /// 获取指定条目收藏信息
    /// </summary>
    public async Task<UserSubjectCollection?> GetUserCollectionAsync(string username, int subjectId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/{subjectId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserSubjectCollection>(cancellationToken);
    }

    /// <summary>
    /// 创建或修改收藏
    /// </summary>
    public async Task<UserSubjectCollection?> UpdateUserCollectionAsync(int subjectId, UserCollectionModifyRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"/v0/users/-/collections/{subjectId}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserSubjectCollection>(cancellationToken);
    }

    /// <summary>
    /// 更新收藏
    /// </summary>
    public async Task PatchUserCollectionAsync(int subjectId, UserCollectionModifyRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsJsonAsync($"/v0/users/-/collections/{subjectId}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// 获取用户章节收藏
    /// </summary>
    public async Task<UserEpisodeCollection?> GetUserSubjectEpisodeCollectionAsync(string username, int subjectId, int? episodeType = null, CancellationToken cancellationToken = default)
    {
        var url = $"/v0/users/{username}/collections/{subjectId}/episodes";
        if (episodeType.HasValue)
            url += $"?episode_type={episodeType.Value}";

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserEpisodeCollection>(cancellationToken);
    }

    /// <summary>
    /// 批量更新章节收藏
    /// </summary>
    public async Task PatchUserSubjectEpisodeCollectionAsync(int subjectId, EpisodeCollectionBatchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsJsonAsync($"/v0/users/-/collections/{subjectId}/episodes", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// 获取单集收藏状态
    /// </summary>
    public async Task<EpisodeCollectionInfo?> GetUserEpisodeCollectionAsync(string username, int episodeId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/-/episodes/{episodeId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EpisodeCollectionInfo>(cancellationToken);
    }

    /// <summary>
    /// 更新单集收藏状态
    /// </summary>
    public async Task PutUserEpisodeCollectionAsync(int episodeId, EpisodeCollectionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/v0/users/-/collections/-/episodes/{episodeId}", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// 获取用户角色收藏列表
    /// </summary>
    public async Task<PagedResult<UserCharacterCollection>> GetUserCharacterCollectionsAsync(string username, int limit = 30, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/-/characters?limit={limit}&offset={offset}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<UserCharacterCollection>>(cancellationToken) ?? new PagedResult<UserCharacterCollection>();
    }

    /// <summary>
    /// 获取用户单个角色收藏信息
    /// </summary>
    public async Task<UserCharacterCollection?> GetUserCharacterCollectionAsync(string username, int characterId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/-/characters/{characterId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserCharacterCollection>(cancellationToken);
    }

    /// <summary>
    /// 获取用户人物收藏列表
    /// </summary>
    public async Task<PagedResult<UserPersonCollection>> GetUserPersonCollectionsAsync(string username, int limit = 30, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/-/persons?limit={limit}&offset={offset}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResult<UserPersonCollection>>(cancellationToken) ?? new PagedResult<UserPersonCollection>();
    }

    /// <summary>
    /// 获取用户单个人物收藏信息
    /// </summary>
    public async Task<UserPersonCollection?> GetUserPersonCollectionAsync(string username, int personId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v0/users/{username}/collections/-/persons/{personId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserPersonCollection>(cancellationToken);
    }

    #endregion

    #region Calendar API

    /// <summary>
    /// 获取每日放送
    /// </summary>
    public async Task<List<CalendarItem>> GetCalendarAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/calendar", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CalendarItem>>(cancellationToken) ?? new List<CalendarItem>();
    }

    #endregion

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
