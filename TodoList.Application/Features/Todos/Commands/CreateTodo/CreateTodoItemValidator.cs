using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;

namespace TodoList.Application.Features.Todos.Commands.CreateTodo;

public class CreateTodoItemValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoItemValidator()
    {
        RuleFor(x => x.title).NotEmpty().WithMessage("Title is Necessary").MaximumLength(100).WithMessage("Title can't be more than 100 char");
        RuleFor(x => x.description).MaximumLength(500).WithMessage("Description Lenght cant be more than 500 char")
            .NotEmpty().When(x => !string.IsNullOrEmpty(x.description));
    }
}
