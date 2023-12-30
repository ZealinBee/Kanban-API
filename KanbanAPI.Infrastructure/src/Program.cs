using Microsoft.OpenApi.Models;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using KanbanAPI.Business;
using KanbanAPI.Controller;
using KanbanAPI.Domain;
using KanbanAPI.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IBoardRepo, BoardRepo>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBoardService, BoardService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kanban API",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.MapControllers();
app.Run();