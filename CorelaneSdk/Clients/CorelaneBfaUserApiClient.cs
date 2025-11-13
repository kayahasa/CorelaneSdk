using CorelaneSdk.Attributes;
using CorelaneSdk.Enums.Core;
using CorelaneSdk.Models;
using CorelaneSdk.RefitClient;

namespace CorelaneSdk.Clients;

public interface ICorelaneBfaUserApiClient
{
    [CorelaneEndpoint("/Login", HttpMethodEnum.POST, ApiTypeEnum.UserApiForAdmin, true)]
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginWithPasswordRequest request);

    [CorelaneEndpoint("/GetAllUsers", HttpMethodEnum.GET, ApiTypeEnum.UserApiForAdmin)]
    Task<ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync(string? filter, string? orderBy, int? skip, int? top, string? includes);

    [CorelaneEndpoint("/DeleteUser", HttpMethodEnum.GET, ApiTypeEnum.UserApiForAdmin)]
    Task<ApiResponse<bool>> DeleteUserAsync(Guid userId);

    [CorelaneEndpoint("/CreateUser", HttpMethodEnum.POST, ApiTypeEnum.UserApiForAdmin)]
    Task<ApiResponse<bool>> CreateUserAsync(LoginWithPasswordRequest request);
}

public class CorelaneBfaUserApiClient : ICorelaneBfaUserApiClient
{
    private readonly ICorelaneBfaUserApiHttpClient _corelaneBfaUserApiHttpClient;

    public CorelaneBfaUserApiClient(ICorelaneBfaUserApiHttpClient corelaneBfaUserApiHttpClient)
    {
        _corelaneBfaUserApiHttpClient = corelaneBfaUserApiHttpClient;
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginWithPasswordRequest request)
    {
        return await _corelaneBfaUserApiHttpClient.LoginAsync(request);
    }

    public async Task<ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync(string? filter, string? orderBy, int? skip, int? top, string? includes)
    {
        return await _corelaneBfaUserApiHttpClient.GetAllUsersAsync(filter, orderBy, skip, top, includes);
    }

    public async Task<ApiResponse<bool>> DeleteUserAsync(Guid userId)
    {
        return await _corelaneBfaUserApiHttpClient.DeleteUserAsync(userId);
    }

    public async Task<ApiResponse<bool>> CreateUserAsync(LoginWithPasswordRequest request)
    {
        return await _corelaneBfaUserApiHttpClient.CreateUserAsync(request);
    }
}
