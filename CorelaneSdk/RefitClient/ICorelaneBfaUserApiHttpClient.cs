using CorelaneSdk.Models.Core;
using CorelaneSdk.Models.UserApi.Common;
using CorelaneSdk.Models.UserApi.GetUserProfile;
using CorelaneSdk.Models.UserApi.LoginWithPassword;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneBfaUserApiHttpClient
{
    [Post("/api/v1/UserApi/Login")]
    Task<Models.Core.ApiResponse<LoginResponse>> LoginAsync([Body] LoginWithPasswordRequest request);

    [Get("/api/v1/UserApi/GetAllUsers")]
    Task<Models.Core.ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync([Query] string? filter, [Query] string? orderBy, [Query] int? skip, [Query] int? top, [Query] string? includes);

    [Get("/api/v1/UserApi/DeleteUser")]
    Task<Models.Core.ApiResponse<bool>> DeleteUserAsync([Query] Guid userId);

    [Post("/api/v1/UserApi/CreateUser")]
    Task<Models.Core.ApiResponse<bool>> CreateUserAsync([Body] LoginWithPasswordRequest request);
}
