using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CrossCutting.DTOs.ResponseDTO;

public class HeaderProfileDTO
{
    [JsonProperty(PropertyName = "authorities")]
    [JsonPropertyName("authorities")]
    public IEnumerable<string> UserRoles { get; set; }  
    public string email { get; set; }
    public string fullName { get; set; }
    public string ProfilePicture { get; set; }
    public string bio { get; set; }
}