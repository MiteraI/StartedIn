using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    // public DbSet<PostInteraction> PostInteractions { get; set; }
    public DbSet<Team> Teams { get; set; }
    // public DbSet<TeamAccount> TeamAccounts { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Phase> Phases { get; set; }
    public DbSet<Taskboard> Taskboards { get; set; }
    public DbSet<MajorTask> MajorTasks { get; set; }
    public DbSet<MinorTask> MinorTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasMany(x => x.Interactions)
            .WithMany(y => y.Posts)
            .UsingEntity(z => z.ToTable("PostInteraction"));
        
        modelBuilder.Entity<Team>()
            .HasMany(x => x.Accounts)
            .WithMany(y => y.Teams)
            .UsingEntity(z => z.ToTable("TeamAccount"));
        base.OnModelCreating(modelBuilder);
    }
}