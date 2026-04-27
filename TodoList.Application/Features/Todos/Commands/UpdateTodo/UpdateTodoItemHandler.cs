using MediatR;
using System;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Features.Todos.Commands.UpdateTodo;

public class UpdateTodoItemHandler(ITodoItemRepository repository) : IRequestHandler<UpdateTodoCommand, TodoItemDto?>
{
    public async Task<TodoItemDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await repository.GetTodoByIdAsync(request.id, request.userId, cancellationToken);

        if (todoItem is null || todoItem.UserId != request.userId)
            return null;

        if (!string.IsNullOrWhiteSpace(request.title))
            todoItem.Title = request.title;

        if(request.description != null)
            todoItem.Description = request.description;

        if(request.isComplete.HasValue)
            todoItem.IsComplete = request.isComplete.Value;

        await repository.UpdateTodoAsync(todoItem, cancellationToken);

        return new TodoItemDto
        {
            id = todoItem.Id,
            title = todoItem.Title ?? string.Empty,
            description = todoItem.Description ?? string.Empty,
            isComplete = todoItem.IsComplete,
            CreatedAt = todoItem.CreatedAt,
        };
    }

}
