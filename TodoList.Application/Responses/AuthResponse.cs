using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Responses;

public record AuthResponse
{
    public string Token { get; set; }
    public string? ErrorMessege = null;
}
