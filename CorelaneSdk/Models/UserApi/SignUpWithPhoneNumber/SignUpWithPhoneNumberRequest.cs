using CorelaneSdk.Enums.Common;

namespace CorelaneSdk.Models.UserApi.SignUpWithPhoneNumber;

public class SignUpWithPhoneNumberRequest
{
    public string PhoneNumber { get; set; }
    public string? ReferencedBy { get; set; }
    public bool DocumentsAccepted { get; set; }
    public string? FcmToken { get; set; }
    public string? Platform { get; set; }
    public CorelaneLanguageEnum Language { get; set; }
}
