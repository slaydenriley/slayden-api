namespace Slayden.Api.Requests.Posts;

public class CreatePostRequest
{
    public required string Title { get; set; }

    public required string Body { get; set; }
}
