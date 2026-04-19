using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Common.Models;
using TodoList.Application.DTOs;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Features.Todos.Queries.GetAllTodos;

public class GetAllTodosHandler(ITodoItemRepository repository) : IRequestHandler<GetAllTodosQuery, PagedResult<TodoItemDto>>
{
    public async Task<PagedResult<TodoItemDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetAllTodosAsync(
            page: request.Page,
            pageSize: request.PageSize,
            searchTerm: request.SearchTerm,
            isComplete: request.IsComplete,
            cancellationToken: cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var dtos = items.Select(t => new TodoItemDto
        {
            id = t.Id,
            title = t.Title,
            description = t.Description ?? string.Empty,
            isComplete = t.IsComplete,
            CreatedAt = t.CreatedAt,
        }).ToList();

        return new PagedResult<TodoItemDto>(
            Items: dtos,
            TotalCount: totalCount,
            Page: request.Page,
            PageSize: request.PageSize,
            TotalPages: totalPages
        );
    }
}
