using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces;
public interface ITodoItemRepository
{
    Task<(List<TodoItem> Items, int TotalCount)> GetAllTodosAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isComplete,
        Guid userId,
        CancellationToken cancellationToken = default);
    Task<TodoItem?> GetTodoByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task AddTodoAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
    Task UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
    Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default);
}

