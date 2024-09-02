using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserDto
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? PasswordHash { get; set; }
}