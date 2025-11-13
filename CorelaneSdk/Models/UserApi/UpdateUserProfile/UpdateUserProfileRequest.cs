using CorelaneSdk.Enums.Common;

namespace CorelaneSdk.Models.UserApi.UpdateUserProfile;

public class UpdateUserProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Avatar { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Instagram { get; set; }
    public string? City { get; set; }
    public CorelaneLanguageEnum? Language { get; set; }
    public DateTime? Birthday { get; set; }
    public GenderEnum? Gender { get; set; }
}
