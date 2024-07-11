using CrossCutting.DTOs.Email;
namespace Service.Services.Interface
{
    public interface IEmailService
    {
        void SendMail(SendEmailModel model);
        void SendVerificationMail(string receiveEmail, string id);
        void SendInvitationToTeam(string receiveEmail, string teamId);
    }
}
