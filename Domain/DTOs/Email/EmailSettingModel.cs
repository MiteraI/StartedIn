using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Email
{
    public class EmailSettingModel
    {
        public static EmailSettingModel Instance { get; set; }

        public string FromEmailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public Smtp Smtp { get; set; }
    }
}
