using Application.DTOs;
using Application.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class UserUseCase
{
    private readonly ICrudDefault<User, UserDto> _userAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserUseCase"/> class.
    /// </summary>
    /// <param name="userdApter">The user adapter.</param>
    public UserUseCase(ICrudDefault<User, UserDto> userdApter)
    {
        this._userAdapter = userdApter;
    }
    
    /// <summary>
    /// Creates a new user with the provided user data.
    /// </summary>
    /// <param name="userDto">The user data to create the new user with.</param>
    /// <returns>The newly created user.</returns>
    public async Task<User> Create(UserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            PasswordHash = userDto.PasswordHash,
            CreatedAt = DateTime.UtcNow
        };
    
        return await this._userAdapter.Create(user);
    }
}