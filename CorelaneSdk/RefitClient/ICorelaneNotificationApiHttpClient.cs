using CorelaneSdk.Models.NotificationApi.GetUserPushNotifications;
using CorelaneSdk.Models.NotificationApi.SendPushNotification;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneNotificationApiHttpClient
{
    [Post("/api/v1/NotificationApi/SendPushNotification")]
    Task<Models.Core.ApiResponse<bool>> SendPushNotificationAsync(SendPushNotificationRequest request);

    [Get("/api/v1/NotificationApi/GetUserPushNotifications")]
    Task<Models.Core.ApiResponse<List<Notification>>> GetUserPushNotificationsAsync([FromQuery] string fcmToken);

    [Get("/api/v1/NotificationApi/ReadAllPushNotifications")]
    Task<Models.Core.ApiResponse<bool>> ReadAllPushNotificationsAsync();

    [Get("/api/v1/NotificationApi/ReadPushNotification")]
    Task<Models.Core.ApiResponse<bool>> ReadPushNotificationAsync([FromQuery] Guid notificationId);
}
