using Repository.Repositories;
using Repository.Repositories.Interface;

namespace StartedIn.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
