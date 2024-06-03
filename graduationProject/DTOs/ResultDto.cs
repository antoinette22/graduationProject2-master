using System.Text.Json.Serialization;

namespace graduationProject.Dtos
{
    public class ResultDto
    {
        
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string[] Errors { get; set; }
    }
}
