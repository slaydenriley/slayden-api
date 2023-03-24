using System.Linq;
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
        public IActionResult Index()
        {
            var posts = _context.Post.ToList();
            return Ok(posts);
        }
    }
}
