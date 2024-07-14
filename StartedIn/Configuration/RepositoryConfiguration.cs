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
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConnectionRepository, ConnectionRepository>();
            services.AddScoped<IPhaseRepository, PhaseRepository>();
            services.AddScoped<IMajorTaskRepository, MajorTaskRepository>();
            services.AddScoped<IMinorTaskRepository, MinorTaskRepository>();
            return services;
        }
    }
}
