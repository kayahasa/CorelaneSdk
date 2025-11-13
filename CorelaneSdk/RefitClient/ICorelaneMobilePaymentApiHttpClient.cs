using CorelaneSdk.Models.MobilePaymentApi.AppleServerNotification;
using CorelaneSdk.Models.MobilePaymentApi.GoogleNotification;
using CorelaneSdk.Models.MobilePaymentApi.VerifyReceipt;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneMobilePaymentApiHttpClient
{
    [Post("/api/v1/MobilePaymentApi/VerifyReceipt")]
    Task<Models.Core.ApiResponse<VerifyReceiptResponse>> VerifyReceiptAsync([Body] VerifyReceiptRequest request);

    [Post("/api/v1/MobilePaymentApi/AppleServerNotification")]
    Task<Models.Core.ApiResponse<bool>> AppleServerNotificationAsync([Body] AppleNotificationRequest request);

    [Post("/api/v1/MobilePaymentApi/GoogleServerNotification")]
    Task<Models.Core.ApiResponse<bool>> GoogleServerNotificationAsync([Body] GoogleNotificationRequest request);
}
