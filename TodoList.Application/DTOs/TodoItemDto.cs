using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs;
public class TodoItemDto
{
    public Guid id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public bool isComplete { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTodoItemDto
{
    public string title { get; set; } = string.Empty;
    public string? description { get; set; } = string.Empty;
}

public class UpdateTodoItemDto
{
    public string? title { get; set; }
    public string? description { get; set; }
    public bool? isComplete { get; set; }
}