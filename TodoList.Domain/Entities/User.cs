using Microsoft.AspNetCore.Identity;
using System;
namespace TodoList.Domain.Entities;

public class User : IdentityUser
{
    public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>();
}
