using System.ComponentModel.DataAnnotations;


namespace CrossCutting.DTOs.RequestDTO
{
    public class RegisterDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters and less than 40 characters long.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác thực không khớp")]
        public string? ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? PhoneNumber { get; set; }

    }
}
