using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Application.Responses;

namespace TodoList.Application.Features.Auths.Commands.Register;

public record RegisterUserCommand(RegisterUserDto dto) : IRequest<AuthResponse>;
