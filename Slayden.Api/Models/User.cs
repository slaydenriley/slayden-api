using System.ComponentModel.DataAnnotations.Schema;

namespace Slayden.Api.Models;

public class User
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("guid")]
    public Guid Guid { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("permission_level")]
    public int PermissionLevel { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
