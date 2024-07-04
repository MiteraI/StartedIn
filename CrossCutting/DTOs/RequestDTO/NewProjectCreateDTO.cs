using System.ComponentModel.DataAnnotations;

namespace CrossCutting.DTOs.RequestDTO;

public class NewProjectCreateDTO
{
    [Required(ErrorMessage = "Vui lòng điền tên dự án")]
    public string ProjectName { get; set; }
    [Required(ErrorMessage = "Vui lòng ngày bắt đầu")]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "Vui lòng ngày kết thúc")]
    public DateTime EndDate { get; set; }
    public string TeamId { get; set; }
}