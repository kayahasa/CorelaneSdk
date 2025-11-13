using CorelaneSdk.Enums.Common;

namespace CorelaneSdk.Models.UserApi.GetProjectFaqs;

public class FaqItem
{
    public CorelaneLanguageEnum Language { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}
