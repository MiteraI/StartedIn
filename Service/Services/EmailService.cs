using CrossCutting.DTOs.Email;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Service.Services.Interface;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public void SendMail(SendEmailModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = "",
                    Body = model.Content,
                    IsBodyHtml = false,
                };
                mailMessage.From = new MailAddress(EmailSettingModel.Instance.FromEmailAddress, EmailSettingModel.Instance.FromDisplayName);
                mailMessage.To.Add(model.ReceiveAddress);

                var smtp = new SmtpClient()
                {
                    EnableSsl = EmailSettingModel.Instance.Smtp.EnableSsl,
                    Host = EmailSettingModel.Instance.Smtp.Host,
                    Port = EmailSettingModel.Instance.Smtp.Port,
                };
                var network = new NetworkCredential(EmailSettingModel.Instance.Smtp.EmailAddress, EmailSettingModel.Instance.Smtp.Password);
                smtp.Credentials = network;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SendVerificationMail(string receiveEmail, string id)
        {
            var appDomain = _configuration.GetValue<string>("API_DOMAIN") ?? (_configuration["Api_domain"]);
            try
            {

                MailMessage mailMessage = new MailMessage()
                {

                    Subject = "Kích hoạt tài khoản",
                    Body = $"Bạn vui lòng bấm vào đường link sau để kích hoạt tài khoản StartedIn:\n{appDomain}/api/activate-user/{id} \n\n Xin chân thành cảm ơn vì đã đồng hành cùng StartedIn!",
                    IsBodyHtml = false,
                };
                mailMessage.From = new MailAddress(EmailSettingModel.Instance.FromEmailAddress, EmailSettingModel.Instance.FromDisplayName);
                mailMessage.To.Add(receiveEmail);

                var smtp = new SmtpClient()
                {
                    EnableSsl = EmailSettingModel.Instance.Smtp.EnableSsl,
                    Host = EmailSettingModel.Instance.Smtp.Host,
                    Port = EmailSettingModel.Instance.Smtp.Port,
                };
                var network = new NetworkCredential(EmailSettingModel.Instance.Smtp.EmailAddress, EmailSettingModel.Instance.Smtp.Password);
                smtp.Credentials = network;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
    }
}
