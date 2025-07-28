using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        try
        {
            var tasks = await _tasksRepository.GetAllAsync();
            return Ok(tasks);
        }

        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao buscar as tarefas.");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        try
        {
            var task = await _tasksRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound("Tarefa não encontrada.");

            return Ok(task);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao buscar a tarefa.");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {

        try
        {
            task.CreatedAt = DateTime.UtcNow;

            await _tasksRepository.CreateAsync(task);
            var createdTask = await _tasksRepository.GetByIdAsync(task.Id);

            if (createdTask == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao criar a tarefa.");
        }
    }

    [Authorize]
    [HttpPut("{id}")]
public async Task<IActionResult> Update(int id, TaskItem task)
{
    try
    {
        if (id != task.Id)
            return BadRequest("O ID da URL não corresponde ao ID da tarefa.");

        var existingTask = await _tasksRepository.GetByIdAsync(id);
        if (existingTask == null)
            return NotFound("Tarefa não encontrada.");

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;
        existingTask.UserId = task.UserId;

        await _tasksRepository.UpdateAsync(existingTask);

        return NoContent();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return StatusCode(500, "Ocorreu um erro interno ao atualizar a tarefa.");
    }
}
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var task = await _tasksRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound("Tarefa não encontrada.");

            await _tasksRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao deletar a tarefa.");
        }
    }
}