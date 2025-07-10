using Corelane.Sdk;
using CorelaneSdk.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CorelaneSdk.Extensions
{
    public static class RegisterExtensions
    {
        public static IServiceCollection AddCoreApiClient(this IServiceCollection services, Action<CorelaneApiClientOptions> configureOptions)
        {
            // Configure the options from the user
            services.Configure(configureOptions);

            // This is the corrected line.
            // The second generic argument must be the concrete implementation of the interface.
            services.AddHttpClient<ICorelaneApiClient, CorelaneApiClient>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<CorelaneApiClientOptions>>().Value;

                if (string.IsNullOrWhiteSpace(options.BaseUrl) || !Uri.IsWellFormedUriString(options.BaseUrl, UriKind.Absolute))
                {
                    throw new InvalidOperationException("The BaseUrl must be a valid, absolute URI.");
                }

                if (string.IsNullOrWhiteSpace(options.ApiKey))
                {
                    throw new InvalidOperationException("ApiKey cannot be null or empty.");
                }

                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("ApiKey", options.ApiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
