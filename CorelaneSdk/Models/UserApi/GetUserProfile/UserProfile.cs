using CorelaneSdk.Enums.Common;
using System.Text.Json.Serialization;

namespace CorelaneSdk.Models.UserApi.GetUserProfile;

public class UserProfile
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("emailConfirmed")]
    public bool EmailConfirmed { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")]
    public bool PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    [JsonPropertyName("instagram")]
    public string? Instagram { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("birthday")]
    public DateTime? Birthday { get; set; }
    [JsonPropertyName("gender")]
    public GenderEnum? Gender { get; set; }
    [JsonPropertyName("language")]
    public CorelaneLanguageEnum Language { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
