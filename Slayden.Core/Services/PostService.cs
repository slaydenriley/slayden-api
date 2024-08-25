using ErrorOr;
using Slayden.Core.Models;
using Slayden.Core.Repositories;

namespace Slayden.Core.Services;

public interface IPostService
{
    Task<ErrorOr<Post>> GetPostById(Guid id);

    Task<ErrorOr<List<Post>>> GetAllPosts();

    Task<ErrorOr<Post>> CreatePost(string title, string body);

    Task<ErrorOr<Post>> UpdatePost(Guid id, string? title, string? body);
}

public class PostService(IPostRepository repository) : IPostService
{
    public async Task<ErrorOr<Post>> GetPostById(Guid id)
    {
        if (id == new Guid())
        {
            return Error.Validation("Validation Error", "Post ID must not be a zero guid");
        }

        var postDto = await repository.GetPostById(id);
        if (postDto == null)
        {
            return Error.NotFound("Not Found", $"Post with id {id} not found");
        }

        return Post.From(postDto);
    }

    public async Task<ErrorOr<List<Post>>> GetAllPosts()
    {
        var postDtoList = await repository.GetAllPosts();

        var posts = new List<Post>();
        posts.AddRange(postDtoList.Select(Post.From));

        return posts;
    }

    public async Task<ErrorOr<Post>> CreatePost(string title, string body)
    {
        var errors = new List<Error>();
        if (title.Length > 50)
        {
            errors.Add(
                Error.Validation(
                    "Validation Error",
                    "Title must be less than or equal to 50 characters."
                )
            );
        }

        if (body.Length > 500)
        {
            errors.Add(
                Error.Validation(
                    "Validation Error",
                    "Body must be less than or equal to 500 characters."
                )
            );
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var postDto = await repository.CreatePost(title, body);
        if (postDto == null)
        {
            return Error.Failure("Internal Error", "Error creating a new post");
        }

        return Post.From(postDto);
    }

    public async Task<ErrorOr<Post>> UpdatePost(Guid id, string? title, string? body)
    {
        var errors = new List<Error>();
        if (title == null && body == null)
        {
            errors.Add(
                Error.Validation("Validation Error", "Must include at least one field to update.")
            );
        }

        if (title is { Length: > 50 })
        {
            errors.Add(
                Error.Validation(
                    "Validation Error",
                    "Title must be less than or equal to 50 characters."
                )
            );
        }

        if (body is { Length: > 500 })
        {
            errors.Add(
                Error.Validation(
                    "Validation Error",
                    "Body must be less than or equal to 500 characters."
                )
            );
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var postDto = await repository.UpdatePost(id, title, body);
        if (postDto == null)
        {
            return Error.Failure("Internal Error", "Error updating existing post");
        }

        return Post.From(postDto);
    }
}
