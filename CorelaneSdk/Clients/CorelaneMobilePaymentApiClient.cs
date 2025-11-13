using CorelaneSdk.Attributes;
using CorelaneSdk.Enums.Core;
using CorelaneSdk.Models;
using CorelaneSdk.RefitClient;

namespace CorelaneSdk.Clients;

public interface ICorelaneMobilePaymentApiClient
{
    [CorelaneEndpoint("/VerifyReceipt", HttpMethodEnum.POST, ApiTypeEnum.MobilePaymentApi)]
    Task<ApiResponse<VerifyReceiptResponse>> VerifyReceiptAsync(VerifyReceiptRequest request);

    [CorelaneEndpoint("/AppleServerNotification", HttpMethodEnum.POST, ApiTypeEnum.MobilePaymentApi, true)]
    Task<ApiResponse<bool>> AppleServerNotificationAsync(AppleNotificationRequest request);

    [CorelaneEndpoint("/GoogleServerNotification", HttpMethodEnum.POST, ApiTypeEnum.MobilePaymentApi, true)]
    Task<ApiResponse<bool>> GoogleServerNotificationAsync(GoogleNotificationRequest request);
}

public class CorelaneMobilePaymentApiClient : ICorelaneMobilePaymentApiClient
{
    private readonly ICorelaneMobilePaymentApiHttpClient _corelaneMobilePaymentApiHttpClient;

    public CorelaneMobilePaymentApiClient(ICorelaneMobilePaymentApiHttpClient corelaneMobilePaymentApiHttpClient)
    {
        _corelaneMobilePaymentApiHttpClient = corelaneMobilePaymentApiHttpClient;
    }

    public async Task<ApiResponse<VerifyReceiptResponse>> VerifyReceiptAsync(VerifyReceiptRequest request)
    {
        return await _corelaneMobilePaymentApiHttpClient.VerifyReceiptAsync(request);
    }

    public async Task<ApiResponse<bool>> AppleServerNotificationAsync(AppleNotificationRequest request)
    {
        return await _corelaneMobilePaymentApiHttpClient.AppleServerNotificationAsync(request);
    }

    public async Task<ApiResponse<bool>> GoogleServerNotificationAsync(GoogleNotificationRequest request)
    {
        return await _corelaneMobilePaymentApiHttpClient.GoogleServerNotificationAsync(request);
    }
}
