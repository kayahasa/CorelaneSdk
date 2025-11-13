using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CorelaneSdk.Enums.UserApi;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PolicyTypeEnum
{
    [Description("Privacy Policy")]
    PrivacyPolicy,
    [Description("Terms Of Service")]
    TermsOfService,
}
