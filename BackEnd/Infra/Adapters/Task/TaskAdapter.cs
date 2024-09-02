using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using Application.DTOs;
using Infra.Adapters.Databases.Postgres.DbContext;

namespace Infra.Adapters.Task;

public class TaskAdapter : ICrudDefault<TaskToDo, TaskToDoDto>
{
    private readonly DbContext _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskAdapter"/> class.
    /// </summary>
    /// <param name="iContextFactory">The context factory used to create the database context.</param>
    public TaskAdapter(IContextFactory iContextFactory)
    {
        this._database = iContextFactory.CreateDbFactory() as PostgresDbContext;
    }

    /// <summary>
    /// Retrieves a task from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <returns>The task with the specified ID, or throws a <see cref="KeyNotFoundException"/> if the task is not found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the task with the specified ID is not found in the database.</exception>
    /// <exception cref="AuthenticationException">Thrown if an unexpected error occurs while fetching the task.</exception>
    public async Task<TaskToDo> List(int id)
    {
        try
        {
            var task = await _database.Set<TaskToDo>().FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            return task;
        }
        catch (Exception e)
        {
            if (e is KeyNotFoundException)
            {
                throw new KeyNotFoundException(e.Message, e);
            }
            else if (e is InvalidOperationException)
            {
                throw;
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while fetching the task.", e);
            }
        }
    }

    /// <summary>
    /// Retrieves a list of tasks for the specified user ID.
    /// </summary>
    /// <param name="id">The ID of the user whose tasks should be retrieved.</param>
    /// <param name="filter"></param>
    /// <returns>A list of <see cref="TaskToDo"/> entities for the specified user.</returns>
    /// <exception cref="ArgumentException">Thrown if an argument exception occurs while retrieving the tasks.</exception>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while retrieving the tasks from the database.</exception>
    /// <exception cref="AuthenticationException">Thrown if an unexpected error occurs while retrieving the tasks.</exception>
    public Task<List<TaskToDo>> GetAll(int id, int? filter)
    {
        try
        {
            var query = _database.Set<TaskToDo>().AsQueryable();
            query = query.Where(task => task.UserId == id);

            if (filter != null && filter > 0)
            {
                query = query.Where(task => Equals(task.CategoryId, filter));
            }

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
                throw new DbUpdateException("An error occurred while geting the tasks in the database.", e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while fetching the task.", e);
            }
        }
    }

    /// <summary>
    /// Creates a new task in the database.
    /// </summary>
    /// <param name="entity">The task entity to be created.</param>
    /// <returns>The created task entity.</returns>
    /// <exception cref="ArgumentException">Thrown if an argument exception occurs while creating the task.</exception>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while creating the task in the database.</exception>
    /// <exception cref="AuthenticationException">Thrown if an unexpected error occurs while creating the task.</exception>
    public async Task<TaskToDo> Create(TaskToDo entity)
    {
        try
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            _database.Set<TaskToDo>().Add(entity);
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
            else
            {
                throw new AuthenticationException("An unexpected error occurred while creating the task.", e);
            }
        }
    }

    /// <summary>
    /// Updates an existing task in the database.
    /// </summary>
    /// <param name="entity">The updated task entity.</param>
    /// <returns>The updated task entity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the task with the specified ID is not found.</exception>
    /// <exception cref="DbUpdateConcurrencyException">Thrown if a concurrency error occurs while updating the task.</exception>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while updating the task in the database.</exception>
    /// <exception cref="AuthenticationException">Thrown if an unexpected error occurs while updating the task.</exception>
    public async Task<TaskToDo> Update(TaskToDo entity)
    {
        try
        {
            var existingTask = await _database.Set<TaskToDo>().FindAsync(entity.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {entity.Id} not found.");
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _database.Entry(existingTask).CurrentValues.SetValues(entity);
            await _database.SaveChangesAsync();

            return existingTask;
        }
        catch (Exception e)
        {
            if (e is KeyNotFoundException)
            {
                throw new KeyNotFoundException(e.Message, e);
            }
            else if (e is InvalidOperationException)
            {
                throw;
            }
            else if (e is DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("A concurrency error occurred while updating the task.", e);
            }
            else if (e is DbUpdateException)
            {
                throw new DbUpdateException("An error occurred while updating the task in the database.", e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while updating the task.", e);
            }
        }
    }

    /// <summary>
    /// Deletes a task from the database.
    /// </summary>
    /// <param name="entity">The task to be deleted.</param>
    /// <returns>The deleted task.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the task with the specified ID is not found.</exception>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while deleting the task from the database.</exception>
    /// <exception cref="AuthenticationException">Thrown if an unexpected error occurs while deleting the task.</exception>
    public async Task<TaskToDo> Delete(TaskToDo entity)
    {
        try
        {
            var existingTask = await _database.Set<TaskToDo>().FindAsync(entity.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {entity.Id} not found.");
            }

            _database.Set<TaskToDo>().Remove(existingTask);
            await _database.SaveChangesAsync();

            return existingTask;
        }
        catch (Exception e)
        {
            if (e is KeyNotFoundException)
            {
                throw new KeyNotFoundException(e.Message, e);
            }
            else if (e is InvalidOperationException)
            {
                throw;
            }
            else if (e is DbUpdateException)
            {
                throw new DbUpdateException("An error occurred while deleting the task from the database.", e);
            }
            else
            {
                throw new AuthenticationException("An unexpected error occurred while deleting the task.", e);
            }
        }
    }
}