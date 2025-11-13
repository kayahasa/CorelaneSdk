namespace CorelaneSdk.Models.MobilePaymentApi.VerifyReceipt;

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
