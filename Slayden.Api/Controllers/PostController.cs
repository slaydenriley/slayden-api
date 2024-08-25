using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Requests.Posts;
using Slayden.Api.Responses;
using Slayden.Core.Models;
using Slayden.Core.Services;

namespace Slayden.Api.Controllers;

[Route("posts")]
public class PostController(IPostService postService) : SlaydenControllerBase
{
    /// <summary>
    /// Retrieves a specific post by its unique id
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid or missing id for post</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetPost([FromRoute] Guid id)
    {
        var result = await postService.GetPostById(id);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Retrieves all posts
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation failure</response>
    [HttpGet]
    public async Task<ActionResult> GetPosts()
    {
        var result = await postService.GetAllPosts();
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        var response = new PageResponse<Post>
        {
            TotalItems = result.Value.Count,
            Items = result.Value
        };

        return Ok(response);
    }

    /// <summary>
    /// Create a new post
    /// </summary>
    /// <response code="201">Post successfully created</response>
    /// <response code="400">Invalid or missing fields for creating a new post</response>
    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostRequest request)
    {
        var result = await postService.CreatePost(request.Title, request.Body);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return StatusCode(StatusCodes.Status201Created, result.Value);
    }
}
