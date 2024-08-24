namespace Slayden.Api.Responses;

public class GetCommentResponse
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string CommentorEmail { get; set; }
    public string CommentorName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
