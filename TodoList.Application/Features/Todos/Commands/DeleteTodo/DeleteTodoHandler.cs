using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Features.Todos.Commands.DeleteTodo;

public class DeleteTodoHandler(ITodoItemRepository repository) : IRequestHandler<DeleteTodoCommand, bool>
{
    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await repository.GetTodoByIdAsync(request.id, cancellationToken);

        if (todoItem is null)
            return false;

        await repository.DeleteTodoAsync(request.id, cancellationToken);
        return true;
    }
}
