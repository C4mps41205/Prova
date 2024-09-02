using System.Data.Entity.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using Application.DTOs;
using Application.Repositories;
using Infra.Config.Jwt;
using Microsoft.IdentityModel.Tokens;
using Infra.Adapters.Databases.Postgres.DbContext;

namespace Infra.Adapters.Auth.Jwt;

public class Auth : IAuthDefault<LoginDto>
{
    private readonly PostgresDbContext _database;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Auth"/> class.
    /// </summary>
    /// <param name="iContextFactory">The context factory used to create the PostgresDbContext instance.</param>
    public Auth(IContextFactory iContextFactory)
    {
        this._database = iContextFactory.CreateDbFactory() as PostgresDbContext;
    }
    
    /// <summary>
    /// Authenticates a user based on the provided login credentials.
    /// </summary>
    /// <param name="login">The login credentials of the user.</param>
    /// <returns>A JWT token representing the authenticated user.</returns>
    /// <exception cref="AuthenticationException">Thrown when the login credentials are invalid.</exception>
    /// <exception cref="ArgumentException">Thrown when the login credentials are invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an unexpected error occurs during authentication.</exception>
    public object Authenticate(LoginDto login)
    {
        try
        {
            var user = this._database.User
                .FirstOrDefault(
                    x => x.Name == login.Name);

            if (user == null)
            {
                throw new KeyNotFoundException("Invalid username or password.");
            }

            if (!Crypto.VerifyHashedPassword(user.PasswordHash, login.Password))
            {
                throw new KeyNotFoundException("Invalid username or password."); 
            }
            
            var tokenHandles = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Name, login.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                                                            SecurityAlgorithms.HmacSha256Signature),
            };
            
            var token = tokenHandles.CreateToken(tokenDescriptor);
            return new
            {
                user = user,
                token = tokenHandles.WriteToken(token),
            };
        }
        catch (Exception e)
        {
            if (e is KeyNotFoundException)
            {
                throw new KeyNotFoundException("Invalid login credentials.", e);
            }
            else if (e is InvalidOperationException)
            {
                throw;
            }
            else 
            {
                throw new AuthenticationException("An unexpected error occurred during authentication.", e); 
            }
        }
    }
}