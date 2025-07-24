using System.Text.Json.Serialization;

namespace TaskManagerAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    [JsonIgnore]
    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
   