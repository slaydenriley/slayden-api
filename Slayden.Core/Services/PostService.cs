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
        List<Error> errors = [];
        if (title.Length > 50)
        {
            errors.Add(Error.Validation(description: "Title must be less than 50 characters."));
        }

        if (body.Length > 500)
        {
            errors.Add(Error.Validation(description: "Body must be less than 500 characters."));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var postDto = await repository.CreatePost(title, body);
        if (postDto == null)
        {
            return Error.Failure(description: "Error creating a new post");
        }

        return Post.From(postDto);
    }
}
