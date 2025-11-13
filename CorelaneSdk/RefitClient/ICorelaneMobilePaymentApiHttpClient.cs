using CorelaneSdk.Models;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneMobilePaymentApiHttpClient
{
    [Post("/api/v1/MobilePaymentApi/VerifyReceipt")]
    Task<Models.ApiResponse<VerifyReceiptResponse>> VerifyReceiptAsync([Body] VerifyReceiptRequest request);

    [Post("/api/v1/MobilePaymentApi/AppleServerNotification")]
    Task<Models.ApiResponse<bool>> AppleServerNotificationAsync([Body] AppleNotificationRequest request);

    [Post("/api/v1/MobilePaymentApi/GoogleServerNotification")]
    Task<Models.ApiResponse<bool>> GoogleServerNotificationAsync([Body] GoogleNotificationRequest request);
}
