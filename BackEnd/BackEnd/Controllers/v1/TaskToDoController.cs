using Application.DTOs;
using Application.Responses.v1;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace BackEnd.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class TaskToDoController : ControllerBase
{
    private readonly TaskToDoUseCase _taskToDoUseCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskToDoController"/> class.
    /// </summary>
    /// <param name="taskToDoUseCase">The task to do use case.</param>
    public TaskToDoController(TaskToDoUseCase taskToDoUseCase)
    {
        this._taskToDoUseCase = taskToDoUseCase;
    }

    /// <summary>
    /// Retrieves all tasks for the specified user ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve tasks for.</param>
    /// <returns>An <see cref="IActionResult"/> containing the retrieved tasks in an <see cref="ApiResponse"/>.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while retrieving the tasks.</exception>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetAll(int id, [FromQuery] int? category = null)
    {
        try
        {
            var response = await this._taskToDoUseCase.GetAll(id, category);
            return Ok(new ApiResponse
            {
                Status = 200,
                Message = "Tasks retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
    
    /// <summary>
    /// Retrieves a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the retrieved task in an <see cref="ApiResponse"/>.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while retrieving the task.</exception>
    [HttpGet("/task/{id:int}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var response = await this._taskToDoUseCase.Get(id);
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
    /// Creates a new task.
    /// </summary>
    /// <param name="taskToDoDto">The task data to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing the created task in an <see cref="ApiResponse"/>.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while creating the task.</exception>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] TaskToDoDto taskToDoDto)
    {
        try
        {
            var response = await this._taskToDoUseCase.Create(taskToDoDto);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, new ApiResponse
            {
                Status = 201,
                Message = "Task created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="taskToDoDto">The updated task data.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated task in an <see cref="ApiResponse"/>.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while updating the task.</exception>
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] TaskToDoDto taskToDoDto)
    {
        try
        {
            taskToDoDto = taskToDoDto;
            var response = await this._taskToDoUseCase.Update(taskToDoDto);
            return Ok(new ApiResponse
            {
                Status = 200,
                Message = "Task updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>An <see cref="IActionResult"/> containing a success message in an <see cref="ApiResponse"/>.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while deleting the task.</exception>
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await this._taskToDoUseCase.Delete(id);
            return Ok(new ApiResponse
            {
                Status = 200,
                Message = "Task deleted successfully",
                Data = null
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
