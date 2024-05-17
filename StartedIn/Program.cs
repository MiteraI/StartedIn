using DataAccessLayer.Context;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Repositories.Repository;
using Services.Extensions;
using Services.Interface;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddJwtAuthenticationService(config);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerService();

var envDatabaseUrl = builder.Configuration.GetValue<string>("DATABASE_URL");
if (!string.IsNullOrEmpty(envDatabaseUrl))
{
    string connectionString = envDatabaseUrl.Replace("postgres://", "")
    .Replace("@", " ")
    .Replace(":", " ")
    .Replace("/", " ");

    string[] parts = connectionString.Split(' ');
    string userId = parts[0];
    string password = parts[1];
    string host = parts[2];
    string port = parts[3];
    string database = parts[4];

    string formattedConnectionString = $"Server={host};Port={port};Database={database};User Id={userId};Password={password};";
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(formattedConnectionString);
        options.UseNpgsql(builder => builder.MigrationsAssembly("StartedIn"));
    });
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("StartedInDB"));
        options.UseNpgsql(builder => builder.MigrationsAssembly("StartedIn"));
    });
}

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<ITokenService, Services.Extensions.TokenService>();

builder.Services.AddAuthorization();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Apply pending migrations
    }
    catch (Exception ex)
    {
        throw new Exception(ex.InnerException?.ToString());
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

var appScope = app.Services.CreateScope();
var appContext = appScope.ServiceProvider.GetRequiredService<AppDbContext>();
var userManager = appScope.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = appScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
try
{
    await DBInitializer.Initialize(appContext, userManager, roleManager);
}
catch (Exception ex)
{
    throw new Exception(ex.InnerException?.ToString());
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();