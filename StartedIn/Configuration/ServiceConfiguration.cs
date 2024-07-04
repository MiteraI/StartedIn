using Service.Services;
using Service.Services.Interface;

namespace StartedIn.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<IProjectService, ProjectService>();
            return services;
        }
    }
}
