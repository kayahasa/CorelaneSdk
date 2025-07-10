using Corelane.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace CorelaneSdk.Extensions;

public static class ServiceCollectionExtensions
{
    internal const string CorelaneSdkBaseUrl = "http://91.99.132.165:8004";

    public static IServiceCollection AddCorelaneSDK(this IServiceCollection services, string apiKey)
    {
        if (string.IsNullOrWhiteSpace(CorelaneSdkBaseUrl) || !Uri.IsWellFormedUriString(CorelaneSdkBaseUrl, UriKind.Absolute))
        {
            throw new InvalidOperationException("The BaseUrl must be a valid, absolute URI.");
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("ApiKey cannot be null or empty.");
        }

        // Register HttpClient with a name
        services.AddHttpClient("CorelaneApiClient", client =>
        {
            client.BaseAddress = new Uri(CorelaneSdkBaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Register the CorelaneApiClient as a singleton or scoped service
        services.AddScoped<ICorelaneApiClient>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("CorelaneApiClient");
            return new CorelaneApiClient(httpClient);
        });

        return services;
    }
}