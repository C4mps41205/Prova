using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Application.DTOs;

public class TaskToDoDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }

    public User? User { get; set; }
}