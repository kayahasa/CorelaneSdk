using System.Text.Json.Serialization;

namespace CorelaneSdk.Models.MobilePaymentApi.GoogleNotification;

public class GoogleNotificationRequest
{
    [JsonPropertyName("message")]
    public GoogleRtdnMessage Message { get; set; }
}

public class GoogleRtdnMessage
{
    [JsonPropertyName("data")]
    public string Data { get; set; } // Base64 encoded JSON
}
