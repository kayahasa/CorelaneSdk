using CorelaneSdk.Enums.Common;
using System.Text.Json.Serialization;

namespace CorelaneSdk.Models;

public class NotificationMessage
{
    public NotificationMessage() { }
    public NotificationMessage(string title, string body)
    {
        Title = title;
        Body = body;
    }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class SendPushNotificationRequest
{
    public List<Guid> UserIds { get; set; } = new();
    public required string Title { get; set; }
    public required string Body { get; set; }
    public string? Deeplink { get; set; } = null;
    public required List<string> Tokens { get; set; }
    public Dictionary<CorelaneLanguageEnum, NotificationMessage>? Translations { get; set; } = null;
}

public class Notification
{
    public Guid Id { get; set; }
    [JsonPropertyName("eventType")]
    public EventTypeEnum EventType { get; set; }
    [JsonPropertyName("to")]
    public string? To { get; set; }
    [JsonPropertyName("body")]
    public string? Body { get; set; }
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }
    [JsonPropertyName("templateName")]
    public string? TemplateName { get; set; }
    [JsonPropertyName("group")]
    public Guid Group { get; set; }
    [JsonPropertyName("useTemplate")]
    public bool UseTemplate { get; set; } = false;
    [JsonPropertyName("deeplink")]
    public string? Deeplink { get; set; } = null;
    [JsonPropertyName("isHtml")]
    public bool IsHtml { get; set; } = false;
    [JsonPropertyName("read")]
    public bool? Read { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
}

public enum EventTypeEnum
{
    Email,
    Sms,
    PushNotification,
    InAppNotification,
    TelegramMessage
}
