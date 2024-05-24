using Domain.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEmailService
    {
        void SendMail(SendEmailModel model);
        void SendVerificationMail(string receiveEmail, string id);
    }
}
