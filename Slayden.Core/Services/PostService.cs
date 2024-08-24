using ErrorOr;
using Slayden.Core.Models;

namespace Slayden.Core.Services;

public interface IPostService
{
    Task<ErrorOr<Post>> GetPostById(Guid id);
}

public class PostService : IPostService
{
    public async Task<ErrorOr<Post>> GetPostById(Guid id)
    {
        return Error.NotFound(description: "Not found");
    }
}
