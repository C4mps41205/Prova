using System.Security.Authentication;
using System.Web.Helpers;
using Application.DTOs;
using Application.Repositories;
using Domain.Entities;
using Infra.Adapters.Databases.Postgres.DbContext;
using Infra.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.User;

public class UserAdapter : ICrudDefault<Domain.Entities.User, UserDto>
{
    private readonly DbContext _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAdapter"/> class.
    /// </summary>
    /// <param name="iContextFactory">The context factory used to create the database context.</param>
    public UserAdapter(IContextFactory iContextFactory)
    {
        this._database = iContextFactory.CreateDbFactory() as PostgresDbContext;
    }

    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="entity">The user entity to be created.</param>
    /// <returns>The created user entity.</returns>
    /// <exception cref="UserAlreadyExistsException">Thrown when a user with the same name already exists.</exception>
    /// <exception cref="ArgumentException">Thrown when there is an argument exception.</exception>
    /// <exception cref="DbUpdateException">Thrown when there is an error updating the database.</exception>
    /// <exception cref="AuthenticationException">Thrown when there is an unexpected error while creating the user.</exception>
    public async Task<Domain.Entities.User> Create(Domain.Entities.User entity)
    {
        try
        {
            var existingUser = await _database.Set<Domain.Entities.User>()
                .FirstOrDefaultAsync(u => u.Name == entity.Name);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("A user with the same Name already exists.");
            }

            entity.PasswordHash = Crypto.HashPassword(entity.PasswordHash);

            _database.Set<Domain.Entities.User>().Add(entity);
            await _database.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            if (e is ArgumentException)
            {
                throw new ArgumentException(e.Message, e);
            }
            else if (e is DbUpdateException)
            {
                throw new DbUpdateException("An error occurred while creating the task in the database.", e);
            }
            else if (e is UserAlreadyExistsException)
            {
                throw new UserAlreadyExistsException(e.Message, e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while creating the task.", e);
            }
        }
    }

    public Task<Domain.Entities.User> Update(TaskToDo entity)
    {
        // TODO: Update the entity in the database
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.User> Delete(TaskToDo entity)
    {
        // TODO: Delete the entity from the database
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.User> List(int id)
    {
        // TODO: Fetch the entity from the database
        throw new NotImplementedException();
    }

    public Task<List<Domain.Entities.User>> GetAll(int id, int? filter)
    {
        // TODO: Fetch all the entities from the database
        throw new NotImplementedException();
    }
}