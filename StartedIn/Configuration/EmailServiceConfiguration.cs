using CrossCutting.DTOs.Email;
namespace StartedIn.Configuration
{
    public static class EmailServiceConfiguration
    {
        public static IServiceCollection EmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            EmailSettingModel.Instance = configuration.GetSection("EmailSettings").Get<EmailSettingModel>();
            EmailSettingModel.Instance.Smtp.EmailAddress = configuration.GetValue<string>("SMTP_EMAIL") ?? EmailSettingModel.Instance.Smtp.EmailAddress;
            EmailSettingModel.Instance.Smtp.Password = configuration.GetValue<string>("SMTP_PASSWORD") ?? EmailSettingModel.Instance.Smtp.Password;
            EmailSettingModel.Instance.FromEmailAddress = configuration.GetValue<string>("FROM_EMAIL") ?? EmailSettingModel.Instance.FromEmailAddress;
            services.AddSingleton(EmailSettingModel.Instance);

            return services;
        }
       
    }
}
