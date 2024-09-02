using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public ICollection<TaskToDo> Tasks { get; set; }
}