using System.Net;

namespace CorelaneSdk.Models
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? ErrorCode { get; }
        public List<string>? Errors { get; }

        public ApiException(object? errorResponse, HttpStatusCode statusCode)
            : base(GetErrorMessage(errorResponse))
        {
            StatusCode = statusCode;
            if (errorResponse is ApiResponse<object> apiResponse)
            {
                ErrorCode = apiResponse.ErrorCode;
                Errors = apiResponse.Errors;
            }
        }

        private static string GetErrorMessage(object? errorResponse)
        {
            if (errorResponse is ApiResponse<object> apiResponse)
            {
                return apiResponse.Message ?? "An unknown API error occurred.";
            }
            return "An unknown API error occurred.";
        }
    }
}
