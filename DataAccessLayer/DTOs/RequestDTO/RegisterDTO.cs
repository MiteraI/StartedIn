using DataAccessLayer.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.RequestDTO
{
    public class RegisterDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "FullName is required")]
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
       

    }
}
