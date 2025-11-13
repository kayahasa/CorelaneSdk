namespace CorelaneSdk.Models.UserApi.CreateFeedback;

public class Feedback
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public bool IsResolved { get; set; }
    public DateTime CreatedAt { get; set; }
}
