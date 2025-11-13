using CorelaneSdk.Enums.Common;
using CorelaneSdk.Enums.UserApi;
using CorelaneSdk.Models.Core;
using CorelaneSdk.Models.UserApi;
using CorelaneSdk.Models.UserApi.Common;
using CorelaneSdk.Models.UserApi.CreateFeedback;
using CorelaneSdk.Models.UserApi.GetProjectAppVersion;
using CorelaneSdk.Models.UserApi.GetProjectFaqs;
using CorelaneSdk.Models.UserApi.GetProjectPolicy;
using CorelaneSdk.Models.UserApi.GetUserProfile;
using CorelaneSdk.Models.UserApi.IsWorkingTime;
using CorelaneSdk.Models.UserApi.RefreshToken;
using CorelaneSdk.Models.UserApi.SignInWithSmsOtp;
using CorelaneSdk.Models.UserApi.SignUpWithPhoneNumber;
using CorelaneSdk.Models.UserApi.UpdateUserProfile;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneBffUserApiHttpClient
{
    [Post("/api/v1/UserApi/SignUpWithPhoneNumber")]
    Task<Models.Core.ApiResponse<string>> SignUpWithPhoneNumberAsync([Body] SignUpWithPhoneNumberRequest request);

    [Post("/api/v1/UserApi/SignInWithSmsOtp")]
    Task<Models.Core.ApiResponse<LoginResponse>> SignInWithSmsOtpAsync([Body] SignInWithSmsOtpRequest request);

    [Get("/api/v1/UserApi/GetUserProfile")]
    Task<Models.Core.ApiResponse<UserProfile>> GetUserProfileAsync();

    [Post("/api/v1/UserApi/UpdateUser")]
    Task<Models.Core.ApiResponse<bool>> UpdateUserProfileAsync([Body] UpdateUserProfileRequest request);

    [Get("/api/v1/UserApi/GetProjectAppVersion")]
    Task<Models.Core.ApiResponse<AppVersion>> GetAppVersionAsync();

    [Post("/api/v1/UserApi/RefreshToken")]
    Task<Models.Core.ApiResponse<LoginResponse>> RefreshTokenAsync([Body] RefreshTokenRequest request);

    [Get("/api/v1/UserApi/DoesUserExist")]
    Task<Models.Core.ApiResponse<bool>> DoesUserExist([Query] string propertyName, [Query] string value);
    
    [Get("/api/v1/UserApi/SearchUsersByUsername")]
    Task<Models.Core.ApiResponse<List<UserSearchResult>>> SearchUsersByUsername([Query] string keyword);
    
    [Get("/api/v1/UserApi/LockUser")]
    Task<Models.Core.ApiResponse<bool>> LockUser([Query] bool lockUser);

    [Get("/api/v1/UserApi/GetProjectFaqs")]
    Task<Models.Core.ApiResponse<List<FaqItem>>> GetProjectFaqsAsync([Query] CorelaneLanguageEnum language);

    [Get("/api/v1/UserApi/GetProjectPolicy")]
    Task<Models.Core.ApiResponse<Policy>> GetProjectPolicyAsync([Query] PolicyTypeEnum policyType, [Query] CorelaneLanguageEnum language);

    [Post("/api/v1/UserApi/CreateFeedback")]
    Task<Models.Core.ApiResponse<Feedback>> CreateFeedbackAsync([Body] CreateFeedBackRequest request);

    [Get("/api/v1/UserApi/IsWorkingTime")]
    Task<Models.Core.ApiResponse<WorkingTime>> IsWorkingTimeAsync([Query] string planName, [Query] DateTime? date);

    [Get("/api/v1/UserApi/GetNextWorkingDate")]
    Task<Models.Core.ApiResponse<DateTime>> GetNextWorkingDateAsync([Query] string planName);

    [Get("/api/v1/UserApi/GetAllUsers")]
    Task<Models.Core.ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync([Query] string? filter, [Query] string? orderBy, [Query] int? skip, [Query] int? top, [Query] string? includes);
}
