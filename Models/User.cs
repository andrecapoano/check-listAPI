using System.Text.Json.Serialization;

namespace TaskManagerAPI.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    // [JsonIgnore]
    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
   