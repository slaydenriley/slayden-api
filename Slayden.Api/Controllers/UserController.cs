using Microsoft.AspNetCore.Mvc;
using Slayden.Api.Data;
using Slayden.Api.Models;

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
            var users = _context.User.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
            }

            return BadRequest(ModelState);
        }
    }
}
