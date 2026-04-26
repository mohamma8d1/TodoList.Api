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


namespace TodoList.Application.Features.Auths.Queries.Login;

public class LoginUserHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtService jwtService
    ) : IRequestHandler<LoginUserQuery, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.dto.Email);
        if (user == null)
            return new AuthResponse { Token = string.Empty, ErrorMessege = "Invalid creadintials" };

        var result = await signInManager.CheckPasswordSignInAsync(user, request.dto.Password, false);
        if(!result.Succeeded)
            return new AuthResponse { Token = string.Empty, ErrorMessege = "Invalid creadintials" };

        var token = jwtService.GenerateToken(user);
        return new AuthResponse { Token = token };
    }
}
