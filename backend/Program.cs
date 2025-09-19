using backend.Controllers;
using backend.Repositories;
using backend.Repository;
using backend.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<LoginRepository>();

builder.Services.AddSingleton<LoginRepository>();
builder.Services.AddScoped<backend.Repositories.EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => 
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:5274")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middlewares
// app.UseLogger();
// app.UseJwtAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
