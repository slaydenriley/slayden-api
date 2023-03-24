using Slayden.Api.Models;

namespace Slayden.Api.Responses;

public class GetUserResponse
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<GetPostResponse>? Posts { get; set; }
}
