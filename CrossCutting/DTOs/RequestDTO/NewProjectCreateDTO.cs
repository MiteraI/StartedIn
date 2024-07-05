using System.ComponentModel.DataAnnotations;

namespace CrossCutting.DTOs.RequestDTO;

public class NewProjectCreateDTO
{
    [Required(ErrorMessage = "Vui lòng điền tên dự án")]
    public string ProjectName { get; set; }
    public string TeamId { get; set; }
}