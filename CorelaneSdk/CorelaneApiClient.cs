using CorelaneSdk;

namespace Corelane.Sdk
{
    /// <summary>
    /// The main client to access all Corelane APIs.
    /// </summary>
    public interface ICorelaneApiClient
    {
        /// <summary>
        /// Access the User Management API.
        /// </summary>
        IUserApiClient UserApi { get; }
        /// <summary>
        /// Access the Notification Management API.
        /// </summary>
        INotificationApiClient NotificationApi { get; }
    }

    public class CorelaneApiClient : ICorelaneApiClient
    {
        private readonly HttpClient _httpClient;

        public IUserApiClient UserApi { get; }
        public INotificationApiClient NotificationApi { get; }

        public CorelaneApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            UserApi = new UserApiClient(_httpClient);
            NotificationApi = new NotificationApiClient(_httpClient);
        }
    }
}
