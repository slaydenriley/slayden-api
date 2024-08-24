using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Requests.Posts;
using Slayden.Core.Services;

namespace Slayden.Api.Controllers;

[Route("posts")]
public class PostController(IPostService postService) : SlaydenControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult> GetPost([FromRoute] Guid id)
    {
        var result = await postService.GetPostById(id);
        return BadRequest("Not implemented");
    }

    [HttpGet]
    public async Task<ActionResult> GetPosts()
    {
        // var result = await postService.GetAllPosts(id)
        return BadRequest("Not implemented");
    }

    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostRequest request)
    {
        // var result = await postService.CreatePost(id)
        return BadRequest("Not implemented");
    }
}
