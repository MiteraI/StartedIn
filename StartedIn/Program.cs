using Domain.Context;
using Domain.DTOs.Email;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Repositories.Repository;
using Repositories.Repository.Interface;
using Serilog;
using Services.Interface;
using Services.Service;
using StartedIn.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Logging.AddSerilog();

builder.Services.EmailConfiguration(config);

// Add services to the container.
builder.Services.AddSecurityConfiguration(config);
builder.Services.AddDatabaseConfiguration(config);
builder.Services.AddRepositoryConfiguration();
builder.Services.AddServiceConfiguration(config);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddJwtAuthenticationService(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerService();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddAuthorization();

builder.Host.UseSerilog((ctx,config) =>
{
    config.WriteTo.Console().MinimumLevel.Information();
});

var app = builder.Build();

app.UseSerilogRequestLogging();

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


app.SeedIdentity();
app.UseSecurityConfiguration();
app.MapControllers();

app.Run();