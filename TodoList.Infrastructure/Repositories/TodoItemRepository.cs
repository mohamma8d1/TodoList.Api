
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Repositories;
public class TodoItemRepository(ApplicationDbContext context) : ITodoItemRepository
{
    public async Task AddTodoAsync(TodoItem todoItem, CancellationToken cancellationToken)
    {
        await context.TodoItems.AddAsync(todoItem , cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken)
    {
        var todoItem = await context.TodoItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (todoItem != null)
        {
            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<(List<TodoItem> Items, int TotalCount)> GetAllTodosAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isComplete,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var query = context.TodoItems.Where(t => t.UserId == userId).AsNoTracking();

        // For Search Feature
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t =>
                t.Title.Contains(searchTerm) ||
                (t.Description != null && t.Description.Contains(searchTerm)));
        }

        // Filter by IsComplete
        if (isComplete.HasValue)
        {
            query = query.Where(t => t.IsComplete == isComplete.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public Task<TodoItem?> GetTodoByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken) => context.TodoItems.Where(e => e.UserId == userId).AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id,
        cancellationToken);

    public async Task UpdateTodoAsync(TodoItem todoItem, CancellationToken cancellationToken)
    {
        context.TodoItems.Attach(todoItem);
        context.Entry(todoItem).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }
}

