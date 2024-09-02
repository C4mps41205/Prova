using Application.DTOs;
using Application.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases;

public class TaskToDoUseCase
{
    private readonly ICrudDefault<TaskToDo, TaskToDoDto> _taskToDoAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskToDoUseCase"/> class.
    /// </summary>
    /// <param name="taskToDoAdapter">The adapter for performing CRUD operations on TaskToDo entities.</param>
    public TaskToDoUseCase(ICrudDefault<TaskToDo, TaskToDoDto> taskToDoAdapter)
    {
        _taskToDoAdapter = taskToDoAdapter;
    }

    /// <summary>
    /// Retrieves the TaskToDo item with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the TaskToDo item to retrieve.</param>
    /// <returns>The TaskToDo item with the specified ID.</returns>
    public async Task<TaskToDo> Get(int id)
    {
        return await this._taskToDoAdapter.List(id);
    }

    /// <summary>
    /// Creates a new TaskToDo item with the specified details.
    /// </summary>
    /// <param name="taskToDoDto">A DTO containing the details for the new TaskToDo item.</param>
    /// <returns>The newly created TaskToDo item.</returns>
    public async Task<TaskToDo> Create(TaskToDoDto taskToDoDto)
    {
        var taskToDo = new TaskToDo
        {
            Title = taskToDoDto.Title,
            Description = taskToDoDto.Description,
            IsCompleted = taskToDoDto.IsCompleted,
            CategoryId = taskToDoDto.CategoryId,
            UserId = taskToDoDto.UserId
        };
        return await this._taskToDoAdapter.Create(taskToDo);
    }

    /// <summary>
    /// Updates an existing TaskToDo item with the specified details.
    /// </summary>
    /// <param name="taskToDoDto">A DTO containing the updated details for the TaskToDo item.</param>
    /// <returns>The updated TaskToDo item.</returns>
    public async Task<TaskToDo> Update(TaskToDoDto taskToDoDto)
    {
        var taskToDo = new TaskToDo
        {
            Id = taskToDoDto.Id,
            Title = taskToDoDto.Title,
            Description = taskToDoDto.Description,
            IsCompleted = taskToDoDto.IsCompleted,
            CategoryId = taskToDoDto.CategoryId,
            UserId = taskToDoDto.UserId
        };
        return await this._taskToDoAdapter.Update(taskToDo);
    }

    /// <summary>
    /// Deletes the TaskToDo item with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the TaskToDo item to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Delete(int id)
    {
        var taskToDo = await this._taskToDoAdapter.List(id);
        await this._taskToDoAdapter.Delete(taskToDo);
    }

    /// <summary>
    /// Retrieves all TaskToDo items for the specified user ID.
    /// </summary>
    /// <param name="id">The user ID to retrieve TaskToDo items for.</param>
    /// <param name="filter"></param>
    /// <returns>A list of TaskToDo items for the specified user.</returns>
    public async Task<List<TaskToDo>> GetAll(int id, int? filter = null)
    {
        return await this._taskToDoAdapter.GetAll(id, filter == null ? 0 : filter );
    }
}