using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Domain.Context;

public class AppDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _config;
    private readonly ILogger<AppDbContext> _logger;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config, ILogger<AppDbContext> logger) : base(options)
    {
        _config = config;
        _logger = logger;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var envDatabaseUrl = _config.GetValue<string>("DATABASE_URL");
        if (!string.IsNullOrEmpty(envDatabaseUrl))
        {
            var databaseUri = new Uri(envDatabaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            optionsBuilder.UseNpgsql(
                               $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};Trust Server Certificate=true"
                                          );
        }
        else
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("StartedInDB"));
        }
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Phase> Phases { get; set; }
    public DbSet<Taskboard> Taskboards { get; set; }
    public DbSet<MajorTask> MajorTasks { get; set; }
    public DbSet<MinorTask> MinorTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasMany(x => x.Interactions)
            .WithMany(y => y.Posts)
            .UsingEntity(z => z.ToTable("PostInteraction"));

        modelBuilder.Entity<Team>()
            .HasMany(x => x.Users)
            .WithMany(y => y.Teams)
            .UsingEntity(z => z.ToTable("TeamAccount"));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}