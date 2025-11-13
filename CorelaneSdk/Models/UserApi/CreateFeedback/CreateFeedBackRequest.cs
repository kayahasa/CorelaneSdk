namespace CorelaneSdk.Models.UserApi.CreateFeedback;

public class CreateFeedBackRequest
{
    public string Email { get; set; }
    public string Message { get; set; }
    public Guid? UserId { get; set; }
}
