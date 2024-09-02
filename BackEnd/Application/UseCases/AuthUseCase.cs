using Application.DTOs;
using Application.Repositories;

namespace Application.UseCases;

public class AuthUseCase
{
    private readonly IAuthDefault<LoginDto> _authAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthUseCase"/> class.
    /// </summary>
    /// <param name="authAdapter">The authentication adapter to use for authenticating users.</param>
    public AuthUseCase(IAuthDefault<LoginDto> authAdapter)
    {
        _authAdapter = authAdapter;
    }

    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    /// <param name="login">The login credentials to authenticate.</param>
    /// <returns>A string representing the authenticated user's token or session identifier.</returns>
    public object Authenticate(LoginDto login)
    {
        return this._authAdapter.Authenticate(login);
    }
}