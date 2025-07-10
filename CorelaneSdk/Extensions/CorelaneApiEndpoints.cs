using Corelane.Sdk;
using CorelaneSdk.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CorelaneSdk.Extensions;

public static class CorelaneApiEndpoints
{
    /// <summary>
    /// Maps Corelane SDK endpoints to the application
    /// </summary>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="configure">Optional configuration for the endpoints</param>
    /// <returns>The endpoint route builder for chaining</returns>
    public static IEndpointRouteBuilder MapCorelaneApiEndpoints(this IEndpointRouteBuilder endpoints,
        Action<CorelaneEndpointOptions>? configure = null)
    {
        var options = new CorelaneEndpointOptions();
        configure?.Invoke(options);

        var group = endpoints.MapGroup(options.RoutePrefix);

        // Apply authorization if required
        if (options.RequireAuthorization)
        {
            group.RequireAuthorization();
        }

        // Map User endpoints
        if (options.EnableUserEndpoints)
        {
            MapUserEndpoints(group, options);
        }

        // Map Notification endpoints
        if (options.EnableNotificationEndpoints)
        {
            MapNotificationEndpoints(group, options);
        }

        return endpoints;
    }

    private static void MapUserEndpoints(RouteGroupBuilder group, CorelaneEndpointOptions options)
    {
        var userGroup = group.MapGroup("/users").WithTags("Users");

        // Authentication endpoints
        userGroup.MapPost("/login", LoginAsync)
            .WithName("Login")
            .WithSummary("Login with username and password")
            .Produces<ApiResponse<LoginResponse>>();

        userGroup.MapPost("/login-2fa", LoginWith2FaAsync)
            .WithName("LoginWith2FA")
            .WithSummary("Login with two-factor authentication")
            .Produces<ApiResponse<LoginWithTwoFaResponse>>();

        userGroup.MapPost("/refresh-token", RefreshTokenAsync)
            .WithName("RefreshToken")
            .WithSummary("Refresh authentication token")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/logout-all", LogoutFromAllDevicesAsync)
            .WithName("LogoutFromAllDevices")
            .WithSummary("Logout from all devices")
            .Produces<ApiResponse<bool>>();

        // Profile endpoints
        userGroup.MapGet("/profile", GetUserProfileAsync)
            .WithName("GetUserProfile")
            .WithSummary("Get user profile information")
            .Produces<ApiResponse<UserProfileInfoDto>>();

        userGroup.MapPost("/change-password", ChangePasswordAsync)
            .WithName("ChangePassword")
            .WithSummary("Change user password")
            .Produces<ApiResponse<bool>>();

        // Email confirmation endpoints
        userGroup.MapPost("/send-confirmation-email", SendConfirmationEmailAsync)
            .WithName("SendConfirmationEmail")
            .WithSummary("Send email confirmation")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/confirm-email", ConfirmEmailAsync)
            .WithName("ConfirmEmail")
            .WithSummary("Confirm email address")
            .Produces<ApiResponse<bool>>();

        // Password reset endpoints
        userGroup.MapPost("/forgot-password", ForgotPasswordAsync)
            .WithName("ForgotPassword")
            .WithSummary("Send forgot password email")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/reset-password", ResetPasswordAsync)
            .WithName("ResetPassword")
            .WithSummary("Reset user password")
            .Produces<ApiResponse<bool>>();

        // Two-factor authentication endpoints
        userGroup.MapGet("/2fa/status", GetTwoFactorStatusAsync)
            .WithName("GetTwoFactorStatus")
            .WithSummary("Get two-factor authentication status")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/2fa/setup", GetAuthenticatorSetupAsync)
            .WithName("GetAuthenticatorSetup")
            .WithSummary("Get authenticator setup information")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/2fa/enable", EnableTwoFactorAsync)
            .WithName("EnableTwoFactor")
            .WithSummary("Enable two-factor authentication")
            .Produces<ApiResponse<List<string>>>();

        userGroup.MapPost("/2fa/disable", DisableTwoFactorAsync)
            .WithName("DisableTwoFactor")
            .WithSummary("Disable two-factor authentication")
            .Produces<ApiResponse<bool>>();

        userGroup.MapPost("/2fa/toggle", ToggleTwoFactorAsync)
            .WithName("ToggleTwoFactor")
            .WithSummary("Toggle two-factor authentication")
            .Produces<ApiResponse<bool>>();
    }

    private static void MapNotificationEndpoints(RouteGroupBuilder group, CorelaneEndpointOptions options)
    {
        var notificationGroup = group.MapGroup("/notifications").WithTags("Notifications");

        notificationGroup.MapPost("/email", SendEmailAsync)
            .WithName("SendEmail")
            .WithSummary("Send email notification")
            .Produces<ApiResponse<bool>>();

        notificationGroup.MapPost("/telegram", SendTelegramMessageAsync)
            .WithName("SendTelegramMessage")
            .WithSummary("Send Telegram message")
            .Produces<ApiResponse<bool>>();
    }

    #region User Endpoint Handlers

    private static async Task<IResult> LoginAsync(LoginWithPasswordRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.LoginAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> LoginWith2FaAsync(LoginWithTwoFaRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.LoginWith2FaAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> RefreshTokenAsync(RefreshTokenRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.RefreshTokenAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> LogoutFromAllDevicesAsync(ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.LogoutFromAllDevicesAsync();
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> GetUserProfileAsync(ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.GetUserProfileAsync();
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> ChangePasswordAsync(ChangePasswordRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.ChangePasswordAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> SendConfirmationEmailAsync([FromQuery] string email, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.SendConfirmationEmailAsync(email);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.ConfirmEmailAsync(userId, code);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> ForgotPasswordAsync([FromQuery] string email, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.ForgotPasswordAsync(email);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.ResetPasswordAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> GetTwoFactorStatusAsync(ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.GetTwoFactorStatusAsync();
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> GetAuthenticatorSetupAsync([FromQuery] string appName, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.GetAuthenticatorSetupAsync(appName);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> EnableTwoFactorAsync([FromQuery] string totp, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.EnableTwoFactorAsync(totp);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> DisableTwoFactorAsync(ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.DisableTwoFactorAsync();
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> ToggleTwoFactorAsync(ToggleTotpRequest request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.UserApi.ToggleTwoFactorAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    #endregion

    #region Notification Endpoint Handlers

    private static async Task<IResult> SendEmailAsync(EmailMessageEvent request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.NotificationApi.SendEmailAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }

    private static async Task<IResult> SendTelegramMessageAsync(TelegramMessageEvent request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            var result = await corelaneApiClient.NotificationApi.SendTelegramMessageAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(ex.Message, statusCode: (int)ex.StatusCode);
        }
    }
    #endregion
}

/// <summary>
/// Configuration options for Corelane API endpoints
/// </summary>
public class CorelaneEndpointOptions
{
    /// <summary>
    /// The route prefix for all endpoints. Default is "/api/corelane"
    /// </summary>
    public string RoutePrefix { get; set; } = "/api/corelane";

    /// <summary>
    /// Whether to enable user-related endpoints. Default is true
    /// </summary>
    public bool EnableUserEndpoints { get; set; } = true;

    /// <summary>
    /// Whether to enable notification-related endpoints. Default is true
    /// </summary>
    public bool EnableNotificationEndpoints { get; set; } = true;

    /// <summary>
    /// Whether to require authorization for all endpoints. Default is false
    /// </summary>
    public bool RequireAuthorization { get; set; } = false;

    /// <summary>
    /// Custom endpoint tags for OpenAPI documentation
    /// </summary>
    public string[] Tags { get; set; } = Array.Empty<string>();
}