using CorelaneSdk.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace CorelaneSdk;

public interface IUserApiClient
{
    Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserProfileInfoDto>> GetUserProfileAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<List<string>>> EnableTwoFactorAsync(string totp, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DisableTwoFactorAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> GetTwoFactorStatusAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> GetAuthenticatorSetupAsync(string appName, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> ToggleTwoFactorAsync(ToggleTotpRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginWithPasswordRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> SendConfirmationEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> LogoutFromAllDevicesAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<LoginWithTwoFaResponse>> LoginWith2FaAsync(LoginWithTwoFaRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken = default);
}

/// <summary>
/// Handles all requests to the User API endpoints.
/// </summary>
public class UserApiClient : IUserApiClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    // This client is internal, as it should only be created by the main CorelaneApiClient
    internal UserApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/ChangePassword", request, cancellationToken);

    public Task<ApiResponse<UserProfileInfoDto>> GetUserProfileAsync(CancellationToken cancellationToken = default)
        => GetAsync<UserProfileInfoDto>("/api/v1/UserApi/GetUserProfile", null, cancellationToken);

    public Task<ApiResponse<List<string>>> EnableTwoFactorAsync(string totp, CancellationToken cancellationToken = default)
        => GetAsync<List<string>>("/api/v1/UserApi/EnableTwoFactor", new() { { "totp", totp } }, cancellationToken);

    public Task<ApiResponse<bool>> DisableTwoFactorAsync(CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/DisableTwoFactor", null, cancellationToken);

    public Task<ApiResponse<bool>> GetTwoFactorStatusAsync(CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/GetTwoFactorStatus", null, cancellationToken);

    public Task<ApiResponse<bool>> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
        => GetAsync<bool>("/api/v1/UserApi/ForgotPassword", new() { { "email", email } }, cancellationToken);

    public Task<ApiResponse<bool>> GetAuthenticatorSetupAsync(string appName, CancellationToken cancellationToken = default)
        => PostAsync<bool>($"/api/v1/UserApi/GetAuthenticatorSetup?appName={appName}", null, cancellationToken);

    public Task<ApiResponse<bool>> ToggleTwoFactorAsync(ToggleTotpRequest request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/ToggleTwoFactor", request, cancellationToken);

    public Task<ApiResponse<LoginResponse>> LoginAsync(LoginWithPasswordRequest request, CancellationToken cancellationToken = default)
        => PostAsync<LoginResponse>("/api/v1/UserApi/Login", request, cancellationToken);

    public Task<ApiResponse<bool>> SendConfirmationEmailAsync(string email, CancellationToken cancellationToken = default)
        => GetAsync<bool>("/api/v1/UserApi/SendConfirmationEmail", new() { { "email", email } }, cancellationToken);

    public Task<ApiResponse<bool>> LogoutFromAllDevicesAsync(CancellationToken cancellationToken = default)
        => GetAsync<bool>("/api/v1/UserApi/LogoutFromAllDevices", null, cancellationToken);

    public Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/ResetPassword", request, cancellationToken);

    public Task<ApiResponse<LoginWithTwoFaResponse>> LoginWith2FaAsync(LoginWithTwoFaRequest request, CancellationToken cancellationToken = default)
        => PostAsync<LoginWithTwoFaResponse>("/api/v1/UserApi/LoginWith2Fa", request, cancellationToken);

    public Task<ApiResponse<bool>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        => PostAsync<bool>("/api/v1/UserApi/RefreshToken", request, cancellationToken);

    public Task<ApiResponse<bool>> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken = default)
        => GetAsync<bool>("/api/v1/UserApi/ConfirmEmail", new() { { "userId", userId }, { "code", code } }, cancellationToken);


    // --- Private Helper Methods ---

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
}
