using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}