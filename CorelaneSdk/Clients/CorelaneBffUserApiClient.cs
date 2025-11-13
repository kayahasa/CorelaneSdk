using CorelaneSdk.Attributes;
using CorelaneSdk.Enums.Common;
using CorelaneSdk.Enums.Core;
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
using CorelaneSdk.RefitClient;

namespace CorelaneSdk.Clients;

public interface ICorelaneBffUserApiClient
{
    [CorelaneEndpoint("/SignUpWithPhoneNumber", HttpMethodEnum.POST, ApiTypeEnum.UserApi, true)]
    Task<ApiResponse<string>> SignUpWithPhoneNumberAsync(SignUpWithPhoneNumberRequest request);

    [CorelaneEndpoint("/SignInWithSmsOtp", HttpMethodEnum.POST, ApiTypeEnum.UserApi, true)]
    Task<ApiResponse<LoginResponse>> SignInWithSmsOtpAsync(SignInWithSmsOtpRequest request);

    [CorelaneEndpoint("/GetUserProfile", HttpMethodEnum.GET, ApiTypeEnum.UserApi)]
    Task<ApiResponse<UserProfile>> GetUserProfileAsync();

    [CorelaneEndpoint("/UpdateUserProfile", HttpMethodEnum.POST, ApiTypeEnum.UserApi)]
    Task<ApiResponse<bool>> UpdateUserProfileAsync(UpdateUserProfileRequest request);

    [CorelaneEndpoint("/GetAppVersion", HttpMethodEnum.GET, ApiTypeEnum.UserApi)]
    Task<ApiResponse<AppVersion>> GetAppVersionAsync();

    [CorelaneEndpoint("/RefreshToken", HttpMethodEnum.POST, ApiTypeEnum.UserApi, true)]
    Task<ApiResponse<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);

    [CorelaneEndpoint("/DoesUserExist", HttpMethodEnum.GET, ApiTypeEnum.UserApi)]
    Task<ApiResponse<bool>> DoesUserExist(string propertyName, string value);

    [CorelaneEndpoint("/SearchUsersByUsername", HttpMethodEnum.GET, ApiTypeEnum.UserApi)]
    Task<ApiResponse<List<UserSearchResult>>> SearchUsersByUsername(string keyword);
    
    [CorelaneEndpoint("/LockUser", HttpMethodEnum.GET, ApiTypeEnum.UserApi)]
    Task<ApiResponse<bool>> LockUser(bool lockUser);
    
    [CorelaneEndpoint("/GetFaqs", HttpMethodEnum.GET, ApiTypeEnum.UserApi, true)]
    Task<ApiResponse<List<FaqItem>>> GetProjectFaqsAsync(CorelaneLanguageEnum language);
    
    [CorelaneEndpoint("/GetPolicy", HttpMethodEnum.GET, ApiTypeEnum.UserApi, true)]
    Task<ApiResponse<Policy>> GetProjectPolicyAsync(PolicyTypeEnum policyType, CorelaneLanguageEnum language);
    
    [CorelaneEndpoint("/CreateFeedback", HttpMethodEnum.POST, ApiTypeEnum.UserApi)]
    Task<ApiResponse<Feedback>> CreateFeedbackAsync(CreateFeedBackRequest request);

    Task<ApiResponse<WorkingTime>> IsWorkingTimeAsync(string planName, DateTime? date);

    Task<ApiResponse<DateTime>> GetNextWorkingDateAsync(string planName);

    Task<ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync(string? filter, string? orderBy, int? skip, int? top, string? includes);
}

public class CorelaneBffUserApiClient : ICorelaneBffUserApiClient
{
    private readonly ICorelaneBffUserApiHttpClient _corelaneUserApiHttpClient;

    public CorelaneBffUserApiClient(ICorelaneBffUserApiHttpClient corelaneUserApiHttpClient)
    {
        _corelaneUserApiHttpClient = corelaneUserApiHttpClient;
    }

    public async Task<ApiResponse<string>> SignUpWithPhoneNumberAsync(SignUpWithPhoneNumberRequest request)
    {
        return await _corelaneUserApiHttpClient.SignUpWithPhoneNumberAsync(request);
    }

    public async Task<ApiResponse<LoginResponse>> SignInWithSmsOtpAsync(SignInWithSmsOtpRequest request)
    {
        return await _corelaneUserApiHttpClient.SignInWithSmsOtpAsync(request);
    }

    public async Task<ApiResponse<UserProfile>> GetUserProfileAsync()
    {
        return await _corelaneUserApiHttpClient.GetUserProfileAsync();
    }

    public async Task<ApiResponse<bool>> UpdateUserProfileAsync(UpdateUserProfileRequest request)
    {
        return await _corelaneUserApiHttpClient.UpdateUserProfileAsync(request);
    }

    public async Task<ApiResponse<AppVersion>> GetAppVersionAsync()
    {
        return await _corelaneUserApiHttpClient.GetAppVersionAsync();
    }

    public async Task<ApiResponse<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        return await _corelaneUserApiHttpClient.RefreshTokenAsync(request);
    }

    public async Task<ApiResponse<bool>> DoesUserExist(string propertyName, string value)
    {
        return await _corelaneUserApiHttpClient.DoesUserExist(propertyName, value);
    }

    public async Task<ApiResponse<List<UserSearchResult>>> SearchUsersByUsername(string keyword)
    {
        return await _corelaneUserApiHttpClient.SearchUsersByUsername(keyword);
    }

    public async Task<ApiResponse<bool>> LockUser(bool lockUser)
    {
        return await _corelaneUserApiHttpClient.LockUser(lockUser);
    }

    public async Task<ApiResponse<List<FaqItem>>> GetProjectFaqsAsync(CorelaneLanguageEnum language)
    {
        return await _corelaneUserApiHttpClient.GetProjectFaqsAsync(language);
    }

    public async Task<ApiResponse<Policy>> GetProjectPolicyAsync(PolicyTypeEnum policyType, CorelaneLanguageEnum language)
    {
        return await _corelaneUserApiHttpClient.GetProjectPolicyAsync(policyType, language);
    }

    public async Task<ApiResponse<Feedback>> CreateFeedbackAsync(CreateFeedBackRequest request)
    {
        return await _corelaneUserApiHttpClient.CreateFeedbackAsync(request);
    }

    public async Task<ApiResponse<WorkingTime>> IsWorkingTimeAsync(string planName, DateTime? date)
    {
        return await _corelaneUserApiHttpClient.IsWorkingTimeAsync(planName, date);
    }

    public async Task<ApiResponse<DateTime>> GetNextWorkingDateAsync(string planName)
    {
        return await _corelaneUserApiHttpClient.GetNextWorkingDateAsync(planName);
    }

    public async Task<ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync(string? filter, string? orderBy, int? skip, int? top, string? includes)
    {
        return await _corelaneUserApiHttpClient.GetAllUsersAsync(filter, orderBy, skip, top, includes);
    }
}
