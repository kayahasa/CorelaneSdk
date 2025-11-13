using CorelaneSdk.Attributes;
using CorelaneSdk.Enums.Core;
using CorelaneSdk.Models.Core;
using CorelaneSdk.Models.NotificationApi.GetUserPushNotifications;
using CorelaneSdk.Models.NotificationApi.SendPushNotification;
using CorelaneSdk.RefitClient;

namespace CorelaneSdk.Clients;

public interface ICorelaneNotificationApiClient
{
    Task<ApiResponse<bool>> SendPushNotificationAsync(SendPushNotificationRequest request);

    [CorelaneEndpoint("/GetUserPushNotifications", HttpMethodEnum.GET, ApiTypeEnum.NotificationApi)]
    Task<ApiResponse<List<Notification>>> GetUserPushNotificationsAsync(string fcmToken);

    [CorelaneEndpoint("/ReadAllPushNotifications", HttpMethodEnum.GET, ApiTypeEnum.NotificationApi)]
    Task<ApiResponse<bool>> ReadAllPushNotificationsAsync();

    [CorelaneEndpoint("/ReadPushNotification", HttpMethodEnum.GET, ApiTypeEnum.NotificationApi)]
    Task<ApiResponse<bool>> ReadPushNotificationAsync(Guid notificationId);
}

public class CorelaneNotificationApiClient : ICorelaneNotificationApiClient
{
    private readonly ICorelaneNotificationApiHttpClient _corelaneNotificationApiHttpClient;

    public CorelaneNotificationApiClient(ICorelaneNotificationApiHttpClient corelaneNotificationApiHttpClient)
    {
        _corelaneNotificationApiHttpClient = corelaneNotificationApiHttpClient;
    }

    public async Task<ApiResponse<List<Notification>>> GetUserPushNotificationsAsync(string fcmToken)
    {
        return await _corelaneNotificationApiHttpClient.GetUserPushNotificationsAsync(fcmToken);
    }

    public async Task<ApiResponse<bool>> ReadAllPushNotificationsAsync()
    {
        return await _corelaneNotificationApiHttpClient.ReadAllPushNotificationsAsync();
    }

    public async Task<ApiResponse<bool>> SendPushNotificationAsync(SendPushNotificationRequest request)
    {
        return await _corelaneNotificationApiHttpClient.SendPushNotificationAsync(request);
    }

    public async Task<ApiResponse<bool>> ReadPushNotificationAsync(Guid notificationId)
    {
        return await _corelaneNotificationApiHttpClient.ReadPushNotificationAsync(notificationId);
    }
}
