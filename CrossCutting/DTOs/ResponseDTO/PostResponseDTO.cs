using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class PostResponseDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserImgUrl { get; set; }
        public string UserFullName { get; set; }
        public string Content { get; set; }
        public int CommentCount { get; set; } = 0;
        public int InteractionCount { get; set; } = 0;

        [JsonProperty(PropertyName = "postImgUrl")]
        [JsonPropertyName("postImgUrl")]
        public IEnumerable<string> ImgUrls { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}
