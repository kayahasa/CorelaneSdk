namespace CorelaneSdk.Models.UserApi.LoginWithPassword;

public class LoginWithPasswordRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
