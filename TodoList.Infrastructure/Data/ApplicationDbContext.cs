using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<TodoItem>().HasData(new TodoItem { Id = Guid.Parse("F4E66C9F-E4B5-4F21-AED6-1EAE3AE95ABE"), Title = "First ToDo",
            Description = "This is The First ToDo Created for example", CreatedAt = DateTime.Now, IsComplete = false });
    }

}

