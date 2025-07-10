using Corelane.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace CorelaneSdk.Extensions;

public static class ServiceCollectionExtensions
{
    internal const string CorelaneSdkBaseUrl = "http://91.99.132.165:8004";

    public static IServiceCollection AddCorelaneSDK(this IServiceCollection services, string apiKey)
    {
        // This is the corrected line.
        // The second generic argument must be the concrete implementation of the interface.
        services.AddHttpClient<ICorelaneApiClient, CorelaneApiClient>((serviceProvider, client) =>
        {
            if (string.IsNullOrWhiteSpace(CorelaneSdkBaseUrl) || !Uri.IsWellFormedUriString(CorelaneSdkBaseUrl, UriKind.Absolute))
            {
                throw new InvalidOperationException("The BaseUrl must be a valid, absolute URI.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("ApiKey cannot be null or empty.");
            }

            client.BaseAddress = new Uri(CorelaneSdkBaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}
