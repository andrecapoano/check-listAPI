using TaskManagerAPI.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Repositories;
using TaskManagerAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
