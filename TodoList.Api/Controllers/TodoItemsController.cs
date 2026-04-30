using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Application.Features.Todos.Commands.CreateTodo;
using TodoList.Application.Features.Todos.Commands.DeleteTodo;
using TodoList.Application.Features.Todos.Commands.UpdateTodo;
using TodoList.Application.Features.Todos.Queries.GetAllTodos;
using TodoList.Application.Features.Todos.Queries.GetTodoById;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemsController(IMediator mediator) : ControllerBase
    {
        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
                throw new UnauthorizedAccessException("Invalid user id");
            return userId;
        }
        q
        [HttpGet]
        public async Task<ActionResult<List<TodoItemDto>>> GetAllTodo([FromQuery] GetAllTodosQuery query)
        {
            query.UserId = GetUserId();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoById(Guid id)
        {
            var userId = GetUserId();
            var result = await mediator.Send(new GetTodoByIdQuery(id, userId));
            if (result == null)
                return NotFound( new { message = "Todo Not Found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoDto request)
        {
            var userId = GetUserId();
            var command = new CreateTodoCommand(userId, request.Title, request.Description);
            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetTodoById), new { id }, new { id, message = "Todo Created Successfully" });
        }

        [HttpPut("{id:guid}")]
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoItemDto dto)
        {
            var userId = GetUserId();
            var command = new UpdateTodoCommand(
                id,
                userId,
                dto.title,
                dto.description,
                dto.isComplete
            );

            var result = await mediator.Send(command);

            if( result is null)
                return NotFound( new { message = "Todo Not Found "});

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTodo(Guid id)
        {
            var userId = GetUserId();
            var result = await mediator.Send(new DeleteTodoCommand(id, userId));

            if (!result)
                return NotFound(new { message = "Not Found!" });

            return NoContent();
        }

    }
}
