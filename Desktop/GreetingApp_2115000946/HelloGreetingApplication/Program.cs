using BusinessLayer.Interface;
using BusinessLayer.Service;
using HelloGreetingApplication.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HelloGreetingApplication.Middleware;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("GreetingDB");
builder.Services.AddDbContext<GreetingDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Add Swagger configuration without contact info
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Greeting API",
        Version = "v1",
        Description = "API for Greeting Application"
    });
});

// Register Dependency Injection
builder.Services.AddScoped<IGreetingBL, GreetingBL>();
builder.Services.AddScoped<IGreetingRL, GreetingRL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // Global Exception Handling Middleware

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greeting API v1");
    //c.RoutePrefix = string.Empty; // Access Swagger at root URL
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
