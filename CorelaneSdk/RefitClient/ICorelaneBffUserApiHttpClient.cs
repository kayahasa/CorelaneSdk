using CorelaneSdk.Enums.Common;
using CorelaneSdk.Enums.UserApi;
using CorelaneSdk.Models;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneBffUserApiHttpClient
{
    [Post("/api/v1/UserApi/SignUpWithPhoneNumber")]
    Task<Models.ApiResponse<string>> SignUpWithPhoneNumberAsync([Body] SignUpWithPhoneNumberRequest request);

    [Post("/api/v1/UserApi/SignInWithSmsOtp")]
    Task<Models.ApiResponse<LoginResponse>> SignInWithSmsOtpAsync([Body] SignInWithSmsOtpRequest request);

    [Get("/api/v1/UserApi/GetUserProfile")]
    Task<Models.ApiResponse<UserProfile>> GetUserProfileAsync();

    [Post("/api/v1/UserApi/UpdateUser")]
    Task<Models.ApiResponse<bool>> UpdateUserProfileAsync([Body] UpdateUserProfileRequest request);

    [Get("/api/v1/UserApi/GetProjectAppVersion")]
    Task<Models.ApiResponse<AppVersion>> GetAppVersionAsync();

    [Post("/api/v1/UserApi/RefreshToken")]
    Task<Models.ApiResponse<LoginResponse>> RefreshTokenAsync([Body] RefreshTokenRequest request);

    [Get("/api/v1/UserApi/DoesUserExist")]
    Task<Models.ApiResponse<bool>> DoesUserExist([Query] string propertyName, [Query] string value);
    
    [Get("/api/v1/UserApi/SearchUsersByUsername")]
    Task<Models.ApiResponse<List<UserSearchResult>>> SearchUsersByUsername([Query] string keyword);
    
    [Get("/api/v1/UserApi/LockUser")]
    Task<Models.ApiResponse<bool>> LockUser([Query] bool lockUser);

    [Get("/api/v1/UserApi/GetProjectFaqs")]
    Task<Models.ApiResponse<List<FaqItem>>> GetProjectFaqsAsync([Query] CorelaneLanguageEnum language);

    [Get("/api/v1/UserApi/GetProjectPolicy")]
    Task<Models.ApiResponse<Policy>> GetProjectPolicyAsync([Query] PolicyTypeEnum policyType, [Query] CorelaneLanguageEnum language);

    [Post("/api/v1/UserApi/CreateFeedback")]
    Task<Models.ApiResponse<Feedback>> CreateFeedbackAsync([Body] CreateFeedBackRequest request);

    [Get("/api/v1/UserApi/IsWorkingTime")]
    Task<Models.ApiResponse<WorkingTime>> IsWorkingTimeAsync([Query] string planName, [Query] DateTime? date);

    [Get("/api/v1/UserApi/GetNextWorkingDate")]
    Task<Models.ApiResponse<DateTime>> GetNextWorkingDateAsync([Query] string planName);

    [Get("/api/v1/UserApi/GetAllUsers")]
    Task<Models.ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync([Query] string? filter, [Query] string? orderBy, [Query] int? skip, [Query] int? top, [Query] string? includes);
}
