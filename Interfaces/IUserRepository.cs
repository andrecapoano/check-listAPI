namespace TaskManagerAPI.Interfaces;
using TaskManagerAPI.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<User?> AuthenticateAsync(string username, string password);
    Task<User?> GetByUsernameAsync(string username);
}
