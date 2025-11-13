namespace CorelaneSdk.Models.UserApi
{
    public class UserSearchResult
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}
