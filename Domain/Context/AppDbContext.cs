using CrossCutting.Enum;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class AppDbContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
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
    public DbSet<TeamUser> TeamUsers { get; set; }
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasMany(x => x.Interactions)
            .WithMany(y => y.Posts)
            .UsingEntity(z => z.ToTable("PostInteraction"));

        modelBuilder.Entity<TeamUser>()
            .HasKey(tu => new { tu.TeamId, tu.UserId });

        modelBuilder.Entity<TeamUser>()
            .HasOne(tu => tu.Team)
            .WithMany(t => t.TeamUsers)
            .HasForeignKey(tu => tu.TeamId);

        modelBuilder.Entity<TeamUser>()
            .HasOne(tu => tu.User)
            .WithMany(u => u.TeamUsers)
            .HasForeignKey(tu => tu.UserId);

        modelBuilder.Entity<TeamUser>()
            .Property(u => u.RoleInTeam)
            .HasConversion(
                v => v.ToString(),
                v => (RoleInTeam)Enum.Parse(typeof(RoleInTeam), v));

        modelBuilder.Entity<TeamUser>()
            .ToTable("TeamUser");

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });
        modelBuilder.Entity<Interaction>()
            .Property(u => u.InteractionType)
            .HasConversion(
                v => v.ToString(),
                v => (InteractionType)Enum.Parse(typeof(InteractionType), v));
        modelBuilder.Entity<Post>()
            .Property(u=>u.PostStatus)
            .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v));

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.ToTable("Connection");
        });

        modelBuilder.Entity<Connection>()
            .Property(u => u.ConnectionStatus)
            .HasConversion(
                v => v.ToString(),
                v => (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), v));

        modelBuilder.Entity<MinorTask>()
            .Property(u => u.Status)
            .HasConversion(v => v.ToString(),
                v => (MinorTaskStatus)Enum.Parse(typeof(MinorTaskStatus), v));
        
        modelBuilder.Entity<Connection>()
            .HasOne(c => c.Sender)
            .WithMany(c => c.SentConnections)
            .HasForeignKey(u => u.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Connection>()
            .HasOne(c => c.Receiver)
            .WithMany(c => c.ReceivedConnections)
            .HasForeignKey(u => u.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Connection>()
            .HasIndex(c => new { c.SenderId, c.ReceiverId })
            .IsUnique();
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}