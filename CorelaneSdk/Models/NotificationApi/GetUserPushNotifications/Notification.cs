using CorelaneSdk.Enums.NotificationApi;
using System.Text.Json.Serialization;

namespace CorelaneSdk.Models.NotificationApi.GetUserPushNotifications;

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
