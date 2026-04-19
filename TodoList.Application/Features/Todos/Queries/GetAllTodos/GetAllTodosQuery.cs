using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Common.Models;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todos.Queries.GetAllTodos;

public class GetAllTodosQuery : IRequest<PagedResult<TodoItemDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public bool? IsComplete { get; set; }
}
