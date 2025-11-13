using CorelaneSdk.Enums.Core;

namespace CorelaneSdk.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class CorelaneEndpointAttribute : Attribute
{
    public string Route { get; }
    public HttpMethodEnum HttpMethod { get; }
    public string? DisplayName { get; }
    public string? Summary { get; }
    public int SuccessStatusCode { get; }
    public ApiTypeEnum ApiType { get; set; }
    public bool AllowAnonymous { get; }

    public CorelaneEndpointAttribute(string route, HttpMethodEnum httpMethod, ApiTypeEnum apiType, bool allowAnonymous = false, string displayName = "", string summary = "", int successStatusCode = 200)
    {
        Route = route;
        HttpMethod = httpMethod;
        DisplayName = displayName;
        Summary = summary;
        SuccessStatusCode = successStatusCode;
        ApiType = apiType;
        AllowAnonymous = allowAnonymous;
    }
}
