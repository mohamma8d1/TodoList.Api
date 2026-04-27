using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Features.Todos.Commands.DeleteTodo;

public record DeleteTodoCommand(Guid id, Guid userId) : IRequest<bool>;

