namespace TaskManagerAPI.Interfaces;
using TaskManagerAPI.Models;

public interface ITasksRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId);
}
