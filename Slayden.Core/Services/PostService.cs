using ErrorOr;
using Slayden.Core.Models;
using Slayden.Core.Repositories;

namespace Slayden.Core.Services;

public interface IPostService
{
    Task<ErrorOr<Post>> GetPostById(Guid id);
}

public class PostService(IPostRepository repository) : IPostService
{
    public async Task<ErrorOr<Post>> GetPostById(Guid id)
    {
        var post = await repository.GetPostById(id);
        if (post == null)
        {
            return Error.NotFound(description: $"Post with id {id} not found");
        }

        return post;
    }
}
