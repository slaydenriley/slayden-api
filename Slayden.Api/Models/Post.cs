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

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
    
    [ForeignKey("user_id")]
    public User? User { get; set; }
    
    public virtual ICollection<Comment>? Comments { get; set; }
}
