using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slayden.Api.Data;
using Slayden.Api.Models;
using Slayden.Api.Responses;

namespace Slayden.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly SlaydenDbContext _context;

        public PostsController(SlaydenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var posts = _context.Posts.Include(p => p.User).ToList();
            var postResponses = posts.Select(p => new GetPostResponse
            {
                Id = p.Id,
                Guid = p.Guid,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                AuthorName = p.User.Name
            });

            return Ok(postResponses);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAllPosts), new { id = post.Id }, post);
            }

            return BadRequest(ModelState);
        }
    }
}
