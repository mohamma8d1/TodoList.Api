using MediatR;
using System;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(Guid id) : UpdateTodoItemDto, IRequest<TodoItemDto?>;
