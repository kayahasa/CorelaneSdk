using CorelaneSdk.Models;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneBfaUserApiHttpClient
{
    [Post("/api/v1/UserApi/Login")]
    Task<Models.ApiResponse<LoginResponse>> LoginAsync([Body] LoginWithPasswordRequest request);

    [Get("/api/v1/UserApi/GetAllUsers")]
    Task<Models.ApiResponse<PaginationResponse<List<UserProfile>>>> GetAllUsersAsync([Query] string? filter, [Query] string? orderBy, [Query] int? skip, [Query] int? top, [Query] string? includes);

    [Get("/api/v1/UserApi/DeleteUser")]
    Task<Models.ApiResponse<bool>> DeleteUserAsync([Query] Guid userId);

    [Post("/api/v1/UserApi/CreateUser")]
    Task<Models.ApiResponse<bool>> CreateUserAsync([Body] LoginWithPasswordRequest request);
}
