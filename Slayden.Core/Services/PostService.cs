using ErrorOr;
using Slayden.Core.Models;
using Slayden.Core.Repositories;

namespace Slayden.Core.Services;

public interface IPostService
{
    Task<ErrorOr<Post>> GetPostById(Guid id);

    Task<ErrorOr<Post>> CreatePost(string title, string body);
}

public class PostService(IPostRepository repository) : IPostService
{
    public async Task<ErrorOr<Post>> GetPostById(Guid id)
    {
        var post = await repository.GetPostById(id);
        if (post == null)
        {
            return Error.NotFound("404", $"Post with id {id} not found");
        }

        return post;
    }

    public async Task<ErrorOr<Post>> CreatePost(string title, string body)
    {
        var postDto = await repository.CreatePost(title, body);
        if (postDto == null)
        {
            return Error.Failure(description: "Error creating a new post");
        }

        var post = Post.From(postDto);
        if (post == null)
        {
            return Error.Failure(description: "Error creating a new post");
        }

        return post;
    }
}
