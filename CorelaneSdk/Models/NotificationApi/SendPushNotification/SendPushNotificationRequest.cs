using CorelaneSdk.Enums.Common;

namespace CorelaneSdk.Models.NotificationApi.SendPushNotification;

public class SendPushNotificationRequest
{
    public List<Guid> UserIds { get; set; } = new();
    public required string Title { get; set; }
    public required string Body { get; set; }
    public string? Deeplink { get; set; } = null;
    public required List<string> Tokens { get; set; }
    public Dictionary<CorelaneLanguageEnum, NotificationMessage>? Translations { get; set; } = null;
}
