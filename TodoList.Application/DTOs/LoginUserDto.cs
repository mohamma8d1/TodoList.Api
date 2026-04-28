using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs;

public record LoginUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
