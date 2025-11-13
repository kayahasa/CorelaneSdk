using CorelaneSdk.Clients;
using CorelaneSdk.Handlers;
using CorelaneSdk.RefitClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CorelaneSdk.Extensions;

public static class StartupExtension
{
    private const string CORELANE_SDK_BASE_URL = "https://prod-api.getcorelane.com/bff";
    private const int CORELANE_SDK_TIMEOUT_MINUTES = 60;

    public static IServiceCollection AddCorelaneSdk(this IServiceCollection services, string apiKey, TimeSpan? timeout = null)
    {
        services.AddHttpContextAccessor();

        services.AddTransient(provider =>
        {
            var httpContextAccessor = new HttpContextAccessor();
            return new CorelaneHandler(apiKey, httpContextAccessor);
        });

        services.AddRefitClient<ICorelaneBffUserApiHttpClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(CORELANE_SDK_BASE_URL);
                client.Timeout = timeout ?? TimeSpan.FromMinutes(CORELANE_SDK_TIMEOUT_MINUTES);
            })
            .AddHttpMessageHandler<CorelaneHandler>();

        services.AddRefitClient<ICorelaneBfaUserApiHttpClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(CORELANE_SDK_BASE_URL);
                client.Timeout = timeout ?? TimeSpan.FromMinutes(CORELANE_SDK_TIMEOUT_MINUTES);
            })
            .AddHttpMessageHandler<CorelaneHandler>();

        services.AddRefitClient<ICorelaneNotificationApiHttpClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(CORELANE_SDK_BASE_URL);
                client.Timeout = timeout ?? TimeSpan.FromMinutes(CORELANE_SDK_TIMEOUT_MINUTES);
            })
            .AddHttpMessageHandler<CorelaneHandler>();

        services.AddRefitClient<ICorelaneMobilePaymentApiHttpClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(CORELANE_SDK_BASE_URL);
                client.Timeout = timeout ?? TimeSpan.FromMinutes(CORELANE_SDK_TIMEOUT_MINUTES);
            })
            .AddHttpMessageHandler<CorelaneHandler>();

        services.AddRefitClient<ICorelaneFirebaseHttpClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(CORELANE_SDK_BASE_URL);
                client.Timeout = timeout ?? TimeSpan.FromMinutes(CORELANE_SDK_TIMEOUT_MINUTES);
            })
            .AddHttpMessageHandler<CorelaneHandler>();

        services.AddSingleton<ICorelaneBffUserApiClient, CorelaneBffUserApiClient>();
        services.AddSingleton<ICorelaneBfaUserApiClient, CorelaneBfaUserApiClient>();
        services.AddSingleton<ICorelaneNotificationApiClient, CorelaneNotificationApiClient>();
        services.AddSingleton<ICorelaneMobilePaymentApiClient, CorelaneMobilePaymentApiClient>();
        services.AddSingleton<ICorelaneFirebaseClient, CorelaneFirebaseClient>();

        return services;
    }
}
