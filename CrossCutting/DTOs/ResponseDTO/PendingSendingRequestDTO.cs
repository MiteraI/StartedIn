﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class PendingSendingRequestDTO : IdentityResponseDTO
    {
        public string ReceiverName { get; set; }
        public string ProfilePicture { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
