using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category : EntityBase
{
    [Required]
    public string Name { get; set; }
    
    public User User { get; set; }

    public Guid? UserId { get; set; }
}
