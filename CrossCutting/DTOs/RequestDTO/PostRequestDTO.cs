using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace CrossCutting.DTOs.RequestDTO
{
    public class PostRequestDTO
    {
        public string? Content { get; set; }

        public List<IFormFile>? PostImageUrls { get; set; }
    }

}
