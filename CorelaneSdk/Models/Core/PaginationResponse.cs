namespace CorelaneSdk.Models.Core;

public class PaginationResponse<T>
{
    public int TotalCount { get; set; }
    public T Items { get; set; }
}
