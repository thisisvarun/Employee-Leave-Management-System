using backend.Controllers;
using backend.Repositories;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register repositories and services with connection string
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddScoped(sp =>
    new LoginRepository(builder.Configuration.GetConnectionString("Default")));
#pragma warning restore CS8604 // Possible null reference argument.
builder.Services.AddScoped(sp =>
    new EmployeeRepository(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<EmployeeService>();

// Configure CORS
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