using System.Text.Json.Serialization;

namespace CorelaneSdk.Enums.NotificationApi;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventTypeEnum
{
    Email,
    Sms,
    PushNotification,
    InAppNotification,
    TelegramMessage
}
