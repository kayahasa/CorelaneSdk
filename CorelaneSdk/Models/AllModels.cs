using System.Text.Json.Serialization;

namespace CorelaneSdk.Models
{
    #region UserApi
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
    #endregion

    #region NotificationApi
    public abstract class BaseEvent
    {
        public Guid ProjectId { get; set; }
    }

    public enum EventTypeEnum
    {
        Email,
        Sms,
        PushNotification,
        InAppNotification,
        TelegramMessage
    }

    public abstract class NotificationEvent : BaseEvent
    {
        public EventTypeEnum EventType { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string? TemplateName { get; set; }
        public bool UseTemplate { get; set; } = false;
        public bool IsHtml { get; set; } = false;
        public Dictionary<string, string> Parameters { get; set; } = new();
    }

    public class EmailMessageEvent : NotificationEvent
    {
        public EmailMessageEvent()
        {

            EventType = EventTypeEnum.Email;
        }
    }

    public class TelegramMessageEvent : NotificationEvent
    {
        public TelegramMessageEvent()
        {
            EventType = EventTypeEnum.TelegramMessage;
        }
        public bool DisableNotification { get; set; } // If true, the message will be sent silently
        public int? ReplyToMessageId { get; set; }
    }
    #endregion
}
