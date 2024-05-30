
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CrossCutting.DTOs.ResponseDTO;

public class ProfileDTO
{
    [JsonProperty(PropertyName = "authorities")]
    [JsonPropertyName("authorities")]
    public IEnumerable<string> UserRoles { get; set; }  
    public string email { get; set; }
    public string fullName { get; set; }
    public string imageUrl { get; set; }
}