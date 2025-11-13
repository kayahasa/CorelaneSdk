namespace CorelaneSdk.Models.UserApi.Common;

public class LoginResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? JwtExpireDate { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public string? Avatar { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsFirstLogin { get; set; }
}
