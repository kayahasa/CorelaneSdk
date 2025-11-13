using System.Text.Json.Serialization;

namespace CorelaneSdk.Models;

public class VerifyReceiptRequest
{
    public string Platform { get; set; } // "ios" or "android"
    public string ProductId { get; set; }
    public string? TransactionId { get; set; } // For iOS
    public string? ReceiptData { get; set; } // Purchase Token for Android
    public string? BasePlanId { get; set; } // For Android Subscriptions
    public string? DeviceId { get; set; }
    public Guid? UserId { get; set; }
    public bool IsSubscription { get; set; }
}

public class VerifyReceiptResponse
{
    public bool Success { get; set; } = true;
    public string ProductId { get; set; }
    public double CreditAdded { get; set; }
    public bool PremiumActivated { get; set; }
    public string Message { get; set; }
}

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

public class AppleNotificationRequest
{
    [JsonPropertyName("signedPayload")]
    public string SignedPayload { get; set; }
}
