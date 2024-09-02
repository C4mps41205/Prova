using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public ICollection<TaskToDo> Tasks { get; set; } = new List<TaskToDo>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}