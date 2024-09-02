using Application.DTOs;
using Application.Responses.v1;
using Application.UseCases;
using Infra.Adapters.User;
using Infra.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserUseCase _userUseCase;


    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="userUseCase">The user use case instance to be used by the controller.</param>
    public UserController(UserUseCase userUseCase)
    {
        this._userUseCase = userUseCase;
    }
    
    /// <summary>
    /// Creates a new user based on the provided user data transfer object (DTO).
    /// </summary>
    /// <param name="userDto">The user data transfer object containing the user information to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing an <see cref="ApiResponse"/> with the status code 201 and the created user data if successful, or an <see cref="IActionResult"/> containing an <see cref="ApiResponse"/> with an appropriate error status code and message if an exception occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        try
        {
            var response = await this._userUseCase.Create(userDto);
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
    /// Handles exceptions that occur during the execution of the controller actions.
    /// </summary>
    /// <param name="e">The exception that occurred.</param>
    /// <returns>An <see cref="IActionResult"/> containing an <see cref="ApiResponse"/> with the appropriate HTTP status code and error message.</returns>
    private IActionResult HandleException(Exception e)
    {
        int status;
        string message = e.Message;

        if (e is ArgumentException)
            status = 400;
        else if (e is KeyNotFoundException)
            status = 409;
        else if (e is UserAlreadyExistsException)
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