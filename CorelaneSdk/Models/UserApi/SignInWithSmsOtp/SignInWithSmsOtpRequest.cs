namespace CorelaneSdk.Models.UserApi.SignInWithSmsOtp;

public class SignInWithSmsOtpRequest
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
    public string VerificationId { get; set; }
}
