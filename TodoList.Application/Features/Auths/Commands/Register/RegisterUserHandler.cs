using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Responses;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Services;

namespace TodoList.Application.Features.Auths.Commands.Register;

public class RegisterUserHandler(UserManager<User> userManager, IJwtService jwtService) : IRequestHandler<RegisterUserCommand,AuthResponse>
{

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var dto = request.dto;
        var user = new User
        {
            UserName = dto.Username,
            Email = dto.Email
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return new AuthResponse { Token = string.Empty, ErrorMessege = string.Join("; ", result.Errors.Select(e => e.Description)) };

        var token = jwtService.GenerateToken(user);
        return new AuthResponse { Token = token };
    }
}
