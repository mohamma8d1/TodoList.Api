using MediatR;
using System;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(
    Guid id,
    string? title,
    string? description,
    bool? isComplete
) : IRequest<TodoItemDto?>;