using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Data;
using Slayden.Api.Models;
using Microsoft.EntityFrameworkCore;
using Slayden.Api.Responses;

namespace Slayden.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly SlaydenDbContext _context;

        public UsersController(SlaydenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            try
            {
                var response = users.Select(u => new GetUserResponse
                {
                    Id = u.Id,
                    Guid = u.Guid,
                    Name = u.Name,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                });
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest($"Something went wrong: {e}");
            }
        }
        
        [HttpGet]
        [Route("users/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new GetUserResponse
            {
                Id = user.Id,
                Guid = user.Guid,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };

            return Ok(response);
        }
        
        [HttpGet]
        [Route("users/{id}/posts")]
        public async Task<ActionResult<User>> GetUserPosts(int id)
        {
            var user = await _context.Users
                .Include(u => u.Posts) // include posts
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new GetUserResponse
            {
                Id = user.Id,
                Guid = user.Guid,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Posts = user.Posts?.Select(p => new GetPostResponse
                {
                    Id = p.Id,
                    Title = p.Title,
                    AuthorName = p.User.Name,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("users")]
        public IActionResult Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }

            return BadRequest(ModelState);
        }
    }
}
