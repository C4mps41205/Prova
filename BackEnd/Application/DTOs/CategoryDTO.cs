using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CategoryDto
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "User is required.")]
    public int UserId { get; set; }

}