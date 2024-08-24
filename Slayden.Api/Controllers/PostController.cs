using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Requests.Posts;

namespace Slayden.Api.Controllers;

[Controller]
[Route("posts")]
public class PostController
{
    [HttpGet]
    public async Task GetPost(Guid id)
    {
        // var result = await postService.GetPostById(id)
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task GetPosts()
    {
        // var result = await postService.GetAllPosts(id)
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task CreatePost(CreatePostRequest request)
    {
        // var result = await postService.CreatePost(id)
        throw new NotImplementedException();
    }
}
