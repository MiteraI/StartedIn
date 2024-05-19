using Domain.Context;
using Domain.Data;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Repositories.Repository;
using Serilog;
using Services.Extensions;
using Services.Interface;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddJwtAuthenticationService(config);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerService();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthorization();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
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