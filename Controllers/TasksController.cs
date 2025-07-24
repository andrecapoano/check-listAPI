using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using TaskManagerAPI.Interfaces;

namespace TaskManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _tasksRepository;

    public TasksController(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        return Ok(await _tasksRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;

        await _tasksRepository.CreateAsync(task);
        var createdTask = await _tasksRepository.GetByIdAsync(task.Id);

        if (createdTask == null)
            return BadRequest();

        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
    {
        if (id != task.Id)
            return BadRequest();

        var existingTask = await _tasksRepository.GetByIdAsync(id);
        if (existingTask == null)
            return NotFound();

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;
        existingTask.UserId = task.UserId;

        await _tasksRepository.UpdateAsync(existingTask);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        await _tasksRepository.DeleteAsync(id);
        return NoContent();
    }
}