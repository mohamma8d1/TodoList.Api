
namespace TodoList.Domain.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
}

