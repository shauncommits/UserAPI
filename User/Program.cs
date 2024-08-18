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

// Add appsettings unto the configuration root
var appSettingsBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Get the current environment url
var url = Environment.GetEnvironmentVariable("ASPNETCORE_SQL_ENV");
// var url = "127.0.0.1"; 

// Get connection string from appsettings
string connectionString = appSettingsBuilder["ConnectionStrings:SqlConnection"]
    .Replace("${ENV}", url);

// Add DbContext Service
builder.Services.AddDbContext<UserDBContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});



// services.AddDbContext<YourDbContext>(options =>
// {
//     options.UseSqlServer(Configuration.GetConnectionString("YourConnectionString"),
//         sqlServerOptionsAction: sqlOptions =>
//         {
//             sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
//         });
// });


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
