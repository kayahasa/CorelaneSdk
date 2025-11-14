using CorelaneSdk.Enums.Common;
using CorelaneSdk.Enums.UserApi;
using System.Text.Json.Serialization;

namespace CorelaneSdk.Models;

public class UserSearchResult
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}

public class UpdateUserProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Avatar { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Instagram { get; set; }
    public string? City { get; set; }
    public CorelaneLanguageEnum? Language { get; set; }
    public DateTime? Birthday { get; set; }
    public GenderEnum? Gender { get; set; }
}

public class SignUpWithPhoneNumberRequest
{
    public string PhoneNumber { get; set; }
    public string? ReferencedBy { get; set; }
    public bool DocumentsAccepted { get; set; }
    public string? FcmToken { get; set; }
    public string? Platform { get; set; }
    public CorelaneLanguageEnum Language { get; set; }
}

public class SignInWithSmsOtpRequest
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
    public string VerificationId { get; set; }
}

public class RefreshTokenRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}

public class LoginWithPasswordRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class WorkingTime
{
    public bool Value { get; set; }
    public string? Description { get; set; }
}

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

public class Policy
{
    public CorelaneLanguageEnum Language { get; set; }
    public PolicyTypeEnum PolicyType { get; set; }
    public string Content { get; set; }
}

public class FaqItem
{
    public CorelaneLanguageEnum Language { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}

public class AppVersion
{
    public string MinVersion { get; set; }
    public string LatestVersion { get; set; }
    public bool ForceUpdate { get; set; }
}

public class CreateFeedBackRequest
{
    public string Email { get; set; }
    public string Message { get; set; }
    public Guid? UserId { get; set; }
}

public class Feedback
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public bool IsResolved { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class LoginResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? JwtExpireDate { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public string? Avatar { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsFirstLogin { get; set; }
}

public class SignUpWithAnonymousRequest
{
    public string? FcmToken { get; set; }
    public string? Platform { get; set; }
    public LanguageEnum? Language { get; set; }
}

public class SendSmsOtpRequest
{
    public string PhoneNumber { get; set; }
}
