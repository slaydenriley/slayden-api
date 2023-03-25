using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slayden.Api.Data;
using Slayden.Api.Models;
using Slayden.Api.Responses;

namespace Slayden.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class PostsController : ControllerBase
    {
        private readonly SlaydenDbContext _context;

        public PostsController(SlaydenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetAllPosts()
        {
            var posts = _context.Posts
                .Include(p => p.User)
                .Include(p=> p.Comments)
                .ToList();
            
            var postResponses = posts.Select(p => new GetPostResponse
            {
                Id = p.Id,
                Guid = p.Guid,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                AuthorName = p.User.Name,
                Comments = p.Comments?.Select(c => new GetCommentResponse
                {
                    Id = c.Id,
                    Guid = c.Guid,
                    CommentorName = c.CommentorName,
                    CommentorEmail = c.CommentorEmail,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToList()
            });

            return Ok(postResponses);
        }
        
        [HttpGet]
        [Route("posts/{id}")]
        public async Task<ActionResult> GetPostById(int id)
        {
            var post = await  _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            var response = new GetPostResponse
            {
                Id = post.Id,
                Guid = post.Guid,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                AuthorName = post.User.Name,
                Comments = post.Comments?.Select(c => new GetCommentResponse
                {
                    Id = c.Id,
                    Guid = c.Guid,
                    CommentorName = c.CommentorName,
                    CommentorEmail = c.CommentorEmail,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return Ok(response);
        }

        // TODO
        [HttpPost]
        [Route("posts")]
        public IActionResult CreatePost([FromBody] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAllPosts), new { id = post.Id }, post);
            }

            return BadRequest(ModelState);
        }
        
        // TODO
        [HttpPost]
        [Route("posts/{id}/comments")]
        public IActionResult CreatePostComment([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetPostById), new { id = comment.Post.Id }, comment.Post);
            }

            return BadRequest(ModelState);
        }
    }
}
