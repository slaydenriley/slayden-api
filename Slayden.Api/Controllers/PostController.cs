using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Requests.Posts;
using Slayden.Api.Responses;
using Slayden.Core.Models;
using Slayden.Core.Services;

namespace Slayden.Api.Controllers;

/// <summary>
/// Controller for managing CRUD on blog posts
/// </summary>
[Route("posts")]
public class PostController(IPostService postService) : SlaydenControllerBase
{
    /// <summary>
    /// Retrieve a specific post by its unique id
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation failure, view error response</response>
    /// <response code="404">Not Found</response>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    /// Retrieve all posts
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation failure, view error response</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
            Items = result.Value,
        };

        return Ok(response);
    }

    /// <summary>
    /// Create a new post
    /// </summary>
    /// <response code="201">Post created successfully</response>
    /// <response code="400">Validation failure, view error response</response>
    /// <param name="title">Title of blog post</param>
    /// <param name="body">Body of blog post, preferably formatted as markdown</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        var result = await postService.CreatePost(request.Title, request.Body);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return StatusCode(StatusCodes.Status201Created, result.Value);
    }

    /// <summary>
    /// Update an existing post
    /// </summary>
    /// <response code="200">Post updated successfully</response>
    /// <response code="400">Validation failure, view error response</response>
    /// <response code="404">Not Found</response>
    /// <param name="title">Title of blog post</param>
    /// <param name="body">Body of blog post, preferably formatted as markdown</param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdatePost(
        [FromRoute] Guid id,
        [FromBody] UpdatePostRequest request
    )
    {
        var existingPost = await postService.GetPostById(id);
        if (existingPost.IsError)
        {
            return BadRequest(existingPost.Errors);
        }

        var updateResult = await postService.UpdatePost(id, request.Title, request.Body);
        if (updateResult.IsError)
        {
            return BadRequest(updateResult.Errors);
        }

        return StatusCode(StatusCodes.Status201Created, updateResult.Value);
    }

    /// <summary>
    /// Delete an existing post
    /// </summary>
    /// <response code="204">Post deleted successfully</response>
    /// <response code="400">Validation failure, view error response</response>
    /// <response code="404">Not Found</response>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeletePost([FromRoute] Guid id)
    {
        var existingPost = await postService.GetPostById(id);
        if (existingPost.IsError)
        {
            return BadRequest(existingPost.Errors);
        }

        var deleteResult = await postService.DeletePost(id);
        if (deleteResult.IsError)
        {
            return BadRequest(deleteResult.Errors);
        }

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
