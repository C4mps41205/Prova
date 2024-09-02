using Application.DTOs;
using Application.Responses.v1;
using Application.UseCases;
using Application.Responses.v1;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthUseCase _authUseCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authUseCase">The authentication use case.</param>
    public AuthController(AuthUseCase authUseCase)
    {
        this._authUseCase = authUseCase;
    }
    
    /// <summary>
    /// Authenticates a user and returns a response.
    /// </summary>
    /// <param name="login">The login credentials for the user.</param>
    /// <returns>An <see cref="IActionResult"/> containing the authentication response.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown when there is an unexpected error during authentication.</exception>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        try
        {
            var response = this._authUseCase.Authenticate(login);

            return Ok(new ApiResponse
            {
                Status = 200,
                Message = "User authenticated",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    /// <summary>
    /// Handles exceptions that occur during the authentication process.
    /// </summary>
    /// <param name="e">The exception that occurred.</param>
    /// <returns>An <see cref="IActionResult"/> containing the error response.</returns>
    private IActionResult HandleException(Exception e)
    {
        int status;
        string message = e.Message;

        if (e is KeyNotFoundException)
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