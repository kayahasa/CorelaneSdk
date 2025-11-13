using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace CorelaneSdk.Handlers;

public class CorelaneHandler : DelegatingHandler
{
    private readonly string _apiKey;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorelaneHandler(string apiKey, IHttpContextAccessor httpContextAccessor)
    {
        _apiKey = apiKey;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_apiKey))
        {
            request.Headers.Add("ApiKey", _apiKey);
        }

        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(authHeader))
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
