using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Serilog;
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

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}); 
builder.Services.AddJwtAuthenticationService(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

builder.Host.UseSerilog((ctx,config) =>
{
    config.WriteTo.Console().MinimumLevel.Information();
});

var app = builder.Build();

app.UseSerilogRequestLogging();

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