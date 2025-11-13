using System.Text.Json.Serialization;

namespace CorelaneSdk.Models.MobilePaymentApi.AppleServerNotification;

public class AppleNotificationRequest
{
    [JsonPropertyName("signedPayload")]
    public string SignedPayload { get; set; }
}
