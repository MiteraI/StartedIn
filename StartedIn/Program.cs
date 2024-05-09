

using DataAccessLayer.Context;
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
builder.Services.AddJwtAuthenticationService(config);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerService();

// Add session
builder.Services.AddControllersWithViews(); // This registers ITempDataDictionaryFactory and other services
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "EXE201";
    options.IdleTimeout = new TimeSpan(0, 30, 0);
});



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("StartedInDB"));
    options.UseNpgsql(builder => builder.MigrationsAssembly("StartedIn"));
});

builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddTransient<ITokenService, Services.Extensions.TokenService>();




builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //context.Database.Migrate(); // Apply pending migrations
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


app.MapIdentityApi<IdentityUser>();

app.UseStaticFiles();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
