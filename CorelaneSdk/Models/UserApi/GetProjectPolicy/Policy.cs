using CorelaneSdk.Enums.Common;
using CorelaneSdk.Enums.UserApi;

namespace CorelaneSdk.Models.UserApi.GetProjectPolicy;

public class Policy
{
    public CorelaneLanguageEnum Language { get; set; }
    public PolicyTypeEnum PolicyType { get; set; }
    public string Content { get; set; }
}
