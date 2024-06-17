using Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace StartedIn.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Get the database URL from the environment variables for containerized deployments
            var envDatabaseUrl = configuration.GetValue<string>("DATABASE_URL");
            if (string.IsNullOrEmpty(envDatabaseUrl))
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("StartedInDB"));
                });
            }
            else
            {
                var databaseUri = new Uri(envDatabaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(
                                   $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};Trust Server Certificate=true"
                                              );
                });
            }

            // Create scope to migrate database
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            return services;
        }
    }
}
