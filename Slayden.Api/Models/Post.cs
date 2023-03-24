using System.ComponentModel.DataAnnotations.Schema;

namespace Slayden.Api.Models;

public class Post
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("guid")]
    public Guid Guid { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
    
    [Column("content")]
    public string Content { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
    [Column("deleted_at")]
    public DateTime DeletedAt { get; set; }
    
    public User User { get; set; }
}