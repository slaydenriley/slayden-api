using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slayden.Api.Data;
using Slayden.Api.Models;
using Slayden.Api.Responses;

namespace Slayden.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly SlaydenDbContext _context;

        public CommentsController(SlaydenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("comments")]
        public async Task<ActionResult> GetAllComments()
        {
            var comments = _context.Comments
                .Include(c => c.Post)
                .ToList();
            
            var responses = comments.Select(c => new GetCommentResponse
            {
                Id = c.Id,
                Guid = c.Guid,
                CommentorEmail = c.CommentorEmail,
                CommentorName = c.CommentorName,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            });

            return Ok(responses);
        }
        
        [HttpGet]
        [Route("comments/{id}")]
        public async Task<ActionResult> GetCommentById(int id)
        {
            var comment = await  _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }
            
            var response = new GetCommentResponse
            {
                Id = comment.Id,
                Guid = comment.Guid,
                CommentorEmail = comment.CommentorEmail,
                CommentorName = comment.CommentorName,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return Ok(response);
        }
        
        // TODO
        [HttpPost]
        [Route("comments")]
        public IActionResult CreateComment([FromBody] Post post)
        { 
            return BadRequest(ModelState);
        }
    }
}
