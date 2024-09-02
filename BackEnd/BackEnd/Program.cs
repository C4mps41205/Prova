using System.Text;
using Application.DTOs;
using Application.Repositories;
using Application.UseCases;
using Domain.Entities;
using Infra.Adapters.Auth.Jwt;
using Infra.Adapters.Category;
using Infra.Adapters.Databases.Postgres.Context;
using Infra.Adapters.Databases.Postgres.DbContext;
using Infra.Adapters.Task;
using Infra.Adapters.User;
using Infra.Config.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Registro do ContextFactory e DbContext para PostgreSQL (FUTURAMENTE PODE ESCALAR E UTILIZAR QUALQUER OUTRO DB)
builder.Services.AddScoped<IContextFactory, PostgresContextFactory>();
builder.Services.AddDbContext<PostgresDbContext>((serviceProvider, options) =>
{
    var factory = serviceProvider.GetRequiredService<IContextFactory>();
    var context = factory.CreateDbFactory() as PostgresDbContext;
    options.UseNpgsql(context.Database.GetDbConnection().ConnectionString);
});

// dependencia de injeção auth
builder.Services.AddScoped<IAuthDefault<LoginDto>, Auth>();
builder.Services.AddScoped<AuthUseCase>();

// dependencia de injeção task
builder.Services.AddScoped<ICrudDefault<TaskToDo, TaskToDoDto>, TaskAdapter>();
builder.Services.AddScoped<TaskToDoUseCase>();

// dependencia de injeção uer
builder.Services.AddScoped<ICrudDefault<User, UserDto>, UserAdapter>();
builder.Services.AddScoped<UserUseCase>();

// dependencia de injeção cateogry
builder.Services.AddScoped<ICrudDefault<Category, CategoryDto>, CategoryAdapter>();
builder.Services.AddScoped<CategoryUseCase>();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.Run();