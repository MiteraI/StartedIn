using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.Email
{
    public class SendEmailModel
    {
        public string ReceiveAddress { get; set; }
        public string Content { get; set; }
    }
}
