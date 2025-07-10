using System.Text.Json.Serialization;

namespace CorelaneSdk.Models
{   

    // Generic API Response Wrapper
    public record ApiResponse<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }
        public List<string>? Errors { get; init; }
        public string? ErrorCode { get; init; }
        public int StatusCode { get; init; }
    }

    // Request Models
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword, string? UserId = null);
    public record LoginWithPasswordRequest(string Email, string Password);
    public record LoginWithTwoFaRequest(string Code, string TemporaryToken, TwoFactorMethodEnum Method);
    public record RefreshTokenRequest(string? AccessToken, string? RefreshToken, Guid? ProjectId = null);
    public record ResetPasswordRequest(string Email, string ResetCode, string NewPassword);
    public record ToggleTotpRequest(string Totp, bool Enabled, TwoFactorMethodEnum Method, string? UserId = null);

    // Response Data Models
    public record LoginResponse
    {
        public bool RequiresTwoFactor { get; init; }
        public string? TemporaryToken { get; init; }
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
        public string? PhoneNumber { get; init; }
        public DateTime? JwtExpireDate { get; init; }
        public DateTime? RefreshTokenExpireDate { get; init; }
        public List<string>? TwoFaProviders { get; init; }
    }

    public record LoginWithTwoFaResponse
    {
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime? JwtExpireDate { get; init; }
        public DateTime? RefreshTokenExpireDate { get; init; }
    }

    public record UserProfileInfoDto
    {
        public Guid Id { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool PhoneNumberConfirmed { get; init; }
        public required string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Avatar { get; init; }
        public DateTime CreatedAt { get; init; }
    }

    // Enums
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TwoFactorMethodEnum
    {
        Email,
        Phone,
        Authenticator
    }
}
