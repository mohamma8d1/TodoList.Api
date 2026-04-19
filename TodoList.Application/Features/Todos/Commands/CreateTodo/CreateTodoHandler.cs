using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Features.Todos.Commands.CreateTodo;

public class CreateTodoHandler(ITodoItemRepository repository) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = request.title,
            Description = request.description,
            IsComplete = false,
            CreatedAt = DateTime.Now,
        };

        await repository.AddTodoAsync(todoItem);

        return todoItem.Id;
    }
}
