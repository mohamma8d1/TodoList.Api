using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using TodoList.Application.DTOs;
using TodoList.Application.Features.Auths.Commands.Register;
using TodoList.Application.Features.Auths.Queries.Login;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(new RegisterUserDto
            {
                Email = request.Email,
                Username = request.UserName,
                Password = request.Password
            });

            var result = await mediator.Send(command);

            if (!string.IsNullOrEmpty(result.ErrorMessege))
                return BadRequest(new { message = result.ErrorMessege });

            return Ok(new { token = result.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var command = new LoginUserQuery(new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password
            });

            var result = await mediator.Send(command);

            if (!string.IsNullOrEmpty(result.ErrorMessege))
                return BadRequest(new { message = result.ErrorMessege });

            return Ok(new { token = result.Token });

        }
    }
}
