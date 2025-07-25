namespace TaskManagerAPI.Interfaces;
using TaskManagerAPI.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> AuthenticateAsync(string username, string password);
}
