using Slayden.Core.Dtos;

namespace Slayden.Core.Models;

public class Post
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public static Post From(PostDto postDto)
    {
        return new Post
        {
            Id = new Guid(postDto.id),
            Title = postDto.title,
            Body = postDto.body,
            CreatedAt = DateTime.Parse(postDto.createdAt),
            UpdatedAt = DateTime.Parse(postDto.updatedAt),
            DeletedAt = DateTime.TryParse(postDto.deletedAt, out var deletedAt)
                ? deletedAt
                : null
        };
    }
}
