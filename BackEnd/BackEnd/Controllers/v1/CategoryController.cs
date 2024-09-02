using Application.DTOs;
using Application.Responses.v1;
using Application.UseCases;
using Infra.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryUseCase _categoryUseCase;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryUseCase">The category use case instance to be used by the controller.</param>
    public CategoryController(CategoryUseCase categoryUseCase)
    {
        this._categoryUseCase = categoryUseCase;
    }
    
    /// <summary>
    /// Creates a new category based on the provided CategoryDto.
    /// </summary>
    /// <param name="userDto">The CategoryDto containing the details of the new category to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing an <see cref="ApiResponse"/> with the created category.</returns>
    /// <exception cref="ArgumentException">Thrown when the input parameters are invalid.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the requested category is not found.</exception>
    /// <exception cref="CategoryAlreadyExistsException">Thrown when the requested category already exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs.</exception>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto userDto)
    {
        try
        {
            var response = await this._categoryUseCase.Create(userDto);
            return Ok(new ApiResponse
            {
                Status = 201,
                Message = "User created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    /// <summary>
    /// Retrieves a list of categories, optionally filtered by an ID or a category ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve, or null to retrieve all categories.</param>
    /// <param name="category">The category ID to filter the results by, or null to retrieve all categories.</param>
    /// <returns>An <see cref="IActionResult"/> containing an <see cref="ApiResponse"/> with the retrieved categories.</returns>
    /// <exception cref="ArgumentException">Thrown when the input parameters are invalid.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the requested category is not found.</exception>
    /// <exception cref="CategoryAlreadyExistsException">Thrown when the requested category already exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs.</exception>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Get(int id , [FromQuery] int? category = null)
    {
        try
        {
            var response = await this._categoryUseCase.Get(id, 0);
            return Ok(new ApiResponse
            {
                Status = 200,
                Message = "Task retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    /// <summary>
    /// Handles exceptions that occur in the CategoryController by returning an appropriate HTTP status code and an ApiResponse object.
    /// </summary>
    /// <param name="e">The exception that occurred.</param>
    /// <returns>An IActionResult containing an ApiResponse with the appropriate status code and error message.</returns>
    private IActionResult HandleException(Exception e)
    {
        int status;
        string message = e.Message;

        if (e is ArgumentException)
            status = 400;
        else if (e is KeyNotFoundException)
            status = 409;
        else if (e is CategoryAlreadyExistsException)
            status = 404;
        else if (e is InvalidOperationException)
            status = 500;
        else
        {
            status = 500;
            message = "An unexpected error occurred.";
        }

        return StatusCode(status, new ApiResponse
        {
            Status = status,
            Message = message,
            Data = null
        });
    }
}