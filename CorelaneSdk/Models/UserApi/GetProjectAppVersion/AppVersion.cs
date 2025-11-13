namespace CorelaneSdk.Models.UserApi.GetProjectAppVersion;

public class AppVersion
{
    public string MinVersion { get; set; }
    public string LatestVersion { get; set; }
    public bool ForceUpdate { get; set; }
}
