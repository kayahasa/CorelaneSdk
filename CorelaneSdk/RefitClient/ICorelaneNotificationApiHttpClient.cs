using CorelaneSdk.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneNotificationApiHttpClient
{
    [Post("/api/v1/NotificationApi/SendPushNotification")]
    Task<Models.ApiResponse<bool>> SendPushNotificationAsync(SendPushNotificationRequest request);

    [Get("/api/v1/NotificationApi/GetUserPushNotifications")]
    Task<Models.ApiResponse<List<Notification>>> GetUserPushNotificationsAsync([FromQuery] string fcmToken);

    [Get("/api/v1/NotificationApi/ReadAllPushNotifications")]
    Task<Models.ApiResponse<bool>> ReadAllPushNotificationsAsync();

    [Get("/api/v1/NotificationApi/ReadPushNotification")]
    Task<Models.ApiResponse<bool>> ReadPushNotificationAsync([FromQuery] Guid notificationId);
}
