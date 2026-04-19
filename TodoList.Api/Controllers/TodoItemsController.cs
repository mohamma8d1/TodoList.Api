using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TodoItemsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<TodoItemDto>>> GetAllTodo([FromQuery] GetAllTodosQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoById(Guid id)
        {
            var result = await mediator.Send(new GetTodoByIdQuery(id));
            if (result == null)
                return NotFound( new { message = "Todo Not Found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoCommand command)
        {
            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetTodoById), new { id }, new { id, message = "Todo Created Sucessfuly" });
        }

        [HttpPut("{id:guid}")]
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoItemDto dto)
        {
            var command = new UpdateTodoCommand(id)
            {
                title = dto.title,
                description = dto.description,
                isComplete = dto.isComplete,
            };

            var result = await mediator.Send(command);

            if( result is null)
                return NotFound( new { message = "Todo Not Found "});

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTodo(Guid id)
        {
            var result = await mediator.Send(new DeleteTodoCommand(id));

            if (!result)
                return NotFound(new { message = "Not Found!" });

            return NoContent();
        }

    }
}
