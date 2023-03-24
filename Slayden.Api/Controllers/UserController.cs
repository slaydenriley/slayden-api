using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Data;
using Slayden.Api.Models;
using Microsoft.EntityFrameworkCore;
using Slayden.Api.Responses;

namespace Slayden.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SlaydenDbContext _context;

        public UsersController(SlaydenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.Include(u => u.Posts).ToList();
            var response = users.Select(u => new GetUserResponse
            {
                Id = u.Id,
                Guid = u.Guid,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Posts = u.Posts.Select(p => new GetPostResponse
                {
                    Id = p.Id,
                    Guid = p.Guid,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList()
            });

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
            }

            return BadRequest(ModelState);
        }
    }
}
