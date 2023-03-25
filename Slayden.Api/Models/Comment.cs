using System.ComponentModel.DataAnnotations.Schema;

namespace Slayden.Api.Models;

public class Comment
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("guid")]
    public Guid Guid { get; set; }
    
    [Column("commentor_email")]
    public string CommentorEmail { get; set; }
    
    [Column("commentor_name")]
    public string CommentorName { get; set; }
    
    [Column("content")]
    public string Content { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }
    
    [ForeignKey("post_id")]
    public Post? Post { get; set; }
}
