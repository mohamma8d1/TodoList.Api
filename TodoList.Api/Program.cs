using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoList.Api.Configurations;
using TodoList.Application.Common.Behaviors;
using TodoList.Application.Features.Todos.Commands.CreateTodo;
using TodoList.Application.Features.Todos.Commands.UpdateTodo;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var allowedOriginsSettings = builder.Configuration.GetSection(AllowedOriginsSettings.SectionName).Get<AllowedOriginsSettings>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOriginsSettings?.Frontend ?? throw new InvalidOperationException("Frontend URL not configured"))
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<AllowedOriginsSettings>(builder.Configuration.GetSection(AllowedOriginsSettings.SectionName));

builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly));

//builder.Services.AddAutoMapper(typeof(TodoItemProfile));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
