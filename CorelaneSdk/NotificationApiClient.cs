using CorelaneSdk.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace CorelaneSdk;

public interface INotificationApiClient
{
    Task<ApiResponse<bool>> SendEmailAsync(EmailMessageEvent request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> SendTelegramMessageAsync(TelegramMessageEvent request, CancellationToken cancellationToken = default);
}

public class NotificationApiClient : INotificationApiClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    // This client is internal, as it should only be created by the main CorelaneApiClient
    internal NotificationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponse<bool>> SendTelegramMessageAsync(TelegramMessageEvent request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/NotificationApi/SendTelegramMessage", request, cancellationToken);

    public Task<ApiResponse<bool>> SendEmailAsync(EmailMessageEvent request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/NotificationApi/SendEmail", request, cancellationToken);

    #region Private Helper Methods
    private async Task<ApiResponse<T>> SendRequestAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.SendAsync(request, cancellationToken);
        await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<ApiResponse<T>>(responseStream, _jsonOptions, cancellationToken)
                   ?? throw new InvalidOperationException("Failed to deserialize API response.");
        }

        var errorResponse = await JsonSerializer.DeserializeAsync<ApiResponse<T>>(responseStream, _jsonOptions, cancellationToken);
        throw new ApiException(errorResponse, response.StatusCode);
    }

    private async Task<ApiResponse<T>> PostAsync<T>(string url, object? body, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (body != null)
        {
            request.Content = JsonContent.Create(body);
        }
        return await SendRequestAsync<T>(request, cancellationToken);
    }

    private async Task<ApiResponse<T>> GetAsync<T>(string url, Dictionary<string, string?>? queryParams, CancellationToken cancellationToken)
    {
        var uri = queryParams != null ? QueryHelpers.AddQueryString(url, queryParams) : url;
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequestAsync<T>(request, cancellationToken);
    }
    #endregion
}
