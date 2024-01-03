using Microsoft.OpenApi.Models;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

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
app.UseAuthentication();
app.MapControllers();
app.Run();