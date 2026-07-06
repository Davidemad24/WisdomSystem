using Microsoft.EntityFrameworkCore;
using Wisdom.Persistence;

// Create web application builder
var builder = WebApplication.CreateBuilder(args);

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServer")
));

// Register controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Create web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Http redirection, authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run application
app.Run();