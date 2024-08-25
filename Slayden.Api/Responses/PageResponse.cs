namespace Slayden.Api.Responses;

public class PageResponse<T>
{
    public int TotalItems { get; set; }

    public IEnumerable<T> Items { get; set; } = [];
}
