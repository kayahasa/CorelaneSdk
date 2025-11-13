using CorelaneSdk.Attributes;
using CorelaneSdk.Clients;
using CorelaneSdk.Enums.Core;
using CorelaneSdk.Models.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

namespace CorelaneSdk.Extensions;

public static class CorelaneEndpointExtensions
{
    public static void MapCorelaneEndpoints(this WebApplication app, ApiTypeEnum apiType, string routePrefix = "", string tagName = "")
    {
        tagName = !string.IsNullOrWhiteSpace(tagName) ? tagName : apiType.ToString();
        var group = app.MapGroup(routePrefix)
            .WithTags(tagName)
            .WithOpenApi();

        // ApiType'a göre client type'ı belirle
        var clientType = GetClientTypeByApiType(apiType);
        if (clientType == null)
        {
            throw new InvalidOperationException($"No client type found for API type: {apiType}");
        }

        var methods = clientType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        foreach (var method in methods)
        {
            var endpointAttr = method.GetCustomAttribute<CorelaneEndpointAttribute>();
            if (endpointAttr == null) continue;

            var parameters = method.GetParameters();
            var parameterType = parameters.FirstOrDefault()?.ParameterType;

            if (endpointAttr.ApiType != apiType) continue;

            // ApiResponse<T> tipinden T'yi çıkar
            var returnType = method.ReturnType;
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var taskResultType = returnType.GetGenericArguments()[0];
                if (taskResultType.IsGenericType && taskResultType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    var responseDataType = taskResultType.GetGenericArguments()[0];
                    MapEndpointForType(group, endpointAttr, method.Name, parameters, responseDataType, tagName, clientType);
                }
            }
        }
    }

    private static Type? GetClientTypeByApiType(ApiTypeEnum apiType)
    {
        return apiType switch
        {
            ApiTypeEnum.UserApi => typeof(ICorelaneBffUserApiClient),
            ApiTypeEnum.NotificationApi => typeof(ICorelaneNotificationApiClient),
            ApiTypeEnum.MobilePaymentApi => typeof(ICorelaneMobilePaymentApiClient),
            ApiTypeEnum.UserApiForAdmin => typeof(ICorelaneBfaUserApiClient),
            // Diğer API türleri için client'ları buraya ekleyin
            _ => null
        };
    }

    private static void MapEndpointForType(RouteGroupBuilder group, CorelaneEndpointAttribute attr, string methodName, ParameterInfo[] parameters, Type responseType, string tagName = "", Type clientType = null)
    {
        RouteHandlerBuilder endpoint;

        endpoint = attr.HttpMethod switch
        {
            HttpMethodEnum.GET => group.MapGet(attr.Route, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                // GET parametrelerini işle
                for (int i = 0; i < parameters.Length; i++)
                {
                    var param = parameters[i];
                    var paramType = param.ParameterType;

                    if (IsPrimitiveOrValueType(paramType))
                    {
                        // Primitive types için query string'den al
                        var queryParams = context.Request.Query;
                        if (queryParams.TryGetValue(param.Name, out var value))
                        {
                            methodArgs[i] = ConvertValue(value.ToString(), paramType);
                        }
                        else
                        {
                            methodArgs[i] = GetDefaultValue(paramType);
                        }
                    }
                    else
                    {
                        // Complex types için query parametrelerinden object oluştur
                        var queryParams = context.Request.Query;
                        var request = Activator.CreateInstance(paramType);

                        foreach (var prop in paramType.GetProperties())
                        {
                            if (queryParams.TryGetValue(prop.Name, out var value))
                            {
                                var convertedValue = ConvertValue(value.ToString(), prop.PropertyType);
                                prop.SetValue(request, convertedValue);
                            }
                        }
                        methodArgs[i] = request;
                    }
                }

                return await InvokeClientMethod(client, methodName, methodArgs, clientType);
            }),

            HttpMethodEnum.POST => group.MapPost(attr.Route, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;
                    methodArgs[i] = await context.Request.ReadFromJsonAsync(paramType);
                }

                return await InvokeClientMethod(client, methodName, methodArgs, clientType);
            }),

            HttpMethodEnum.PUT => group.MapPut(attr.Route, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;
                    methodArgs[i] = await context.Request.ReadFromJsonAsync(paramType);
                }

                return await InvokeClientMethod(client, methodName, methodArgs, clientType);
            }),

            HttpMethodEnum.PATCH => group.MapPatch(attr.Route, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;
                    methodArgs[i] = await context.Request.ReadFromJsonAsync(paramType);
                }

                return await InvokeClientMethod(client, methodName, methodArgs, clientType);
            }),

            HttpMethodEnum.DELETE => group.MapDelete(attr.Route, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;
                    methodArgs[i] = await context.Request.ReadFromJsonAsync(paramType);
                }

                return await InvokeClientMethod(client, methodName, methodArgs, clientType);
            }),

            HttpMethodEnum.HEAD => group.MapMethods(attr.Route, new[] { "HEAD" }, async (HttpContext context, IServiceProvider serviceProvider) =>
            {
                var client = serviceProvider.GetRequiredService(clientType);
                object?[] methodArgs = new object?[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    methodArgs[i] = GetDefaultValue(parameters[i].ParameterType);
                }

                var result = await InvokeClientMethod(client, methodName, methodArgs, clientType);
                // HEAD response body olmayacağı için sadece status dönelim
                return Results.StatusCode(((IResult)result).ExecuteAsync(context).IsCompletedSuccessfully ? 200 : 400);
            }),

            HttpMethodEnum.OPTIONS => group.MapMethods(attr.Route, new[] { "OPTIONS" }, (HttpContext context) =>
            {
                context.Response.Headers.Append("Allow", "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS");
                return Results.Ok();
            }),

            _ => throw new NotSupportedException($"HTTP method {attr.HttpMethod} is not supported.")
        };

        // OpenAPI metadata ekle
        endpoint
            .WithName(methodName.Replace("Async", ""))
            .WithDisplayName(attr.DisplayName)
            .WithSummary(attr.Summary)
            .WithTags(tagName)
            .WithOpenApi(operation =>
            {
                operation.Summary = attr.Summary;
                operation.Description = attr.DisplayName;

                // GET metodları için query parametrelerini OpenAPI'ye ekle
                if (attr.HttpMethod == HttpMethodEnum.GET)
                {
                    foreach (var param in parameters)
                    {
                        if (IsPrimitiveOrValueType(param.ParameterType))
                        {
                            var parameter = new Microsoft.OpenApi.Models.OpenApiParameter
                            {
                                Name = param.Name,
                                In = Microsoft.OpenApi.Models.ParameterLocation.Query,
                                Required = !param.HasDefaultValue && !IsNullableType(param.ParameterType),
                                Schema = new Microsoft.OpenApi.Models.OpenApiSchema
                                {
                                    Type = GetOpenApiType(param.ParameterType)
                                }
                            };
                            operation.Parameters ??= new List<Microsoft.OpenApi.Models.OpenApiParameter>();
                            operation.Parameters.Add(parameter);
                        }
                    }
                }

                return operation;
            })
            .Produces(attr.SuccessStatusCode, responseType)
            .Produces(400)
            .Produces(500);

        // GET istekleri için request body ekleme, diğerleri için ekle
        if (attr.HttpMethod != HttpMethodEnum.GET && attr.HttpMethod != HttpMethodEnum.HEAD && attr.HttpMethod != HttpMethodEnum.OPTIONS)
        {
            foreach (var param in parameters)
            {
                endpoint.Accepts(param.ParameterType, "application/json");
            }
        }

        // Authorization ekleme
        if (attr.AllowAnonymous)
            endpoint.AllowAnonymous();
        else
            endpoint.RequireAuthorization();
    }

    private static async Task<IResult> InvokeClientMethod(object client, string methodName, object?[] parameters, Type clientType)
    {
        try
        {
            var method = clientType.GetMethod(methodName);
            if (method == null)
                return Results.InternalServerError(CreateErrorResponse("Method not found", 500));

            var task = (Task)method.Invoke(client, parameters)!;
            await task;

            var resultProperty = task.GetType().GetProperty("Result");
            dynamic result = resultProperty!.GetValue(task)!;

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        }
        catch (Refit.ApiException apiEx)
        {
            return Results.BadRequest(JsonSerializer.Deserialize<object>(apiEx.Content ?? ""));
        }
        catch (Exception ex)
        {
            return Results.InternalServerError(CreateErrorResponse($"An unexpected error occurred: {ex.Message}", 500));
        }
    }

    private static bool IsPrimitiveOrValueType(Type type)
    {
        // Nullable types için underlying type'ı kontrol et
        if (IsNullableType(type))
        {
            type = Nullable.GetUnderlyingType(type);
        }

        return type.IsPrimitive ||
               type.IsValueType ||
               type == typeof(string) ||
               type == typeof(Guid) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type.IsEnum;
    }

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    private static object? GetDefaultValue(Type type)
    {
        if (IsNullableType(type))
            return null;

        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    private static string GetOpenApiType(Type type)
    {
        if (IsNullableType(type))
        {
            type = Nullable.GetUnderlyingType(type);
        }

        return type.Name.ToLower() switch
        {
            "string" => "string",
            "int32" => "integer",
            "int64" => "integer",
            "boolean" => "boolean",
            "datetime" => "string",
            "guid" => "string",
            "double" => "number",
            "decimal" => "number",
            "float" => "number",
            _ when type.IsEnum => "string",
            _ => "string"
        };
    }

    private static object ConvertValue(string value, Type targetType)
    {
        try
        {
            // Nullable types için özel işlem
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(value))
                    return null;

                targetType = Nullable.GetUnderlyingType(targetType);
            }

            // Enum kontrolü
            if (targetType.IsEnum)
            {
                return Enum.Parse(targetType, value, true);
            }

            // GUID kontrolü
            if (targetType == typeof(Guid))
            {
                return Guid.Parse(value);
            }

            // Boolean kontrolü
            if (targetType == typeof(bool))
            {
                return bool.Parse(value);
            }

            // DateTime kontrolü
            if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }

            return Convert.ChangeType(value, targetType);
        }
        catch
        {
            // Conversion başarısız olursa default value dön
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }
    }

    private static object CreateErrorResponse(string message, int statusCode)
    {
        var errors = new List<string> { message };
        return ApiResponse<object>.Instance<object>(false, message, null, errors,
            statusCode == 400 ? "BadRequest" : "InternalServerError", statusCode);
    }
}