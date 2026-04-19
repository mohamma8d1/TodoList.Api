using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Common.Interfaces;
using TodoList.Application.DTOs;


namespace TodoList.Application.Features.Todos.Commands.CreateTodo;

public record CreateTodoCommand(string title, string? description) : ICommand<Guid> { }
