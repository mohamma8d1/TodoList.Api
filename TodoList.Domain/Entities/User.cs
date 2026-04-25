using System;
namespace TodoList.Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;    

    public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>();
}
