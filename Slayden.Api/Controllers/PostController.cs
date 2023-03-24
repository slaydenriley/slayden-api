using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Data;
using Slayden.Api.Models;

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
        public IActionResult GetAll()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAll), new { id = post.Id }, post);
            }

            return BadRequest(ModelState);
        }
    }
}
