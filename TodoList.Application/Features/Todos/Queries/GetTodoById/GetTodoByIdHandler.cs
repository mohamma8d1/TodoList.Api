using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Features.Todos.Queries.GetTodoById;

public class GetTodoByIdHandler(ITodoItemRepository repository) : IRequestHandler<GetTodoByIdQuery, TodoItemDto?>
{
    public async Task<TodoItemDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await repository.GetTodoByIdAsync(request.id, request.userId, cancellationToken);

        if(todoItem is null)
            return null;

        return new TodoItemDto {
            id = todoItem.Id,
            title = todoItem.Title ?? string.Empty,
            description = todoItem.Description ?? string.Empty,
            CreatedAt = todoItem.CreatedAt,
        };
    }
}
