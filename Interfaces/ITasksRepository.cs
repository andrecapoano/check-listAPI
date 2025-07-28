namespace TaskManagerAPI.Interfaces;
using TaskManagerAPI.Models;

public interface ITasksRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
}
