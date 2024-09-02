using System.Security.Authentication;
using Application.DTOs;
using Application.Repositories;
using Domain.Entities;
using Infra.Adapters.Databases.Postgres.DbContext;
using Infra.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Category;

public class CategoryAdapter : ICrudDefault<Domain.Entities.Category, CategoryDto>
{
       private readonly DbContext _database;

    public CategoryAdapter(IContextFactory iContextFactory)
    {
        this._database = iContextFactory.CreateDbFactory() as PostgresDbContext;
    }

    /// <summary>
    /// Creates a new category in the database.
    /// </summary>
    /// <param name="entity">The category entity to create.</param>
    /// <returns>The created category entity.</returns>
    /// <exception cref="ArgumentException">Thrown when an argument is invalid.</exception>
    /// <exception cref="DbUpdateException">Thrown when an error occurs while creating the category in the database.</exception>
    /// <exception cref="UserAlreadyExistsException">Thrown when a category with the same name already exists.</exception>
    /// <exception cref="AuthenticationException">Thrown when an unexpected error occurs while creating the category.</exception>
    public async Task<Domain.Entities.Category> Create(Domain.Entities.Category entity)
    {
        try
        {
            var existingUser = await _database.Set<Domain.Entities.Category>()
                .FirstOrDefaultAsync(u => u.Name == entity.Name);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("A category with the same Name already exists.");
            }

            _database.Set<Domain.Entities.Category>().Add(entity);
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
                throw new DbUpdateException("An error occurred while creating the category in the database.", e);
            }
            else if (e is UserAlreadyExistsException)
            {
                throw new UserAlreadyExistsException(e.Message, e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while creating the cateogry.", e);
            }
        }
    }

    public Task<Domain.Entities.Category> Update(TaskToDo entity)
    {
        // TODO: Update the entity in the database
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Category> Delete(TaskToDo entity)
    {
        // TODO: Delete the entity from the database
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Category> List(int id)
    {
        // TODO: Fetch the entity from the database
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves all categories from the database.
    /// </summary>
    /// <param name="id">The ID of the category to filter by (optional).</param>
    /// <param name="filter">The filter to apply to the categories (optional).</param>
    /// <returns>A task that represents the asynchronous operation of retrieving all categories.</returns>
    /// <exception cref="ArgumentException">Thrown when an argument is invalid.</exception>
    /// <exception cref="DbUpdateException">Thrown when an error occurs while retrieving the categories from the database.</exception>
    /// <exception cref="AuthenticationException">Thrown when an unexpected error occurs while fetching the categories.</exception>
    public Task<List<Domain.Entities.Category>> GetAll(int id, int? filter)
    {
        try
        {
            var query = _database.Set<Domain.Entities.Category>().AsQueryable();
            query = query.Where(task => task.UserId == id);

            return query.ToListAsync();
        }
        catch (Exception e)
        {
            if (e is ArgumentException)
            {
                throw new ArgumentException(e.Message, e);
            }
            else if (e is DbUpdateException)
            {
                throw new DbUpdateException("An error occurred while geting the categories in the database.", e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while fetching the categories.", e);
            }
        }
    }
}