using Microsoft.EntityFrameworkCore;
using User.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Scoped IUserFactory
builder.Services.AddScoped<IUserFactory, UserFactory>();

// Add DbContext Service
string connectionString = "Server=127.0.0.1,1433;Database=Users;User Id=sa;Password=Password_123;TrustServerCertificate=true";

builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseSqlServer(connectionString));

// Adding Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.UseCors();
app.UseRouting();
app.UseAuthorization();
app.Run();
