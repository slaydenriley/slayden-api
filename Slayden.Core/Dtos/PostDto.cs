namespace Slayden.Core.Dtos;

public class PostDto
{
    public string id { get; set; }

    public string userId { get; set; }

    public string title { get; set; }

    public string body { get; set; }

    public string createdAt { get; set; }

    public string updatedAt { get; set; }

    public string? deletedAt { get; set; }
}
