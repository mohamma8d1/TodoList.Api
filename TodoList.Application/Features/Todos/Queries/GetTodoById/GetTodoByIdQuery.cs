using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todos.Queries.GetTodoById;

public record GetTodoByIdQuery(Guid id) : IRequest<TodoItemDto?>;
