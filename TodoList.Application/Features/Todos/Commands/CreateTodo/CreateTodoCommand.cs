using System;
using TodoList.Application.Common.Interfaces;


namespace TodoList.Application.Features.Todos.Commands.CreateTodo;

public record CreateTodoCommand(Guid UserId, string title, string? description) : ICommand<Guid>;
