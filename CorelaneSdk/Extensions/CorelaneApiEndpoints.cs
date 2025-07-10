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

        // Only map notification endpoints since user endpoints are disabled
        if (options.EnableNotificationEndpoints)
        {
            MapNotificationEndpoints(group, options);
        }

        return endpoints;
    }

    private static void MapNotificationEndpoints(RouteGroupBuilder group, CorelaneEndpointOptions options)
    {
        var notificationGroup = group.MapGroup("/notifications").WithTags("Notifications");

        notificationGroup.MapPost("/email", SendEmailAsync)
            .WithName("SendEmail")
            .WithSummary("Send email notification")
            .WithDescription("Sends an email notification using the configured email service")
            .Accepts<EmailMessageEvent>("application/json")
            .Produces<ApiResponse<bool>>(200)
            .Produces<ProblemDetails>(400)
            .Produces<ProblemDetails>(500);

        notificationGroup.MapPost("/telegram", SendTelegramMessageAsync)
            .WithName("SendTelegramMessage")
            .WithSummary("Send Telegram message")
            .WithDescription("Sends a message via Telegram bot")
            .Accepts<TelegramMessageEvent>("application/json")
            .Produces<ApiResponse<bool>>(200)
            .Produces<ProblemDetails>(400)
            .Produces<ProblemDetails>(500);
    }

    #region Notification Endpoint Handlers

    private static async Task<IResult> SendEmailAsync(EmailMessageEvent request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            // Validate the request
            if (string.IsNullOrWhiteSpace(request.To))
            {
                return Results.BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Email recipient is required",
                    StatusCode = 400
                });
            }

            if (string.IsNullOrWhiteSpace(request.Subject) && string.IsNullOrWhiteSpace(request.Body))
            {
                return Results.BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Email subject or body is required",
                    StatusCode = 400
                });
            }

            var result = await corelaneApiClient.NotificationApi.SendEmailAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(
                detail: ex.Message,
                statusCode: (int)ex.StatusCode,
                title: "API Error",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            );
        }
        catch (Exception ex)
        {
            return Results.Problem(
                detail: "An unexpected error occurred while sending the email",
                statusCode: 500,
                title: "Internal Server Error",
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            );
        }
    }

    private static async Task<IResult> SendTelegramMessageAsync(TelegramMessageEvent request, ICorelaneApiClient corelaneApiClient)
    {
        try
        {
            // Validate the request
            if (string.IsNullOrWhiteSpace(request.To))
            {
                return Results.BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Telegram recipient is required",
                    StatusCode = 400
                });
            }

            if (string.IsNullOrWhiteSpace(request.Body))
            {
                return Results.BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Message body is required",
                    StatusCode = 400
                });
            }

            var result = await corelaneApiClient.NotificationApi.SendTelegramMessageAsync(request);
            return Results.Ok(result);
        }
        catch (ApiException ex)
        {
            return Results.Problem(
                detail: ex.Message,
                statusCode: (int)ex.StatusCode,
                title: "API Error",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            );
        }
        catch (Exception ex)
        {
            return Results.Problem(
                detail: "An unexpected error occurred while sending the Telegram message",
                statusCode: 500,
                title: "Internal Server Error",
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            );
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