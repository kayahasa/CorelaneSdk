namespace CorelaneSdk.Models.MobilePaymentApi.VerifyReceipt;

public class VerifyReceiptResponse
{
    public bool Success { get; set; } = true;
    public string ProductId { get; set; }
    public double CreditAdded { get; set; }
    public bool PremiumActivated { get; set; }
    public string Message { get; set; }
}
